using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        // debug
        public Vector2 testPos = Vector2.Zero;
        public Texture2D testTexture;
        
        // basic stuff
        public Camera camera;
        
        // objects
        public Player player;
        public List<Planet> planets;
        
        
        public MainScreen(Game game, int screenId) : base(game, screenId) {
            testTexture = ContentManager.Load<Texture2D>("Images/Common/image");
            
            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            player = new Player(new Vector2(0, 0));

            const float expanse = 3000;
            planets = Enumerable.Range(0, 50).Select(i => new Planet(new Vector2(Util.random(-expanse, expanse), Util.random(-expanse, expanse)), 100)).ToList();
        }

        private static float delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Update(GameTime gameTime, KeyInfo keys) {
            float deltaTime = delta(gameTime);

            // update code

            player.Update(deltaTime, keys);
            camera.Position = player.pos - camera.Origin;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: camera.CalculateViewMatrix());
            
            // rendering code
            
            player.Draw(spriteBatch);
            foreach (var planet in planets) {
                planet.Draw(spriteBatch);
            }

            spriteBatch.Draw(testTexture, new Rectangle(500, 700, 100, 100), Color.White);
            
            // end
            spriteBatch.End();
        }
    }
}