using CalculateHash.Utilities;
using System;

namespace CalculateHash
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				string path = args[0];
				string hash = HashUtilities.GetMD5HashFromFile(path);

				Console.WriteLine("Hash: {0}", hash);
			}

			Console.WriteLine("Press any key to quit...");
			Console.ReadKey();
		}
	}
}
