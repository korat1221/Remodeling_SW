using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace main.License
{
    /// <summary>
    /// 라이선스 기간 정보를 암호화/복호화하는 클래스
    /// </summary>
    public class LicenseCrypto
    {
        // 기본 암호화 키 (실제 사용 시 변경 필요)
        private static readonly byte[] DefaultKey = new byte[]
        {
            0x4A, 0x8F, 0xC2, 0x71, 0xE5, 0x93, 0xD6, 0x2B,
            0x7C, 0x1E, 0x4D, 0x8A, 0xF3, 0x69, 0xB5, 0x2F,
            0x91, 0xC4, 0x7E, 0x3A, 0xD8, 0x5B, 0x0F, 0x62,
            0xA7, 0x1C, 0x49, 0x86, 0xE3, 0x5D, 0xB0, 0x2C
        };

        private static readonly byte[] DefaultIV = new byte[]
        {
            0x3B, 0x7A, 0xC6, 0x15, 0x89, 0xD4, 0x2E, 0xF0,
            0x51, 0xA3, 0x6C, 0x94, 0xE7, 0x28, 0xBD, 0x4F
        };

        private readonly byte[] _encryptionKey;
        private readonly byte[] _iv;

        /// <summary>
        /// 기본 키로 초기화합니다.
        /// </summary>
        public LicenseCrypto()
        {
            _encryptionKey = DefaultKey;
            _iv = DefaultIV;
        }

        /// <summary>
        /// 하드웨어 ID 기반으로 고유 키를 생성하여 초기화합니다.
        /// </summary>
        /// <param name="hardwareId">USB 동글의 하드웨어 ID</param>
        public LicenseCrypto(byte[] hardwareId)
        {
            // 하드웨어 ID와 기본 키를 조합하여 고유 키 생성
            using var sha256 = SHA256.Create();
            var combinedKey = new byte[DefaultKey.Length + hardwareId.Length];
            Array.Copy(DefaultKey, 0, combinedKey, 0, DefaultKey.Length);
            Array.Copy(hardwareId, 0, combinedKey, DefaultKey.Length, hardwareId.Length);
            
            _encryptionKey = sha256.ComputeHash(combinedKey);
            
            // IV는 하드웨어 ID의 해시로 생성
            using var md5 = MD5.Create();
            _iv = md5.ComputeHash(hardwareId);
        }

        /// <summary>
        /// 라이선스 정보를 암호화합니다.
        /// </summary>
        public byte[] Encrypt(LicenseInfo licenseInfo)
        {
            var json = licenseInfo.ToJson();
            return EncryptString(json);
        }

        /// <summary>
        /// 암호화된 데이터를 라이선스 정보로 복호화합니다.
        /// </summary>
        public LicenseInfo? Decrypt(byte[] encryptedData)
        {
            try
            {
                var json = DecryptToString(encryptedData);
                return LicenseInfo.FromJson(json);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 문자열을 암호화합니다.
        /// </summary>
        public byte[] EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _encryptionKey;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();
            
            return ms.ToArray();
        }

        /// <summary>
        /// 암호화된 데이터를 문자열로 복호화합니다.
        /// </summary>
        public string DecryptToString(byte[] cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = _encryptionKey;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(cipherText);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs, Encoding.UTF8);
            
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 데이터의 HMAC 서명을 생성합니다.
        /// </summary>
        public byte[] CreateSignature(byte[] data)
        {
            using var hmac = new HMACSHA256(_encryptionKey);
            return hmac.ComputeHash(data);
        }

        /// <summary>
        /// 서명을 검증합니다.
        /// </summary>
        public bool VerifySignature(byte[] data, byte[] signature)
        {
            var computedSignature = CreateSignature(data);
            
            if (computedSignature.Length != signature.Length)
                return false;

            for (int i = 0; i < computedSignature.Length; i++)
            {
                if (computedSignature[i] != signature[i])
                    return false;
            }
            
            return true;
        }

        /// <summary>
        /// 라이선스 데이터를 서명과 함께 패키징합니다.
        /// </summary>
        public byte[] PackageWithSignature(byte[] encryptedData)
        {
            var signature = CreateSignature(encryptedData);
            var package = new byte[4 + signature.Length + encryptedData.Length];
            
            // 헤더: 서명 길이 (4바이트)
            BitConverter.GetBytes(signature.Length).CopyTo(package, 0);
            // 서명
            signature.CopyTo(package, 4);
            // 암호화된 데이터
            encryptedData.CopyTo(package, 4 + signature.Length);
            
            return package;
        }

        /// <summary>
        /// 패키지를 검증하고 암호화된 데이터를 추출합니다.
        /// </summary>
        public byte[]? UnpackageAndVerify(byte[] package)
        {
            if (package.Length < 36) // 최소 4 + 32 (HMAC) 바이트
                return null;

            var signatureLength = BitConverter.ToInt32(package, 0);
            
            if (package.Length < 4 + signatureLength)
                return null;

            var signature = new byte[signatureLength];
            Array.Copy(package, 4, signature, 0, signatureLength);
            
            var encryptedData = new byte[package.Length - 4 - signatureLength];
            Array.Copy(package, 4 + signatureLength, encryptedData, 0, encryptedData.Length);
            
            if (!VerifySignature(encryptedData, signature))
                return null;
            
            return encryptedData;
        }
    }
}

