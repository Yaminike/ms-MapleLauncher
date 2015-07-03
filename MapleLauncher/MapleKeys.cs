using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MapleLauncher
{
	public static class MapleKeys
    {
        private static Dictionary<Localisation, Dictionary<KeyValuePair<ushort, byte>, byte[]>> Keys { get; set; }

        public static void Initialize()
        {
            MapleKeys.Keys = new Dictionary<Localisation, Dictionary<KeyValuePair<ushort, byte>, byte[]>>();

            try
            {
                if (File.Exists("noupdate.txt"))
                {
                    throw new Exception(); // NOTE: Trigger offline-load.
                }

                HttpWebRequest req = WebRequest.Create("http://direct.craftnet.nl/app_updates/get_keys.php?source=SRK&version=4") as HttpWebRequest;

                req.Proxy = null;

                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        string text = sr.ReadToEnd();

                        MapleKeys.Load(text);

                        File.AppendAllText("MapleKeys.txt", text);
                    }
                }
            }
            catch (Exception e)
            {
                if (File.Exists("MapleKeys.txt"))
                {
                    string text = File.ReadAllText("MapleKeys.txt");

                    MapleKeys.Load(text);
                }
                else
                {
                    throw new Exception("Unable to initialize MapleKeys.");
                }
            }

            MapleKeys.Add(Localisation.Global, 118, 1, new byte[] {
                0x5A, // Full key's lost
                0x22, 
                0xFB, 
                0xD1, 
                0x8F, 
                0x93, 
                0xCD, 
                0xE6, 
            });
        }

        private static void Load(string pContents)
        {
            string[] lines = pContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < lines.Length; i += 2)
            {
                var firstLine = lines[i];
                var semicolonPos = firstLine.IndexOf(':');
                var dotPos = firstLine.IndexOf('.');

                Localisation localisation = (Localisation)(byte.Parse(firstLine.Substring(0, semicolonPos)));
                ushort version = ushort.Parse(firstLine.Substring(semicolonPos + 1, dotPos - (semicolonPos + 1)));
                byte subVersion = byte.Parse(firstLine.Substring(dotPos + 1));

                string tmpkey = lines[i + 1];
                byte[] realkey = new byte[8];
                int tmp = 0;

                for (int j = 0; j < 4 * 8 * 2; j += 4 * 2)
                {
                    realkey[tmp++] = byte.Parse(tmpkey[j] + "" + tmpkey[j + 1], System.Globalization.NumberStyles.HexNumber);
                }

                MapleKeys.Add(localisation, version, subVersion, realkey);
            }
        }

        private static void Add(Localisation localisation, ushort version, byte subVersion, byte[] key)
        {
            if (!MapleKeys.Keys.ContainsKey(localisation))
            {
                MapleKeys.Keys.Add(localisation, new Dictionary<KeyValuePair<ushort, byte>, byte[]>());
            }

            MapleKeys.Keys[localisation].Add(new KeyValuePair<ushort, byte>(version, subVersion), key);
        }

        public static byte[] Get(ServerInfo info)
        {
            if (MapleKeys.Keys == null)
            {
                MapleKeys.Initialize();
            }

            if (!MapleKeys.Keys.ContainsKey(info.Localisation))
            {
                return null;
            }

            byte subVersion = byte.Parse(info.Subversion);

            for (; info.Version > 0; info.Version--)
            {
                for (byte b = subVersion; b > 0; b--)
                {
                    var container = new KeyValuePair<ushort, byte>(info.Version, b);

                    if (MapleKeys.Keys[info.Localisation].ContainsKey(container))
                    {
                        byte[] key = MapleKeys.Keys[info.Localisation][container];
                        byte[] ret = new byte[32];

                        for (int i = 0; i < 8; i++)
                        {
                            ret[i * 4] = key[i];
                        }

                        return ret;
                    }
                }
            }

            return null;
        }
    }
}