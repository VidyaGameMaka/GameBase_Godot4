//Inherits from Lng. Spanish data file
public class spanish : Lng {

    protected override void ImplementationRun() {

        #region Base Dictionaries
        //Languages Dictionary is the same in all data sets
        languages[Languages.english] = "English";
        languages[Languages.spanish] = "Español";
        languages[Languages.french] = "Français";

        //Main Menu
        mainMenu[0] = "Comenzar";
        mainMenu[1] = "Audio";
        mainMenu[2] = "Video";
        mainMenu[3] = "Abandonar";
        mainMenu[4] = "Regresa";
        mainMenu[5] = "Idioma";

        //Audio Menu
        audioMenu[0] = "Maestro";
        audioMenu[1] = "Musica";
        audioMenu[2] = "Sonido o Sonidos";
        audioMenu[3] = "Voz";
        audioMenu[4] = "Masculino";
        audioMenu[5] = "Femenino";

        //Save/Load/Delete Menu
        saveLoadMenu[0] = "Nuevo";
        saveLoadMenu[1] = "Cargar";
        saveLoadMenu[2] = "Borrar";
        saveLoadMenu[3] = "Restablecer todos los datos";

        //Video Menu
        videoMenu[0] = "Ventana";
        videoMenu[1] = "Pantalla Completa";
        videoMenu[2] = "Resolucion";
        videoMenu[3] = "Aplicar";
        #endregion


    }

}