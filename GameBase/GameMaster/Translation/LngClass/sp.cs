//Inherits from Lng. Spanish data file
public class sp : Lng {

    protected override void ImplementationRun() {

        #region Tester
        tester = "Prueba de espanol";
        #endregion

        #region Base Dictionaries
        //Main Menu
        mainMenu[0] = "Comenzar";
        mainMenu[1] = "Audio";
        mainMenu[2] = "Video";
        mainMenu[3] = "Abandonar";
        mainMenu[4] = "Regresa";

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

        #region Example Dialogue
        exampleDialogue[0] = new DiaCstr("Este es un ejemplo de dialogo.", "res://GameBase/Audio/Music/blanksound.wav");
        exampleDialogue[1] = new DiaCstr("Puede especificar el texto para cada idioma.", "res://GameBase/Audio/Music/blanksound.wav");
        exampleDialogue[2] = new DiaCstr("Asi como un archivo de voz diferente para cada idioma.", "res://GameBase/Audio/Music/blanksound.wav");
        #endregion
    }

}