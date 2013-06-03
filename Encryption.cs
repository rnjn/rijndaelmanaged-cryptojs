using System;
using System.Security.Cryptography;
using System.Text;

namespace SalesDashboard.Encryption
{
    public class Encryption
    {
        private readonly string secretKey;

        public Encryption(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(secretKey)));
        }

        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(secretKey)));
        }

        private byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            byte[] transformFinalBlock = rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return transformFinalBlock;
        }

        private byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        private RijndaelManaged GetRijndaelManaged(string key)
        {
            var keyBytes = new byte[16];
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            var rijndaelManaged = new RijndaelManaged
                                      {
                                          Mode = CipherMode.CBC,
                                          Padding = PaddingMode.PKCS7,
                                          KeySize = 128,
                                          BlockSize = 128,
                                          Key = keyBytes,
                                          IV = keyBytes
                                      };
            return rijndaelManaged;
        }

    }
}