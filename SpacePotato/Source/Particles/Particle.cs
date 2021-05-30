using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class Particle {
        
        public Vector2 pos, vel, dimen;
        public Texture2D texture;
        public float timeLeft;
        public bool delete;

        public Particle(Vector2 pos, Vector2 vel) {
            texture = Textures.rect;
            dimen = Vector2.One * 8;

            this.pos = pos;
            this.vel = vel;
        }

        public virtual void Update(float deltaTime) {
            pos += vel * deltaTime;
            timeLeft -= deltaTime;
            if (timeLeft <= 0) delete = true;
        }

        public virtual void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), Color.White);
        }
    }
}