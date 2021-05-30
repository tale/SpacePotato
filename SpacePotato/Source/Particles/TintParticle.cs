using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class TintParticle : Particle {

        public Color color;

        public TintParticle(Color color, Vector2 pos, Vector2 vel) : base(pos, vel) {
            this.color = color;
        }
        
        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), color);
        }
    }
}