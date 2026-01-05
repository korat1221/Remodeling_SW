using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HidSharp;

namespace main.License
{
    /// <summary>
    /// USB 동글과의 통신을 담당하는 클래스
    /// 동글의 고유 정보를 읽어 하드웨어 바인딩에 사용
    /// </summary>
    public class UsbDongle : IDisposable
    {
        // Feitian USB 동글의 Vendor ID (필요시 수정)
        private const int FEITIAN_VENDOR_ID = 0x096E;
        
        // 제품 ID 목록 (ROCKEY 시리즈 등)
        private static readonly int[] PRODUCT_IDS = { 0x0608, 0x0807, 0x0809, 0x080A };
        
        private HidDevice? _device;
        private HidStream? _stream;
        private bool _isConnected;
        private string _hardwareId = "";
        private string _serialNumber = "";
        private string _devicePath = "";
        private int _vendorId;
        private int _productId;

        public bool IsConnected => _isConnected;
        public string SerialNumber => _serialNumber;
        public string HardwareId => _hardwareId;
        public string DevicePath => _devicePath;
        public int VendorId => _vendorId;
        public int ProductId => _productId;

        /// <summary>
        /// USB 동글에 연결합니다.
        /// </summary>
        public bool Connect()
        {
            try
            {
                // Feitian 장치 검색
                var devices = DeviceList.Local.GetHidDevices()
                    .Where(d => d.VendorID == FEITIAN_VENDOR_ID)
                    .ToList();

                // Feitian이 없으면 다른 USB HID 장치 검색 (테스트용)
                if (!devices.Any())
                {
                    devices = DeviceList.Local.GetHidDevices()
                        .Where(d => d.VendorID != 0)
                        .Take(5)
                        .ToList();
                }

                if (!devices.Any())
                {
                    _isConnected = false;
                    return false;
                }

                _device = devices.First();
                
                try
                {
                    _stream = _device.Open();
                }
                catch
                {
                    // 스트림 열기 실패해도 장치 정보는 읽을 수 있음
                    _stream = null;
                }
                
                _isConnected = true;
                _vendorId = _device.VendorID;
                _productId = _device.ProductID;
                _devicePath = _device.DevicePath;
                
                // 장치 정보 읽기
                ReadDeviceInfo();
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connect error: {ex.Message}");
                _isConnected = false;
                return false;
            }
        }

        /// <summary>
        /// 장치의 고유 정보를 읽습니다.
        /// </summary>
        private void ReadDeviceInfo()
        {
            if (_device == null) return;

            try
            {
                // 시리얼 번호 읽기 시도
                try
                {
                    _serialNumber = _device.GetSerialNumber() ?? "";
                }
                catch
                {
                    _serialNumber = "";
                }

                // 하드웨어 ID 생성
                _hardwareId = GenerateHardwareId();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ReadDeviceInfo error: {ex.Message}");
                _serialNumber = "";
                _hardwareId = GenerateHardwareId();
            }
        }

        /// <summary>
        /// 고유 하드웨어 ID를 생성합니다.
        /// 여러 요소를 조합하여 최대한 고유한 ID 생성
        /// </summary>
        private string GenerateHardwareId()
        {
            var sb = new StringBuilder();
            
            // 1. 기본 정보
            sb.Append($"{_vendorId:X4}-{_productId:X4}");
            
            // 2. 시리얼 번호 (있는 경우)
            if (!string.IsNullOrEmpty(_serialNumber))
            {
                sb.Append($"-{_serialNumber}");
            }
            
            // 3. 추가 장치 정보 수집
            if (_device != null)
            {
                try
                {
                    // Report Descriptor 길이 (장치마다 다를 수 있음)
                    var reportDesc = _device.GetRawReportDescriptor();
                    if (reportDesc != null && reportDesc.Length > 0)
                    {
                        sb.Append($"-RD{reportDesc.Length}");
                        // Report Descriptor의 해시 추가
                        var rdHash = ComputeSimpleHash(reportDesc);
                        sb.Append($"-{rdHash:X8}");
                    }
                }
                catch { }

                try
                {
                    // Max Input/Output Report Length
                    sb.Append($"-I{_device.GetMaxInputReportLength()}");
                    sb.Append($"-O{_device.GetMaxOutputReportLength()}");
                    sb.Append($"-F{_device.GetMaxFeatureReportLength()}");
                }
                catch { }
            }
            
            // SHA256 해시로 고정 길이 ID 생성
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            return BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 32);
        }

        /// <summary>
        /// 바이트 배열의 간단한 해시를 계산합니다.
        /// </summary>
        private static uint ComputeSimpleHash(byte[] data)
        {
            uint hash = 5381;
            foreach (byte b in data)
            {
                hash = ((hash << 5) + hash) + b;
            }
            return hash;
        }

        /// <summary>
        /// 하드웨어 ID를 바이트 배열로 반환합니다.
        /// </summary>
        public byte[] GetHardwareIdBytes()
        {
            return Encoding.UTF8.GetBytes(_hardwareId);
        }

