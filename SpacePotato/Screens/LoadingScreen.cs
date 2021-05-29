using SpacePotato.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato.Screens {
    public class LoadingScreen : GameScreen {
        private readonly Spinner _spinner;
        private readonly SpriteFont _titleFont;

        public LoadingScreen(Game game, int screenId) : base(game, screenId) {
            _titleFont = ContentManager.Load<SpriteFont>("Fonts/Title");
            _spinner = new Spinner(ContentManager);
        }

        public override void Update(GameTime gameTime) {
            _spinner.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            var (x, _) = _titleFont.MeasureString("SpacePotato");
            var viewport = spriteBatch.GraphicsDevice.Viewport;

            _spinner.Draw(gameTime, spriteBatch, new Vector2(viewport.Width - 40F, viewport.Height - 40F), 0.1F);
            spriteBatch.DrawString(_titleFont, "SpacePotato", new Vector2(viewport.Width / 2F, viewport.Height / 4F),
                Color.White, 0, new Vector2(x / 2, 0), Vector2.One, SpriteEffects.None, 0);
        }
    }
}
