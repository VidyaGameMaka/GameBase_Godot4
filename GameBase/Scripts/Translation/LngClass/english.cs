//Inherits from Lng. English data file

public class english : Lng {

    protected override void ImplementationRun() {

        #region Base Dictionaries
        //Languages Dictionary is the same in all data sets
        languages[Languages.english] = "English";
        languages[Languages.spanish] = "Español";
        languages[Languages.french] = "Français";


        //Main Menu
        mainMenu[0] = "Start";
        mainMenu[1] = "Audio";
        mainMenu[2] = "Video";
        mainMenu[3] = "Quit";
        mainMenu[4] = "Back";
        mainMenu[5] = "Language";

        //Audio Menu
        audioMenu[0] = "Master";
        audioMenu[1] = "Music";
        audioMenu[2] = "SFX";
        audioMenu[3] = "Voice";
        audioMenu[4] = "Male";
        audioMenu[5] = "Female";

        //Save/Load/Delete Menu
        saveLoadMenu[0] = "New";
        saveLoadMenu[1] = "Load";
        saveLoadMenu[2] = "Delete";
        saveLoadMenu[3] = "Reset all Data";

        //Video Menu
        videoMenu[0] = "Windowed";
        videoMenu[1] = "Full Screen";
        videoMenu[2] = "Resolution";
        videoMenu[3] = "Apply";
        #endregion


    }

}
