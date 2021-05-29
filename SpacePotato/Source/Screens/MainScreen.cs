using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        
        // basic stuff
        private static Camera _camera;
        
        // settings and bools
        public static bool EditMode { get; set; }

        // objects
        private static Player _player;
        
        private static Level[] _levels;

        private static int _currLevel = 0;


        public MainScreen(Game game, int screenId) : base(game, screenId) {

            _camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            _player = CreatePlayer();

            var level1Planets = Enumerable.Range(0, 50).
                Select(i => new Planet(new Vector2(Util.random(-100, 5900), 
                    Util.random(-1000, 1000)), 100)).ToList();
            
            _levels = new Level[1];
            _levels[0] = new Level(level1Planets, new Rectangle(-100, -1000, 6000, 2000), 1);
        }

        public Player CreatePlayer() {
            return new Player(new Vector2(0, 0));
        }

        private static float Delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static List<Planet> GetPlanets() {
            return _levels[_currLevel].Planets;
        }
        
        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = Delta(gameTime);

            // update code
            _player.fullGrav(_levels[_currLevel].Planets);
            
            if (_player.dead) _player = CreatePlayer();
            
            _player.Update(deltaTime, keys, mouse);
            _camera.Position = _player.pos - _camera.Origin;


            if (keys.pressed(Keys.O)) {
                EditMode ^= true;
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // start
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: _camera.CalculateViewMatrix());
            
            // rendering code
            _player.Draw(spriteBatch);

            _levels[0].Draw(gameTime, spriteBatch);
            
            // end
            spriteBatch.End();
        }
    }
}