        /// <summary>
        /// 장치의 상세 정보를 반환합니다. (진단용)
        /// </summary>
        public string GetDeviceInfo()
        {
            if (_device == null) return "장치 없음";
            
            var sb = new StringBuilder();
            sb.AppendLine("═══════════════════════════════════════");
            sb.AppendLine("         USB 동글 상세 정보");
            sb.AppendLine("═══════════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine($"【기본 정보】");
            sb.AppendLine($"  제조사: {GetManufacturer()}");
            sb.AppendLine($"  제품명: {GetProductName()}");
            sb.AppendLine($"  Vendor ID: 0x{_vendorId:X4} ({_vendorId})");
            sb.AppendLine($"  Product ID: 0x{_productId:X4} ({_productId})");
            sb.AppendLine($"  시리얼 번호: {(string.IsNullOrEmpty(_serialNumber) ? "(없음)" : _serialNumber)}");
            sb.AppendLine();
            
            sb.AppendLine($"【장치 경로】");
            sb.AppendLine($"  {_devicePath}");
            sb.AppendLine();

            try
            {
                sb.AppendLine($"【Report 정보】");
                sb.AppendLine($"  Max Input Report: {_device.GetMaxInputReportLength()} bytes");
                sb.AppendLine($"  Max Output Report: {_device.GetMaxOutputReportLength()} bytes");
                sb.AppendLine($"  Max Feature Report: {_device.GetMaxFeatureReportLength()} bytes");
                
                var reportDesc = _device.GetRawReportDescriptor();
                if (reportDesc != null)
                {
                    sb.AppendLine($"  Report Descriptor 길이: {reportDesc.Length} bytes");
                    sb.AppendLine($"  Report Descriptor 해시: 0x{ComputeSimpleHash(reportDesc):X8}");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"  (Report 정보 읽기 실패: {ex.Message})");
            }
            
            sb.AppendLine();
            sb.AppendLine($"【생성된 하드웨어 ID】");
            sb.AppendLine($"  {_hardwareId}");
            sb.AppendLine();
            sb.AppendLine("═══════════════════════════════════════");
            
            return sb.ToString();
        }

        /// <summary>
        /// 모든 연결된 USB HID 장치 목록을 반환합니다. (진단용)
        /// </summary>
        public static List<UsbDeviceInfo> GetAllDevices()
        {
            var result = new List<UsbDeviceInfo>();
            
            try
            {
                var devices = DeviceList.Local.GetHidDevices().ToList();
                
                foreach (var device in devices)
                {
                    var info = new UsbDeviceInfo
                    {
                        VendorId = device.VendorID,
                        ProductId = device.ProductID,
                        DevicePath = device.DevicePath
                    };
                    
                    try { info.Manufacturer = device.GetManufacturer(); } catch { }
                    try { info.ProductName = device.GetProductName(); } catch { }
                    try { info.SerialNumber = device.GetSerialNumber(); } catch { }
                    try { info.MaxInputReportLength = device.GetMaxInputReportLength(); } catch { }
                    try { info.MaxOutputReportLength = device.GetMaxOutputReportLength(); } catch { }
                    try 
                    { 
                        var rd = device.GetRawReportDescriptor();
                        info.ReportDescriptorLength = rd?.Length ?? 0;
                        info.ReportDescriptorHash = rd != null ? ComputeSimpleHash(rd) : 0;
                    } 
                    catch { }
                    
                    result.Add(info);
                }
            }
            catch { }
            
            return result;
        }

        /// <summary>
        /// 제조사 이름을 가져옵니다.
        /// </summary>
        public string GetManufacturer()
        {
            try
            {
                return _device?.GetManufacturer() ?? "알 수 없음";
            }
            catch
            {
                return "알 수 없음";
            }
        }

        /// <summary>
        /// 제품 이름을 가져옵니다.
        /// </summary>
        public string GetProductName()
        {
            try
            {
                return _device?.GetProductName() ?? "알 수 없음";
            }
            catch
            {
                return "알 수 없음";
            }
        }

        /// <summary>
        /// 연결을 해제합니다.
        /// </summary>
        public void Disconnect()
        {
            _stream?.Close();
            _stream?.Dispose();
            _stream = null;
            _device = null;
            _isConnected = false;
            _hardwareId = "";
            _serialNumber = "";
            _devicePath = "";
        }

        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// USB 장치 정보 (진단용)
    /// </summary>
    public class UsbDeviceInfo
    {
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public string Manufacturer { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string DevicePath { get; set; } = "";
        public int MaxInputReportLength { get; set; }
        public int MaxOutputReportLength { get; set; }
        public int ReportDescriptorLength { get; set; }
        public uint ReportDescriptorHash { get; set; }

        public override string ToString()
        {
            return $"[{VendorId:X4}:{ProductId:X4}] {ProductName} (Serial: {(string.IsNullOrEmpty(SerialNumber) ? "없음" : SerialNumber)})";
        }
    }
}

