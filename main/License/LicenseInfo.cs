using System;
using System.Text.Json;

namespace main.License
{
    /// <summary>
    /// 라이선스 정보를 담는 클래스
    /// </summary>
    public class LicenseInfo
    {
        /// <summary>
        /// 라이선스 고유 ID
        /// </summary>
        public string LicenseId { get; set; } = string.Empty;

        /// <summary>
        /// 제품명
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 제품 버전
        /// </summary>
        public string ProductVersion { get; set; } = string.Empty;

        /// <summary>
        /// 라이선스 유형 (Trial, Standard, Professional, Enterprise)
        /// </summary>
        public LicenseType Type { get; set; } = LicenseType.Trial;

        /// <summary>
        /// 라이선스 발급일
        /// </summary>
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 라이선스 시작일
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 라이선스 만료일
        /// </summary>
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(30);

        /// <summary>
        /// 최대 사용자 수 (0 = 무제한)
        /// </summary>
        public int MaxUsers { get; set; } = 1;

        /// <summary>
        /// 회사명
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 사용자 이메일
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 하드웨어 바인딩 ID (USB 동글 시리얼)
        /// </summary>
        public string HardwareId { get; set; } = string.Empty;

        /// <summary>
        /// 활성화된 기능 목록 (쉼표로 구분)
        /// </summary>
        public string EnabledFeatures { get; set; } = string.Empty;

        /// <summary>
        /// 추가 메타데이터 (JSON 형식)
        /// </summary>
        public string Metadata { get; set; } = "{}";

        /// <summary>
        /// 라이선스가 유효한지 확인합니다.
        /// </summary>
        public bool IsValid()
        {
            var now = DateTime.UtcNow;
            return now >= StartDate && now <= ExpirationDate;
        }

        /// <summary>
        /// 라이선스가 만료되었는지 확인합니다.
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpirationDate;
        }

        /// <summary>
        /// 라이선스 시작 전인지 확인합니다.
        /// </summary>
        public bool IsNotStarted()
        {
            return DateTime.UtcNow < StartDate;
        }

        /// <summary>
        /// 남은 일수를 반환합니다.
        /// </summary>
        public int GetRemainingDays()
        {
            if (IsExpired()) return 0;
            return (ExpirationDate - DateTime.UtcNow).Days;
        }

        /// <summary>
        /// 특정 기능이 활성화되어 있는지 확인합니다.
        /// </summary>
        public bool HasFeature(string featureName)
        {
            if (string.IsNullOrEmpty(EnabledFeatures)) return false;
            var features = EnabledFeatures.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return Array.Exists(features, f => f.Trim().Equals(featureName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 하드웨어 ID가 일치하는지 확인합니다.
        /// </summary>
        public bool MatchesHardware(string hardwareId)
        {
            if (string.IsNullOrEmpty(HardwareId)) return true; // 바인딩 없음
            return HardwareId.Equals(hardwareId, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// JSON으로 직렬화합니다.
        /// </summary>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// JSON에서 역직렬화합니다.
        /// </summary>
        public static LicenseInfo? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<LicenseInfo>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 라이선스 정보 요약을 반환합니다.
        /// </summary>
        public override string ToString()
        {
            return $"License: {LicenseId} | Product: {ProductName} v{ProductVersion} | " +
                   $"Type: {Type} | Valid: {StartDate:yyyy-MM-dd} ~ {ExpirationDate:yyyy-MM-dd} | " +
                   $"Remaining: {GetRemainingDays()} days";
        }
    }

    /// <summary>
    /// 라이선스 유형
    /// </summary>
    public enum LicenseType
    {
        /// <summary>
        /// 평가판
        /// </summary>
        Trial = 0,

        /// <summary>
        /// 표준 라이선스
        /// </summary>
        Standard = 1,

        /// <summary>
        /// 프로페셔널 라이선스
        /// </summary>
        Professional = 2,

        /// <summary>
        /// 기업용 라이선스
        /// </summary>
        Enterprise = 3,

        /// <summary>
        /// 교육용 라이선스
        /// </summary>
        Educational = 4,

        /// <summary>
        /// 개발자 라이선스
        /// </summary>
        Developer = 5
    }
}

