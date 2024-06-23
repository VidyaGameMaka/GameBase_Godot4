# Godot 4 C# GameBase for Godot 4.2.2 .NET  

An out-of-the box starting point for new Godot 4 C# games.  
Download the ZIP from this repository and extract it to where you want your game to be located.  
Rename the folder to a name that you desire then import the project.godot file into godot 4.  

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
Everything required to run this project is included in this repository.  Be sure to use the Mono Version of Godot 4.2.1 or better to use this project.  
1: Click on the green code button and download as a zip file. You may also use a GIT client to clone the repository if you desire but I recommend setting up your own repository for your own game.  
2: Choose a location on your computer where you will store your game projects such as C:\Godot4Projects. I recommend storing your Godot Project on another drive if possible and to back up your work using GIT.  
3: After downloading the ZIP file, right click and choose Extract All. Choose the default options and it will create a folder with the entire project inside of it.  You can rename the root folder (GameBase_Godot4) to anything that you like.  
4: Open Godot 4 then click the import button. Navigate to the folder that has project.godot in it. Then click Open then Import & Edit.  

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
* Project -> Assembly Name  (I recommend against changing this value as it may cause the project to no longer function without changes made to the new solution file.)  

# Usage (Save/Load/Delete)  

Player data has 3 save slots. This can be increased by editing GameMaster.cs  
An example of Save/Load/Delete is located in: res://GameBase/Prefabs/Menus/Scripts/SaveLoadDeleteMenu.cs  

## Save Game:  
The recommended method is to use: GameMaster.FullSave();  
This will save your game's system data (GameMaster.rGameData) as well as the current runtime data (GameMaster.rPlayerData)  

## Load Game:  
GameMaster.rGameData is automatically loaded when the game starts, no action is required to load this.  
To load rPlayerData for the current slot.  Example for Slot #1:  
* GameMaster.currentSlotNum = 1;  
* GameMaster.rPlayerData = GameMaster.loadedPlayerDataSlot1;  

## Delete Game:  
* GameMaster.DeletePlayerData(myInt);  
Call GameMaster.DeletePlayerData and replace myInt with the slot you want to erase.  
Note that GameMaster does not actually delete files from the filesystem. Instead it replaces the "deleted" slot with a blank copy of (res://GameBase/GameMaster/DataClasses/PlayerData.cs)  
