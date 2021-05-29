using System.Collections.Generic;
using System.Linq;
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
        
        // settings and bools
        public static bool editMode;
        
        // objects
        public Player player;
        
        public Level level;
        public static List<Planet> planets;
        public Rectangle bounds;


        public MainScreen(Game game, int screenId) : base(game, screenId) {
            testTexture = ContentManager.Load<Texture2D>("Images/Common/image");
            
            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            player = createPlayer();

            const float expanse = 3000;
            planets = Enumerable.Range(0, 50).Select(i => new Planet(new Vector2(Util.random(-expanse, expanse), Util.random(-expanse, expanse)), 100)).ToList();
        }

        public Player createPlayer() {
            return new Player(new Vector2(0, 0));
        }

        private static float delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void loadLevel(Level level) {
            this.level = level;
            planets = level.planets.ToList();
            bounds = level.bounds;
        }

        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = delta(gameTime);

            // update code

            if (player.dead) player = createPlayer();
            
            player.Update(deltaTime, keys, mouse);
            camera.Position = player.pos - camera.Origin;


            if (keys.pressed(Keys.O)) {
                editMode ^= true;
            }

            if (keys.pressed(Keys.L)) {
                planets.Clear();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: camera.CalculateViewMatrix());
            
            // rendering code
            
            foreach (var planet in planets) {
                planet.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            spriteBatch.Draw(testTexture, new Rectangle(500, 700, 100, 100), Color.White);
            
            // end
            spriteBatch.End();
        }
    }
}