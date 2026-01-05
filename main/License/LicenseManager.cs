using System;
using System.IO;

namespace main.License
{
    /// <summary>
    /// 라이선스 관리자 - USB 동글 기반 파일 라이선스 검증 및 관리
    /// </summary>
    public class LicenseManager : IDisposable
    {
        private readonly UsbDongle _dongle;
        private LicenseStorage? _storage;
        private LicenseInfo? _currentLicense;
        private bool _isInitialized;

        /// <summary>
        /// 현재 로드된 라이선스 정보
        /// </summary>
        public LicenseInfo? CurrentLicense => _currentLicense;

        /// <summary>
        /// 초기화 상태
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// USB 동글 연결 상태
        /// </summary>
        public bool IsDongleConnected => _dongle.IsConnected;

        /// <summary>
        /// 라이선스 검증 결과 이벤트
        /// </summary>
        public event EventHandler<LicenseValidationEventArgs>? LicenseValidated;

        /// <summary>
        /// 동글 연결/해제 이벤트
        /// </summary>
        public event EventHandler<DongleConnectionEventArgs>? DongleConnectionChanged;

        public LicenseManager()
        {
            _dongle = new UsbDongle();
        }

        /// <summary>
        /// USB 동글 연결 및 라이선스 검증을 초기화합니다.
        /// </summary>
        public LicenseValidationResult Initialize()
        {
            try
            {
                // 동글 연결 시도
                bool connected = _dongle.Connect();
                
                DongleConnectionChanged?.Invoke(this, new DongleConnectionEventArgs(
                    connected, 
                    _dongle.SerialNumber,
                    _dongle.HardwareId
                ));

                if (!connected)
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.DongleNotFound,
                        Message = "USB 동글을 찾을 수 없습니다. 동글이 올바르게 연결되어 있는지 확인하세요."
                    };
                }

                // 하드웨어 ID 기반 스토리지 생성
                var hardwareIdBytes = _dongle.GetHardwareIdBytes();
                _storage = new LicenseStorage(hardwareIdBytes);

                // 라이선스 파일 로드
                _currentLicense = _storage.Load();
                
