using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace ET
{
    public static class EncryptHelper
    {
        // 加密字符串
        //确保密钥长度符合 AES 的要求（16、24 或 32 字节）
        public static string EncryptString(string plainText, string key)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV(); // 自动生成初始化向量
                byte[] encryptedBytes;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }

                // 将加密数据和 IV 合并后返回（IV 需要用于解密）
                byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];
                Array.Copy(aes.IV, 0, result, 0, aes.IV.Length); // 复制 IV 到结果数组
                Array.Copy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length); // 复制加密数据

                return Convert.ToBase64String(result);
            }
        }

        // 解密字符串
        //确保密钥长度符合 AES 的要求（16、24 或 32 字节）
        public static string DecryptString(string cipherText, string key)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;

                // 手动提取 IV
                int ivSize = aes.BlockSize / 8;
                byte[] iv = new byte[ivSize];
                Array.Copy(cipherBytes, 0, iv, 0, ivSize); // 提取 IV
                aes.IV = iv;

                // 提取加密数据
                byte[] encryptedBytes = new byte[cipherBytes.Length - ivSize];
                Array.Copy(cipherBytes, ivSize, encryptedBytes, 0, encryptedBytes.Length);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    byte[] decryptedBytes = ms.ToArray();

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        // static void Main()
        // {
        //     string original = "Hello, World!";
        //     string key = "mysecretkey12345"; // 密钥长度必须与 AES 的 KeySize 匹配（如 16 字节）
        //
        //     string encrypted = EncryptHelper.EncryptString(original, key);
        //     Console.WriteLine($"Encrypted: {encrypted}");
        //
        //     string decrypted = EncryptHelper.DecryptString(encrypted, key);
        //     Console.WriteLine($"Decrypted: {decrypted}");
        // }
    }
}