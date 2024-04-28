InnerCore.Api.DeConz [![Build Status][azure build]][project]	[![NuGet][nuget badge]][nuget package]	  [![.NET Standard][dotnet-standard badge]][dotnet-standard doc]
====================

Open source library for communication with the Dresden Elektronik DeConz bridge.

As the DeConz api is very similar to the Philips Hue api, its usage should be similar too.
This library is therefore adapted from the Q42.HueApi so the usage is also very simlar.

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
    command.TurnOn().SetColor(new RGBColor("FF00AA"));
	
LightCommands also support Effects and Alerts

	//Blink once
	command.Alert = Alerts.Once;
	
	//Or start a colorloop
	command.Effect = Effects.ColorLoop;
	
Once you have composed your command, send it to one or more lights

	client.SendCommandAsync(command, new List<string> { "1" });
	
Or send it to all lights

	client.SendCommandAsync(command);

### Reading Sensor events in realtime

DeConz allows you to read sensor events in realtime using websockets (this is not supported on Philips Hue however). After you created a DeConConz client
you can register for sensor events:

	client.SensorChanged += Client_SensorChanged;

    ...

	private static void Client_SensorChanged(object sender, Models.WebSocket.SensorChangedEvent e)
	{
        // handle the event here
        // e.Id: the id of the sensor
        // e.state: the current state of the sensor if it has changed
        // e.config: the current config of the sensor if it has changed
	}

After that you can start listening to events

    await client.ListenToEvents();

You then will get events as long as the ListenToEvents task has not completed (i.e. when the server connection is lost).

It is also possible to provide a cancellation token in case you wan't to close the connection manually:

    var cancellationTokenSource = new CancellationTokenSource();
    var task = client.ListenToEvents(cancellationTokenSource.Token);

    cancellationTokenSource.Cancel();

## How To install?
Download the source from GitHub or get the compiled assembly from NuGet [InnerCore.Api.DeConz on NuGet](https://nuget.org/packages/InnerCore.Api.DeConz).

### Open Source Project Credits
* adapted from Q42.HueApi
* Newtonsoft.Json is used for object serialization

## License

InnerCore.Api.DeConz is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/MadMonkey87/InnerCore.Api.DeConz/blob/master/LICENSE.txt) for more information.

## Contributions

Contributions are welcome. Fork this repository and send a pull request if you have something useful to add.

## Related Projects

* [Official DeConz description](https://www.dresden-elektronik.de/funktechnik/products/software/pc/DeConz/)
* [Official DeConz api documentation](https://dresden-elektronik.github.io/DeConz-rest-doc/)

[azure build]: https://innercore.visualstudio.com/InnerCore.Api.DeConz/_apis/build/status/InnerCore.Api.DeConz?branchName=master
[project]: https://github.com/MadMonkey87/InnerCore.Api.DeConz
[nuget badge]: https://img.shields.io/nuget/v/InnerCore.Api.DeConz.svg
[nuget package]: https://www.nuget.org/packages/InnerCore.Api.DeConz
[dotnet-standard badge]: http://img.shields.io/badge/.NET_Standard-v2.1-green.svg
[dotnet-standard doc]: https://docs.microsoft.com/da-dk/dotnet/articles/standard/library