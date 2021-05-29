using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        private static Player _player;
        public static void RecreatePlayer() {
            _player = new Player(new Vector2(0, 0));
        }


        // basic stuff
        public static Camera camera;

        // input
        public static MouseInfo currentMouse;
        public static KeyInfo currentKeys;
        private static Camera _camera;

        // settings and bools
        public static bool editMode;

        public Level level;
        public static List<Planet> planets;
        public Rectangle bounds;


        public MainScreen(Game game, int screenId) : base(game, screenId) {
            testTexture = ContentManager.Load<Texture2D>("Images/Common/image");

            RecreatePlayer();
            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);
            const float expanse = 3000;
            planets = Enumerable.Range(0, 50).Select(i => new Planet(new Vector2(Util.random(-expanse, expanse), Util.random(-expanse, expanse)), 100)).ToList();
        }

        private static float delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static List<Planet> GetPlanets() {
            return _levels[_currLevel].Planets;
        }

        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = Delta(gameTime);
            currentKeys = keys;
            currentMouse = mouse;

            // update code
            _player.Update(deltaTime, keys, mouse);
            camera.Position = _player.pos - camera.Origin;

            if (keys.pressed(Keys.O)) {
                editMode ^= true;
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: _camera.CalculateViewMatrix());

            // rendering code

            foreach (var planet in planets) {
                planet.Draw(spriteBatch);
            }

            _player.Draw(spriteBatch);
            spriteBatch.Draw(testTexture, new Rectangle(500, 700, 100, 100), Color.White);

            // end
            spriteBatch.End();
        }
    }
}
