using System;
using System.IO;
using System.Security.Cryptography;

namespace ET
{
	public static class MD5Helper
	{
		public static string FileMD5(string filePath)
		{
			byte[] retVal;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
	            MD5 md5 = MD5.Create();
				retVal = md5.ComputeHash(file);
			}
			return retVal.ToHex("x2");
		}

		public static string GenerateStringID()
		{
			long i = 1;
			foreach (byte b in Guid.NewGuid().ToByteArray())
			{
				i *= ((int)b + 1);
			}
			return string.Format("{0:x}", i - DateTime.Now.Ticks);
		}

		public static long GenerateIntID()
		{
			byte[] buffer = Guid.NewGuid().ToByteArray();
			return BitConverter.ToInt64(buffer, 0);
		}

		public static long GenerateIntID(string key)
		{
			byte[] buffer = key.ToByteArray();
			return BitConverter.ToInt64(buffer, 0);
		}
	}
}
