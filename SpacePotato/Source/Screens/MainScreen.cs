using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        public Vector2 testPos = Vector2.Zero;
        public Texture2D player;
        public Camera camera;
        
        public MainScreen(Game game, int screenId) : base(game, screenId) {
            player = ContentManager.Load<Texture2D>("Images/Common/image");
            
            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);
        }

        private static float delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Update(GameTime gameTime, KeyInfo keys) {
            float deltaTime = delta(gameTime);

            // update code
            if (keys.down(Keys.Space)) testPos += Vector2.One * 500 * deltaTime;
            camera.Position = testPos;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap,
                transformMatrix: camera.CalculateViewMatrix());
            
            // rendering code

            spriteBatch.Draw(player, new Rectangle((int)testPos.X, (int)testPos.Y, 100, 100), Color.White);
            spriteBatch.Draw(player, new Rectangle(200, 200, 100, 100), Color.White);
            
            
            // end
            spriteBatch.End();
        }
    }
}