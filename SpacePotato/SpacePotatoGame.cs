
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class SpacePotatoGame : Game {
        private readonly GraphicsDeviceManager _graphics;
        public static Options options;
        private readonly ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;

        public static SpacePotatoGame instance;
        
        public KeyboardState lastKeyboardState;
        public MouseState lastMouseState;

        public SpacePotatoGame(Options options) {
            _graphics = new GraphicsDeviceManager(this);
            _screenManager = new ScreenManager(this, 1);
            
            SpacePotatoGame.options = options;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            instance = this;
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth =
                options.Resolution != null ? int.Parse(options.Resolution.Split('x')[0]) : 1280;
            _graphics.PreferredBackBufferHeight =
                options.Resolution != null ? int.Parse(options.Resolution.Split('x')[1]) : 720;
            _graphics.IsFullScreen = options.Fullscreen && options.Fullscreen;

            _graphics.PreferMultiSampling = true;
            _graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            _graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 1024;

            _graphics.ApplyChanges();

            _screenManager.RegisterScreen(new DebugScreen(this, -1));
            _screenManager.RegisterScreen(new LoadingScreen(this, 0));
            _screenManager.RegisterScreen(new MainScreen(this, 1));

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        public static GraphicsDevice getGraphicsDevice() {
            return instance.GraphicsDevice;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            // key input
            KeyboardState keyState = Keyboard.GetState();
            KeyInfo keys = new KeyInfo(keyState, lastKeyboardState);
            lastKeyboardState = keyState;
            
            // mouse input
            MouseState mouseState = Mouse.GetState();
            MouseInfo mouse = new MouseInfo(mouseState, lastMouseState);
            lastMouseState = mouseState;

            _screenManager.Update(gameTime, keys, mouse);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            
            _screenManager.Render(gameTime, _spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}
