using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpacePotato.Source.Editor;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class MainScreen : GameScreen {
        
        // Static player instance
        private static Player _player;
        private static bool _isRespawning;
        private static float _respawnTimeout;
        private const float maxRespawnTime = 0.8F, respawnStartTransition = 0.5F;
        public static void RecreatePlayer(bool dead = false) {
            if (dead) {
                _player.health = 0;
                _isRespawning = true;
                _respawnTimeout = maxRespawnTime;
                _player.Giblerize();
            }
            else {
                Planet start = LevelManager.level.StartPlanet();
                Vector2 startVel = (start == null) ? new Vector2(0, 0) : new Vector2(800, 0);
                _player = new Player(StartPos()) {vel = startVel};
            }
        }

        public static Vector2 StartPos() {
            Planet start = LevelManager.level.StartPlanet();
            Vector2 startPos = (start == null) ? new Vector2(0, 0) : start.pos + (start.radius + 100) * Vector2.UnitX;
            return startPos;
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
        
        // shaders
        public static Effect planetShader;

        public MainScreen(Game game, int screenId) : base(game, screenId) {
            LevelManager.LoadLevels();
            Camera = new Camera(SpacePotatoGame.getGraphicsDevice().Viewport);

            RecreatePlayer();
            
            particlesOver = new ParticleSystem();
            particlesUnder = new ParticleSystem();
            
            planetShader = ContentManager.Load<Effect>("Shaders/Planet");
            planetShader.Parameters["ShadeTexture"].SetValue(Textures.genPlanetShade());
        }

        private static float Delta(GameTime gameTime) {
            return (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static List<Planet> GetPlanets() {
            return LevelManager.level.Planets;
        }
        
        public static List<AsteroidStream> GetAsteroidStreams() {
            return LevelManager.level.AsteroidStreams;
        }

        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = Delta(gameTime);

            CurrentKeys = keys;
            CurrentMouse = mouse;

            // update code
            if (_isRespawning) {
                _respawnTimeout -= deltaTime;
                if (_respawnTimeout <= 0) {
                    RecreatePlayer();
                    _isRespawning = false;
                }
                
                float lerpVal = Math.Max(0, (respawnStartTransition - _respawnTimeout) / respawnStartTransition);
                Camera.Position = Util.sinLerp(lerpVal, _player.pos, StartPos()) - Camera.Origin;

            }
            else {
                _player.Update(deltaTime, keys, mouse);
                Camera.Position = _player.pos - Camera.Origin;
                
            }
            
            particlesOver.Update(deltaTime);
            particlesUnder.Update(deltaTime);
            
            LevelManager.level.Update(deltaTime);
            
            // CONTROLS ==================
            
            // change level
            if (keys.pressed(Keys.Left)) LevelManager.PreviousLevel();
            if (keys.pressed(Keys.Right)) LevelManager.NextLevel();
            
            // game controls
            if (keys.pressed(Keys.R)) RecreatePlayer(true);
            
            // EDITOR CONTROLS
            if (keys.pressed(Keys.O)) {
                EditMode ^= true;
            }
            
            if (keys.pressed(Keys.M)) Editor.toggleMode();

            if (keys.pressed(Keys.K)) {
                GetPlanets().Clear();
            }

            if (keys.pressed(Keys.P)) {
                int ID = Util.randInt(100, 10000);
                Level level = new Level(GetPlanets(), LevelManager.level.bounds, ID) { AsteroidStreams = GetAsteroidStreams()};
                DataSerializer.Serialize("LevelFileTest", level);
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
            spriteBatch.End();
            
            
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: Camera.CalculateViewMatrix(),
                effect: planetShader);
            
            LevelManager.level.Render(gameTime, spriteBatch);
            
            spriteBatch.End();
            
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                transformMatrix: Camera.CalculateViewMatrix());

            if (!_isRespawning) _player.Render(spriteBatch);
            

            particlesOver.Render(spriteBatch);
            spriteBatch.End();

            
            _player.RenderUI(spriteBatch);
        }
    }
}
