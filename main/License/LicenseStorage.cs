using System;
using System.IO;

namespace main.License
{
    /// <summary>
    /// 라이선스 파일 저장 및 로드를 담당하는 클래스
    /// </summary>
    public class LicenseStorage
    {
        private readonly string _licensePath;
        private readonly LicenseCrypto _crypto;

        /// <summary>
        /// 기본 라이선스 파일 경로
        /// </summary>
        public static string DefaultLicensePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ZEROFIX",
            "license.lic"
        );

        /// <summary>
        /// 기본 경로로 초기화합니다.
        /// </summary>
        public LicenseStorage(byte[] hardwareId) : this(DefaultLicensePath, hardwareId)
        {
        }

        /// <summary>
        /// 지정된 경로로 초기화합니다.
        /// </summary>
        public LicenseStorage(string licensePath, byte[] hardwareId)
        {
            _licensePath = licensePath;
            _crypto = new LicenseCrypto(hardwareId);
            
            // 디렉토리 생성
            var directory = Path.GetDirectoryName(_licensePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 라이선스를 파일에 저장합니다.
        /// </summary>
        public bool Save(LicenseInfo license)
        {
            try
            {
                var encryptedData = _crypto.Encrypt(license);
                var packagedData = _crypto.PackageWithSignature(encryptedData);
                
                File.WriteAllBytes(_licensePath, packagedData);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"License save error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 라이선스를 파일에서 로드합니다.
        /// </summary>
        public LicenseInfo? Load()
        {
            try
            {
                if (!File.Exists(_licensePath))
                    return null;

                var packagedData = File.ReadAllBytes(_licensePath);
                var unpackedData = _crypto.UnpackageAndVerify(packagedData);
                
                if (unpackedData == null)
                    return null;

                return _crypto.Decrypt(unpackedData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"License load error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 라이선스 파일이 존재하는지 확인합니다.
        /// </summary>
        public bool Exists()
        {
            return File.Exists(_licensePath);
        }

        /// <summary>
        /// 라이선스 파일을 삭제합니다.
        /// </summary>
        public bool Delete()
        {
            try
            {
                if (File.Exists(_licensePath))
                {
                    File.Delete(_licensePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 라이선스를 외부 파일로 내보냅니다.
        /// </summary>
        public bool ExportToFile(LicenseInfo license, string filePath)
        {
            try
            {
                var encryptedData = _crypto.Encrypt(license);
                var packagedData = _crypto.PackageWithSignature(encryptedData);
                
                File.WriteAllBytes(filePath, packagedData);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"License export error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 외부 파일에서 라이선스를 가져옵니다.
        /// </summary>
        public LicenseInfo? ImportFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                var packagedData = File.ReadAllBytes(filePath);
                var unpackedData = _crypto.UnpackageAndVerify(packagedData);
                
                if (unpackedData == null)
                    return null;

                return _crypto.Decrypt(unpackedData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"License import error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 라이선스 파일 경로를 반환합니다.
        /// </summary>
        public string GetLicensePath() => _licensePath;
    }
}

