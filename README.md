InnerCore.Api.DeConz
====================

Open source library for communication with the Dresden Elektronik DeConz bridge.

As the DeConz api is very similar to the Philips Hue api, its usage should be similar too.
This library is therefore adapted from the Q42.HueApi so the usage is also very simlar.

Note however, that
 - not all features of the Philips Hue api are supported as they are not supported by DeConz either
 - some objects might be missing some properties/features if they are not supported by DeConz
 - some objects have additional properties/features specific to DeConz

 requirements:
  - dotnet core 2.2
  - DeConz 2.05.54 or later (earlier versions might work too)

## State
The project is currently in it's alpha state:
 - not all features have been tested (especially scenes, rules and schedules)
 - there might be further missing features/properties and some properties that are not supported by DeConz might still be there
 - a NuGet package is not yet available

## How to use?
Some basic usage examples -> you might also check [Q42.HueApi on GitHub](https://github.com/Q42/Q42.HueApi).

### Bridge/Gateway
Before you can communicate with the DeConz gateway, you need to find the bridge and register your application:

	IBridgeLocator locator = new HttpBridgeLocator();
	IEnumerable<LocatedBridge> bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
	
Register your application
	
	// use the ip and port retrieved from the discovery
	DeConzClient client = new DeConzClient("ip");
	var appKey = await client.RegisterAsync("mypersonalappname");
	//Save the app key for later use
	
If you already registered an appname, you can initialize the DeConzClient with the app's key:	

	client.Initialize("mypersonalappkey");

### Control the lights
Main usage of this library is to be able to control your lights. We use a LightCommand for that. A LightCommand can be send to one or more / multiple lights. A LightCommand can hold a color, effect, on/off etc.

	var command = new LightCommand();
	command.On = true;
	
There are some helpers to set a color on a command:
	
	//Turn the light on and set a Hex color for the command (see the section about Color Converters)
    command.TurnOn().SetColor(new RGBColor("FF00AA"))
	
LightCommands also support Effects and Alerts

	//Blink once
	command.Alert = Alerts.Once;
	
	//Or start a colorloop
	command.Effect = Effects.ColorLoop;
	
Once you have composed your command, send it to one or more lights

	client.SendCommandAsync(command, new List<string> { "1" });
	
Or send it to all lights

	client.SendCommandAsync(command);

### Open Source Project Credits

* adapted from Q42.HueApi
* Newtonsoft.Json is used for object serialization

## License

InnerCore.Api.DeConz is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/MadMonkey87/InnerCore.Api.DeConz/blob/master/LICENSE.txt) for more information.

## Contributions

Contributions are welcome. Fork this repository and send a pull request if you have something useful to add.

[![Build status](https://innercore.visualstudio.com/InnerCore.Api.DeConz/_apis/build/status/InnerCore.Api.DeConz?branchName=master)](https://innercore.visualstudio.com/InnerCore.Api.DeConz/_apis/build/status/InnerCore.Api.DeConz)


## Related Projects

* [Official DeConz description](https://www.dresden-elektronik.de/funktechnik/products/software/pc/DeConz/)
* [Official DeConz api documentation](https://dresden-elektronik.github.io/DeConz-rest-doc/)