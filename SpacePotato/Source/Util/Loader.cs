using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public static class Loader {
        public static Texture2D texture(string relPath) {
            return GameScreen.publicContentManager.Load<Texture2D>($"Images/{relPath}");
        }
        public static SpriteFont font(string fontName) {
            return GameScreen.publicContentManager.Load<SpriteFont>($"Fonts/{fontName}");
        }
    }
}