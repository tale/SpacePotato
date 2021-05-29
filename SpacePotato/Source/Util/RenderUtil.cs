using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public static class RenderUtil {
        
        public static void drawLineScreen(Vector2 from, Vector2 to, SpriteBatch spriteBatch, Color color, float thickness = 1) {
            var (dist, rot) = Util.magAngle(to - from);

            Vector2 tl = from + Util.rotate(Vector2.One * thickness * -0.5F, rot);
            
            Rectangle rect = new Rectangle((int) tl.X, (int) tl.Y, (int) (dist + thickness), (int) thickness);
            spriteBatch.Draw(Textures.rect, rect, null, color, rot, Vector2.Zero, SpriteEffects.None, 0F);
        }
        
    }
}