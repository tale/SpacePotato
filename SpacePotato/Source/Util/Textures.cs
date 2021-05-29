using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public static class Textures {

        public static Texture2D rect = genRect(Color.White);
        
        public static Texture2D genRect(Color rectColor) {
            Texture2D rect = new Texture2D(SpacePotatoGame.getGraphicsDevice(), 1, 1);
            rect.SetData(new[] {rectColor});
            return rect;
        }
    }
}