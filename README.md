# ms-MapleLauncher
A custom launcher that sends hash checksum of the data files and acts a redirector.

### Configuration
In order to run this launcher, you must have a configuration file. Here's an example configuration file:

```
{
  "MapleStoryPath": "E:\\MapleStory\\GMS\\v.162.3\\"
}
```

### How To Setup

1. Clone the Git repository to a local folder on your machine.
2. Open the project using Visual Studio 2013 or 2015.
3. Add a reference to Newtonsoft.Json.dll.
4. Edit Program.cs to your liking. Here's a list of the values to know about:
Name: The name of your server. This will show up in some places
*IP*: The IP Address of your server to connect to.
*Port*: The Port of your server to connect to.
*ServerIP*: The operation code of the ServerIP packet. That's usualy 0x000B, so you don't have to modify this.
*MaskIP*: Should the program mask the IP Address? In case you have a localhost that connects to 127.0.0.1, you can leave this disabled.
*MaskedIP*: The IP Address to mask. That's mostly for higher-version servers, where the client connects to Neckson's servers.
*ClientName*: The name of the client (localhost.exe, MapleStory.exe, etcetera).
*Website*: The link to your website (used for the tray icon).
*Forums*: The link to your forums (used for the tray icon).
*ConfigName*: The path to the configuration file (config.json as default).
*UseMapleEncryption*: Should the launcher encrypt data with the MapleStory custom encryption? (False for higher versions, true for lower).
5. Edit the frmMain's design to your liking.
6. Build the application.
7. ???
8. PROFIT!

### How To Calculate Hash?

Included in the solution is an application called "CalculateHash" which will return the calculated hash checksum of a file. Simply drag the .wz file on it and it will display it's hash checksum!

### Handling The Packet

Handling the custom packet is quite easy. Here's a small example to help you get started!
(Sorry for my rusty Java skills):

```
case WZ_CHECKSUM:
	Map<String, String> wzFiles = new Map<String, String>();
	wzFiles.put("Base", "1");
	wzFiles.put("Character", "2");
	wzFiles.put("Effect", "3");
	wzFiles.put("Etc", "4");
	wzFiles.put("Item", "5");
	wzFiles.put("Map", "6");
	wzFiles.put("Mob", "7");
	wzFiles.put("Morph", "8");
	wzFiles.put("Npc", "9");
	wzFiles.put("Quest", "10");
	wzFiles.put("Reactor", "11");
	wzFiles.put("Skill", "12");
	wzFiles.put("Sound", "13");
	wzFiles.put("String", "14");
	wzFiles.put("TamingMob", "15");
	wzFiles.put("UI", "16");
	};

	while (slea.Available() >= 0) {
		String name = slea.readMapleString();
		String hash = slea.readMapleString();

		String computedHash = map.get(name);

		if (hash != computedHash) {
			c.disconnect();

			break;
		}
	}
	
	break;
```
