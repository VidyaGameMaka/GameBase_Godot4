using System.Collections.Generic;

namespace VGame {
    public enum Languages { english, spanish, french }

    public enum baseDicts {
        mainMenu = 10,
        audioMenu = 20,
        saveLoadMenu = 30,
        videoMenu = 40,
        languages = 50
    }

    public class Lng {

        #region Base Dictionaries (baseDicts)
        public static Dictionary<int, string> mainMenu = new Dictionary<int, string>();
        public static Dictionary<int, string> audioMenu = new Dictionary<int, string>();
        public static Dictionary<int, string> saveLoadMenu = new Dictionary<int, string>();
        public static Dictionary<int, string> videoMenu = new Dictionary<int, string>();
        public static Dictionary<Languages, string> languages = new Dictionary<Languages, string>();
        #endregion

        public void Run() => ImplementationRun();

        // Stub virtual method to be overriden in child classes
        protected virtual void ImplementationRun() { }
    }
}
