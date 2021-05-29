using SpacePotato.Renderer;
using SpacePotato.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class SpacePotatoGame : Game {
        private readonly GraphicsDeviceManager _graphics;
        private readonly Options _options;
        private readonly ScreenManager _screenManager;
        private Camera _camera;
        private SpriteBatch _spriteBatch;

        public SpacePotatoGame(Options options) {
            _graphics = new GraphicsDeviceManager(this);
            _screenManager = new ScreenManager(this, 1);
            _options = options;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth =
                _options.Resolution != null ? int.Parse(_options.Resolution.Split('x')[0]) : 1280;
            _graphics.PreferredBackBufferHeight =
                _options.Resolution != null ? int.Parse(_options.Resolution.Split('x')[1]) : 720;
            _graphics.IsFullScreen = _options.Fullscreen && _options.Fullscreen;

            _graphics.PreferMultiSampling = true;
            _graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            _graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 1024;

            _graphics.ApplyChanges();
            _camera = new Camera(GraphicsDevice.Viewport);

            _screenManager.RegisterScreen(new LoadingScreen(this, 0));
            _screenManager.RegisterScreen(new MainScreen(this, 1));

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            // movement
            if (keyboardState.IsKeyDown(Keys.W))
                _camera.Position -= new Vector2(0, 250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.S))
                _camera.Position += new Vector2(0, 250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.A))
                _camera.Position -= new Vector2(250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.D))
                _camera.Position += new Vector2(250, 0) * deltaTime;

            _screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap,
                transformMatrix: _camera.CalculateViewMatrix());
            _screenManager.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
