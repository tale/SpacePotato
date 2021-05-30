using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public abstract class Component {
        public abstract void Render(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
