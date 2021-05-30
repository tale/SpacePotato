using System;
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
        public static Camera Camera;
        
        // particles
        public static ParticleSystem particlesOver, particlesUnder;

        // input
        public static MouseInfo CurrentMouse;
        public static KeyInfo CurrentKeys;

        // settings and booleans
        public static bool EditMode;

        // level stuff
        private static Level[] _levels;
        public static Level level => _levels[_currLevel];

        private static int _currLevel = 0;


        public MainScreen(Game game, int screenId) : base(game, screenId) {

            Camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            RecreatePlayer();

            var level1Planets = Enumerable.Range(0, 50).
                Select(i => new Planet(new Vector2(Util.random(-100, 5900),
                    Util.random(-1000, 1000)), 100)).ToList();

            _levels = new[] {
                new Level(level1Planets, new Rectangle(-100, -1000, 6000, 2000), 1),
            };

            particlesOver = new ParticleSystem();
            particlesUnder = new ParticleSystem();
        }

        private static float Delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static List<Planet> GetPlanets() {
            return level.Planets;
        }

        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = Delta(gameTime);
            CurrentKeys = keys;
            CurrentMouse = mouse;

            // update code
            _player.Update(deltaTime, keys, mouse);
            Camera.Position = _player.pos - Camera.Origin;


            particlesOver.Update(deltaTime);
            particlesUnder.Update(deltaTime);
            
            if (keys.pressed(Keys.O)) {
                EditMode ^= true;
            }

            if (keys.pressed(Keys.K)) {
                GetPlanets().Clear();
            }

            if (keys.pressed(Keys.P)) {
                int ID = Util.randInt(100, 10000);
                DataSerializer.Serialize($"LevelFileTest", new Level(GetPlanets(), level.Bounds, ID));
            }

            if (keys.pressed(Keys.L)) {
                _levels[_currLevel] = DataSerializer.Deserialize<Level>("LevelFileTest");
                level.setUpSerialized();
            }
        }

        public override void Render(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: Camera.CalculateViewMatrix());

            // rendering code
            particlesUnder.Render(spriteBatch);
            
            _player.Render(spriteBatch);

            _levels[0].Render(gameTime, spriteBatch);

            particlesOver.Render(spriteBatch);
            // end
            spriteBatch.End();
        }
    }
}
