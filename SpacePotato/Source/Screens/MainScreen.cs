using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        
        // basic stuff
        public static Camera camera;
        
        // input
        public static MouseInfo currentMouse;
        public static KeyInfo currentKeys;
        
        // settings and booleans
        public static bool editMode;

        // objects
        private static Player player;
        
        private static Level[] _levels;
        public static Level level => _levels[_currLevel];

        private static int _currLevel = 0;


        public MainScreen(Game game, int screenId) : base(game, screenId) {

            camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);
            player = CreatePlayer();

            var level1Planets = Enumerable.Range(0, 50).
                Select(i => new Planet(new Vector2(Util.random(-100, 5900), 
                    Util.random(-1000, 1000)), 100)).ToList();

            _levels = new[] {
                new Level(level1Planets, new Rectangle(-100, -1000, 6000, 2000), 1),
            };
        }

        public Player CreatePlayer() {
            return new Player(new Vector2(0, 0));
        }

        private static float Delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static List<Planet> GetPlanets() {
            return level.Planets;
        }
        
        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = Delta(gameTime);
            currentKeys = keys;
            currentMouse = mouse;

            // update code
            player.fullGrav(level.Planets);
            
            if (player.dead) player = CreatePlayer();
            
            player.Update(deltaTime, keys, mouse);
            camera.Position = player.pos - camera.Origin;


            if (keys.pressed(Keys.O)) {
                editMode ^= true;
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: camera.CalculateViewMatrix());
            
            // rendering code
            player.Draw(spriteBatch);

            _levels[0].Draw(gameTime, spriteBatch);
            
            // end
            spriteBatch.End();
        }
    }
}