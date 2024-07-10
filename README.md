# Godot 4 C# GameBase for Godot 4.2.1 or Higher .NET  
An out-of-the box starting point for new Godot 4 C# games.  
  
Click on the Green Code Button and Choose Download ZIP.  
Extract the downloaded zip file where you want your game to be located.  
Rename the folder to a name that you desire open Godot Editor and import the file: project.godot  

# Keep This in Mind  
This project is not perfect and it will probably never be perfect. After downloading this project and extracting it you will need to customize it for your own use. I did this as a starting point for those that intend to make a serious game with a full save system and a few other features that I always use in every game that I make. I've been fiddling with this GameBase project very much part time for over a year and have focused on making comments in the code and adding features that I want in my GameBase. So go ahead and take pieces of the code from this project and put them into your project however you need it to be. Make it your own.  

# This project offers the following features:  
* Save system for Game System options (gameData)  
* Automatically saves game and player data on quit button or from OS window manager  
* Multiple Slot Save System (playerData)  
* Multiple Channel Audio System (Master, Music, SFX, Voice, Male, Female)  
* Video system with resolution changing and fullscreen and windowed mode with automatic centering  
* Translation system with support for text and audio file translation  
* Includes example menus:  
  * Main Menu  
  * Video Menu  
  * Audio Menu  
  * Save / Load / Delete Game Menu  

# Installation  
Everything required to run this project is included in this repository.  Be sure to use the Mono Version of Godot 4.2.1 or newer to use this project.  
1. Click on the green code button and download as a zip file. You may also use a GIT client to clone the repository if you desire but I recommend setting up your own repository for your own game.  
2. Choose a location on your computer where you will store your game projects such as C:\Godot4Projects. I recommend storing your Godot Project on another drive if possible and to back up your work using GIT and another hard drive.  
3. After downloading the ZIP file, right click and choose Extract All. Choose the default options and it will create a folder with the entire project inside of it.  You can rename the root folder (GameBase_Godot4) to anything that you like.  
4. Open Godot 4 then click the import button. Navigate to the folder that has project.godot in it. Then click Open then Import & Edit.  

# Default New Project Changes  
Once this project has been setup on your computer. You can customize and change any settings as you desire. I set up this project mostly for 2D pixel art. But it can be used for any type of game.  
Open Project Settings by clicking on the Project Menu then on Project Settings.  
The following settings have been changed from the default Godot new project settings:  
* Application -> Config -> Name  
Change this to you game's name.  
* Main Scene  
Change this value if you want to use a different scene on start up.  
* Display -> Window
Multiple settings have been altered in the Window section. Altered values will have an undo button visible next to them.  
* Audio -> Buses  
This is the location of the default audio bus. This should not be changed unless you require to completely delete and redo the Audio Bus settings.  
* Rendering -> Textures -> Default Texture Filter  
I selected Nearest for pixel art. Change this value to suit the game you want to make.  
* Project -> Assembly Name  (I recommend against changing this value as it may cause the project to no longer function without adding the Newtonsoft.Json .csproj file.)  
If you make a new assembly or C# solution, delete the two old files first: GameBase.csproj & GameBase.sln then create a new C# assembly in Godot. After creating the new assembly add the following three lines above the last closing directive in your new .csproj file: </Project>  
```  
<ItemGroup>  
 <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />  
</ItemGroup>  
```  
Your .csproj file should look similar to the one in this project.  

# Usage (Save/Load/Delete)  

GameMaster has 3 Player data save slots by default. This can be increased by changing the value of totalSaveSlots in GameMaster.cs  
Changing the value in GameMaster.cs will not update the example menu, you will need to update your menu to function as required.  
An example menu for Save/Load/Delete is located in: res://GameBase/Prefabs/Menus/Scripts/SaveLoadDeleteMenu.cs  

## Save Game:  
The recommended method is to use: GameMaster.FullSave();  
This will save your game's system data (GameMaster.gameData) as well as the current runtime player data (GameMaster.playerData)  

## Load Game:  
GameMaster.gameData is automatically loaded when the game starts, no action is required to load this.  
To load playerData for the current slot.  Example for Slot #1:  

```  
//Set the current slot to the one sent by the argument  
GameMaster.currentSlotNum = myInt;  
  
//Load the slot sent by the argument  
GameMaster.playerData = GameMaster.loadedPlayerDataSlots[myInt];  
  
//Make this save slot as not a new file  
GameMaster.playerData.newFile = false;  
  
//Set Version of Save File  
GameMaster.playerData.saveFileVersion = GameMaster.gameVersion;  
  
//Save Everything  
GameMaster.FullSave();  
  
//Change Scene to start the game or anything else that you'd like to do.  
SceneManager.instance.ChangeScene(GameMaster.playerData.savedScene);  
  
```  

## Delete Game:  
* GameMaster.DeletePlayerData(myInt);  
Call GameMaster.DeletePlayerData and replace myInt with the slot you want to erase.  
Note that GameMaster does not actually delete files from the filesystem. Instead it replaces the "deleted" slot with a blank copy of (res://GameBase/GameMaster/DataClasses/PlayerData.cs)  

## Where are the various settings stored?  
* GameBase/Scripts/Main/DataClasses  
* GameData.cs - Contains game system settings such as: resolution, window size, audio volumes and language choice  
* PlayerData.cs - Contains the default save file for the players game, includes: scenes and checkpoints  
* SceneData.cs - Contains information about the scenes in your game including: Index: Scene Enum - Pretty Scene Name, pauseAllowed, saveAllowed, Scene Path, Music Path  