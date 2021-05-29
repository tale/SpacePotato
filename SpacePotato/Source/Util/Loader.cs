using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato.Source.Util {
    public static class Loader {
        public static Texture2D texture(string relPath) {
            return GameScreen.publicContentManager.Load<Texture2D>($"Images/{relPath}");
        }
    }
}