                if (_currentLicense == null)
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.NoLicense,
                        Message = "라이선스 정보가 없습니다. 라이선스를 활성화하세요."
                    };
                }

                // 하드웨어 바인딩 확인
                if (!string.IsNullOrEmpty(_currentLicense.HardwareId) && 
                    !_currentLicense.MatchesHardware(_dongle.HardwareId))
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.HardwareMismatch,
                        Message = "라이선스가 이 동글에 바인딩되어 있지 않습니다."
                    };
                }

                // 기간 검증
                if (_currentLicense.IsNotStarted())
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.NotStarted,
                        Message = $"라이선스가 {_currentLicense.StartDate:yyyy-MM-dd}부터 유효합니다.",
                        License = _currentLicense
                    };
                }

                if (_currentLicense.IsExpired())
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.Expired,
                        Message = $"라이선스가 {_currentLicense.ExpirationDate:yyyy-MM-dd}에 만료되었습니다.",
                        License = _currentLicense
                    };
                }

                _isInitialized = true;

                var result = new LicenseValidationResult
                {
                    IsValid = true,
                    Status = LicenseStatus.Valid,
                    Message = $"라이선스가 유효합니다. 남은 기간: {_currentLicense.GetRemainingDays()}일",
                    License = _currentLicense
                };

                LicenseValidated?.Invoke(this, new LicenseValidationEventArgs(result));

                return result;
            }
            catch (Exception ex)
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    Status = LicenseStatus.Error,
                    Message = $"라이선스 검증 중 오류가 발생했습니다: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// 라이선스를 활성화합니다.
        /// </summary>
        public bool ActivateLicense(LicenseInfo licenseInfo)
        {
            if (!_dongle.IsConnected)
            {
                if (!_dongle.Connect())
                    return false;
            }

            var hardwareIdBytes = _dongle.GetHardwareIdBytes();
            _storage = new LicenseStorage(hardwareIdBytes);

            // 하드웨어 ID 바인딩
            licenseInfo.HardwareId = _dongle.HardwareId;
            
            return _storage.Save(licenseInfo);
        }

        /// <summary>
        /// 라이선스 파일에서 라이선스를 가져옵니다.
        /// </summary>
        public LicenseValidationResult ImportFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.NoLicense,
                        Message = "라이선스 파일을 찾을 수 없습니다."
                    };
                }

                // 동글 연결 확인
                if (!_dongle.IsConnected)
                {
                    if (!_dongle.Connect())
                    {
                        return new LicenseValidationResult
                        {
                            IsValid = false,
                            Status = LicenseStatus.DongleNotFound,
                            Message = "USB 동글을 찾을 수 없습니다."
                        };
                    }
                }

                var hardwareIdBytes = _dongle.GetHardwareIdBytes();
                _storage = new LicenseStorage(hardwareIdBytes);

                var license = _storage.ImportFromFile(filePath);
                
                if (license == null)
                {
                    return new LicenseValidationResult
                    {
                        IsValid = false,
                        Status = LicenseStatus.InvalidSignature,
                        Message = "라이선스 파일이 유효하지 않거나 이 동글에 바인딩되지 않았습니다."
                    };
                }

                // 로컬에 저장
                if (_storage.Save(license))
                {
                    _currentLicense = license;
                    return Initialize();
                }

                return new LicenseValidationResult
                {
                    IsValid = false,
                    Status = LicenseStatus.Error,
                    Message = "라이선스를 저장하는 데 실패했습니다."
                };
            }
            catch (Exception ex)
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    Status = LicenseStatus.Error,
                    Message = $"라이선스 파일 가져오기 중 오류: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// 특정 기능이 활성화되어 있는지 확인합니다.
        /// </summary>
        public bool HasFeature(string featureName)
        {
            return _currentLicense?.HasFeature(featureName) ?? false;
        }

        /// <summary>
        /// 라이선스 유형을 확인합니다.
        /// </summary>
        public LicenseType GetLicenseType()
        {
            return _currentLicense?.Type ?? LicenseType.Trial;
        }

        /// <summary>
        /// 남은 라이선스 일수를 반환합니다.
        /// </summary>
        public int GetRemainingDays()
        {
            return _currentLicense?.GetRemainingDays() ?? 0;
        }

        /// <summary>
        /// 동글 연결을 다시 확인합니다.
        /// </summary>
        public bool CheckDongleConnection()
        {
            if (_dongle.IsConnected)
                return true;

            bool connected = _dongle.Connect();
            DongleConnectionChanged?.Invoke(this, new DongleConnectionEventArgs(
                connected, 
                _dongle.SerialNumber,
                _dongle.HardwareId
            ));
            
            return connected;
        }

        /// <summary>
        /// 현재 동글의 하드웨어 ID를 반환합니다.
        /// </summary>
        public string GetHardwareId()
        {
            return _dongle.HardwareId;
        }

        /// <summary>
        /// 라이선스 파일 경로를 반환합니다.
        /// </summary>
        public string GetLicenseFilePath()
        {
            return _storage?.GetLicensePath() ?? LicenseStorage.DefaultLicensePath;
        }

        public void Dispose()
        {
            _dongle.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// 라이선스 검증 결과
    /// </summary>
    public class LicenseValidationResult
    {
        public bool IsValid { get; set; }
        public LicenseStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public LicenseInfo? License { get; set; }
    }

    /// <summary>
    /// 라이선스 상태
    /// </summary>
    public enum LicenseStatus
    {
        Valid,
        DongleNotFound,
        NoLicense,
        InvalidSignature,
        DecryptionFailed,
        HardwareMismatch,
        NotStarted,
        Expired,
        Error
    }

    /// <summary>
    /// 라이선스 검증 이벤트 인자
    /// </summary>
    public class LicenseValidationEventArgs : EventArgs
    {
        public LicenseValidationResult Result { get; }

        public LicenseValidationEventArgs(LicenseValidationResult result)
        {
            Result = result;
        }
    }

    /// <summary>
    /// 동글 연결 이벤트 인자
    /// </summary>
    public class DongleConnectionEventArgs : EventArgs
    {
        public bool IsConnected { get; }
        public string? SerialNumber { get; }
        public string? HardwareId { get; }

        public DongleConnectionEventArgs(bool isConnected, string? serialNumber, string? hardwareId)
        {
            IsConnected = isConnected;
            SerialNumber = serialNumber;
            HardwareId = hardwareId;
        }
    }
}

