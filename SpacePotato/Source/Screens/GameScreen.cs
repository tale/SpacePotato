using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public abstract class GameScreen {
        public readonly int ScreenId;
        protected ContentManager ContentManager;

        public static ContentManager publicContentManager;

        protected GameScreen(Game game, int screenId) {
            ContentManager = new ContentManager(game.Services) { RootDirectory = game.Content.RootDirectory };
            publicContentManager = ContentManager;
            ScreenId = screenId;
        }

        public abstract void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse);

        public virtual void Render(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp);
            
            BasicDraw(gameTime, spriteBatch);
            
            spriteBatch.End();
        }
        
        public virtual void BasicDraw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
