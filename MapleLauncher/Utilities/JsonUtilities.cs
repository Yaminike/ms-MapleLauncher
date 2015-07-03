using Newtonsoft.Json;
using System.IO;

namespace MapleLauncher.Utilities
{
	public static class JsonUtilities
	{
		public static string Serialize(object T)
		{
			return JsonConvert.SerializeObject(T, Formatting.Indented);
		}

		public static T Deserialize<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}
	}
}
