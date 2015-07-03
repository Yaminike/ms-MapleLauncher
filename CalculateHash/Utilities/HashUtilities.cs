using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CalculateHash.Utilities
{
	internal static class HashUtilities
	{
		public static string GetMD5HashFromFile(string path)
		{
			FileStream file = new FileStream(path, FileMode.Open);
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(file);
			file.Close();

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < retVal.Length; i++)
			{
				sb.Append(retVal[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}
}
