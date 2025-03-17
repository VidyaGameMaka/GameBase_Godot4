using Godot;
using System.IO;

namespace VGame {

    public partial class ScreenTools : Node {

        public void SaveScreenShot(int slotNum) {
            var screenshot_path = "user://screenshots/screenshot" + slotNum + ".png";
            var image = GetViewport().GetTexture().GetImage();
            image.SavePng(screenshot_path);
            GD.Print("Saved to: " + ProjectSettings.GlobalizePath(screenshot_path));
        }

        public Texture2D LoadScreenShot(int slotNum) {
            var screenshot_path = "user://screenshots/screenshot" + slotNum + ".png";
            try {
                //Load file from user's folder and return to caller
                var texture = GD.Load<Texture2D>(screenshot_path);
                return texture;
            }
            catch {
                //If file fails to load for any reason, use the godot logo
                var texture = GD.Load<Texture2D>("res://GameBase/Sprites/icon.svg");
                return texture;
            }
        }

        public void DeleteScreenShot(int slotNum) {
            var screenshot_path = "user://screenshots/screenshot" + slotNum + ".png";
            var fullPath = ProjectSettings.GlobalizePath(screenshot_path);
            if (File.Exists(fullPath)) {
                File.Delete(fullPath);
            }
        }

    }
}