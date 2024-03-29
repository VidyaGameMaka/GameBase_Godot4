using System.Collections.Generic;

public enum Languages { en,  sp }

public enum baseDicts { 
    mainMenu = 10, 
    audioMenu = 20, 
    saveLoadMenu = 30, 
    videoMenu = 40, 
    languages = 50,
}

public class Lng {

    #region Base Dictionaries (baseDicts)
    public static string tester = string.Empty; //For testing, do not delete.

    public static Dictionary<int, string> mainMenu = new Dictionary<int, string>();
    public static Dictionary<int, string> audioMenu = new Dictionary<int, string>();
    public static Dictionary<int, string> saveLoadMenu = new Dictionary<int, string>();
    public static Dictionary<int, string> videoMenu = new Dictionary<int, string>();
    public static Dictionary<int, string> languages = new Dictionary<int, string>();
    public static Dictionary<int, DiaCstr> exampleDialogue = new Dictionary<int, DiaCstr>();
    #endregion

    public virtual void Run() => ImplementationRun();

    // Stub virtual method to be overriden by child classes
    // En.cs and Sp.Cs
    protected virtual void ImplementationRun() {

        //Languages Dictionary is the same in all data sets
        languages[0] = "English";
        languages[1] = "Espanol";
    
    }
}
