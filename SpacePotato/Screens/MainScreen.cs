using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato.Screens {
    public class MainScreen : GameScreen {
        
        public Texture2D player;
        
        public MainScreen(Game game, int screenId) : base(game, screenId) {
            player = ContentManager.Load<Texture2D>("Images/Common/image");
        }

        public override void Update(GameTime gameTime) {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            
            spriteBatch.Draw(player, new Rectangle(0, 0, 100, 100), Color.White);
            
        }
    }
}