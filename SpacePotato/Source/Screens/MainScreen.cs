using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        // debug
        public Vector2 testPos = Vector2.Zero;
        public Texture2D testTexture;
        
        // basic stuff
        public Camera camera;
        
        // objects
        public Player player;
        
        
        public MainScreen(Game game, int screenId) : base(game, screenId) {
            testTexture = ContentManager.Load<Texture2D>("Images/Common/image");
            
            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            player = new Player(new Vector2(500, 700));
        }

        private static float delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Update(GameTime gameTime, KeyInfo keys) {
            float deltaTime = delta(gameTime);

            // update code
            if (keys.down(Keys.Space)) player.pos += Vector2.One * 500 * deltaTime;
            camera.Position = player.pos - camera.Origin;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap,
                transformMatrix: camera.CalculateViewMatrix());
            
            // rendering code
            
            player.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(testTexture, new Rectangle(500, 700, 100, 100), Color.White);

            
            // end
            spriteBatch.End();
        }
    }
}