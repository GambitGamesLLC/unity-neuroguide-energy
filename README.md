# unity-neuroguide-energy
Unity3D project that utilizes the NeuroGuide hardware to show a visualizer experience.  

In this experience, a series of block pieces form into a cube.  

As the user is in a 'success' state while using the NeuroGuide hardware, the cube forms.  
When the user is not in a success state, the cube splits apart.  

<img width="512" height="284" alt="image" src="https://github.com/user-attachments/assets/eeec3680-c4a0-4640-876e-23402dc550a5" />


---

## PLAY INSTRUCTIONS

- Open `Scenes/Experience 2 - M3D`
- Press Play in the editor
- Use your keyboard up and down keys to fake the UDP data being sent from the NeuroGuide software

---  

## BUILD INSTRUCTIONS

- No special build instructions, simply make a Windows desktop build

---  

## PROCESS COMMAND LINE VALUE INSTRUCTIONS

NeuroGuide experiences like `Building Blocks` can have their settings variables passed in by the NeuroGuide Launcher process.

- When this project is launched normally, without being started from the NeuroGuide launcher, it will use variables setup within the Unity editor.
- These default variables are located on the `Main` component, present in the only scene used by this app.
- The `ProcessManager` package will pass in variables that will replace these defaults from the NeuroGuide Launcher, which uses the `ConfigManager` json system
- These variables are passed into this project using Windows process command line arguments, but by using the `ProcessManager` we can easily send and recieve these values
- The `NeuroGuide Launcher` application itself uses the `ConfigManager` to allow the variables to be set dynamically

## CONFIGURATION FILE INSTRUCTIONS

You can find the appropriate `configuration json` file within the Resources folder of the `NeuroGuide Launcher app`. 
This configuration file only exists as part of that repository and is not stored in this one.

- A `configuration json` file is stored in our Resources folder of the project, and can be updated to modify the application  
- This `configuration json` file is copied to our `%LOCALAPPDATA%` folder, specifically in the path specified in the `config:path` object  
- If there already exists a `configuration json` at the specified path, we will compare it against the one in the Resources folder. If the local file is out of date or missing, it will be written using the version in Resources.

- Locate and open the configuration json file within the resources folder, which has contents similar to this
```json
{
	"config": {
		"version": 2,
		"timestamp": "2025-07-28 12:00:00",
		"path": "%LOCALAPPDATA%\\M3DVR\\Launcher\\Energy.json"
	},
	"app": {
		"name": "Energy",
		"path": "%LOCALAPPDATA%\\M3DVR\\Energy\\Energy.exe",
		"length": 6,
		"debug": true,
		"logs": false,
		"threshold": 0.9
	}
}
```
  
<b>`config` OBJECT  </b>
- `version` - Defines the version number of the configuration file, used to see if this is newer than a config file we're comparing against.  
- `timestamp` - If the version of both config files matches, we check this timestamp to see if one is newer.  
- `path` - The path to the config file on local storage. This path has its environment variables expanded and is deserialized, so it can be used for normal Path operations in Unity.  
  
<b>`app` OBJECT  </b>
- `name` - Used by external software like the M3DVR Neuroguide launcher app to show the app name in a human readable format  
- `path` - The path to the executable for this project. Like other stored Path variables, this will have any environment variables expanded and will be deserialized.  
- `length` - How long should this experience last (in seconds) if the user was in a "success" state the entire time?
- `debug` - Do we want to enable debug mode for this app? This will fake incoming UDP port traffice as if the NeuroGuide Software was sending us messages
- `logs` - Do we want Unity console logs to be printed?  
- `threshold` - Normalized 0-1 value representing how far into the experience you need to be before triggering the reward state of the app. EX: For 0.9, that would be 90% into the experience.

---  

## DEPENDENCIES

Relies on several `Unity Asset Store` plugins as well as Open Source `Gambit Games` packages  

Please make sure the proper `scripting define symbols` and packages are imported into your project.  
This should happen automatically when opening this repo in Unity3D thanks to the package manager.  

Check the package repos directly for their `scripting define symbols`, `namespaces` and guides.  

- `DoTween` [Gambit Repo](https://github.com/GambitGamesLLC/unity-plugin-dotween) | [Unity Asset Store Link](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)  
- Used to perform tweens  

- `TotalJSON` [Gambit Repo](https://github.com/GambitGamesLLC/unity-plugin-totaljson) | [Unity Asset Store Link](https://assetstore.unity.com/packages/tools/input-management/total-json-130344)  
- Used for JSON manipulation  
  
- `Gambit Configuration Manager` [Gambit Repo](https://github.com/GambitGamesLLC/unity-config-manager.git?path=Assets/Plugins/Package)  
- Used for manipulation, saving, and loading of `.json` config files  
  
- `Math Helper` [Gambit Repo](https://github.com/GambitGamesLLC/unity-math-helper)  
- Contains convenience functions for math functionality, such as Map(), which converts one value in a range to another  
  
- `NeuroGuide Manager` [Gambit Repo](https://github.com/GambitGamesLLC/unity-neuroguide-manager.git)  
- Reads data from the NeuroGuide Software via UDP ports  
  
- `Singleton Manager` [Gambit Repo](https://github.com/GambitGamesLLC/unity-singleton)
- Convenience function to easily create global singletons that retain Unity Lifecycle functionality such as a GameObject Instance
