using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class ScreenManager {
        private readonly Game _game;
        private readonly Dictionary<int, GameScreen> _screens = new Dictionary<int, GameScreen>();
        private int _activeScreenId;

        public static ScreenManager instance;

        public ScreenManager(Game game, int initialScreenId) {
            _activeScreenId = initialScreenId;
            _game = game;
            instance = this;
        }

        public void RegisterScreen(GameScreen screen) {
            _screens.Add(screen.ScreenId, screen);
        }

        public static void SwitchScreen(int screenId) {
            instance._activeScreenId = screenId;
        }

        public void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            if (_screens.ContainsKey(_activeScreenId)) _screens[_activeScreenId].Update(gameTime, keys, mouse);
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch) {
            if (_screens.ContainsKey(_activeScreenId)) _screens[_activeScreenId].Render(gameTime, spriteBatch);
        }
    }
}
