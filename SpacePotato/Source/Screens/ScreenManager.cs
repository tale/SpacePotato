using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class ScreenManager {
        private readonly Game _game;
        private readonly Dictionary<int, GameScreen> _screens = new Dictionary<int, GameScreen>();
        private int _activeScreenId;

        public ScreenManager(Game game, int initialScreenId) {
            _activeScreenId = initialScreenId;
            _game = game;
        }

        public void RegisterScreen(GameScreen screen) {
            _screens.Add(screen.ScreenId, screen);
        }

        public void SwitchScreen(int screenId) {
            _activeScreenId = screenId;
        }

        public void Update(GameTime gameTime, KeyInfo keys) {
            if (_screens.ContainsKey(_activeScreenId)) _screens[_activeScreenId].Update(gameTime, keys);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (_screens.ContainsKey(_activeScreenId)) _screens[_activeScreenId].Draw(gameTime, spriteBatch);
        }
    }
}
