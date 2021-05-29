using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public abstract class GameScreen {
        public readonly int ScreenId;
        protected ContentManager ContentManager;

        protected GameScreen(Game game, int screenId) {
            ContentManager = new ContentManager(game.Services) { RootDirectory = game.Content.RootDirectory };
            ScreenId = screenId;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
