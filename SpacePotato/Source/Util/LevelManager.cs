using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class LevelManager {
        
        // Static level management
        private static Level[] _levels;
        private static short _currentLevel;
        public static Level level;

        public static void NextLevel() {
            _currentLevel = (short) ((_currentLevel + 1) % _levels.Length);
            level = _levels[_currentLevel];
        }

        public static void PreviousLevel() {
            _currentLevel--;
            if (_currentLevel == -1) _currentLevel = (short) (_levels.Length - 1);
            level = _levels[_currentLevel];
        }

        public static void ResetLevel() {
            _currentLevel = 0;
            level = _levels[_currentLevel];
        }
        public static void LoadLevels() {
            string[] files = Directory.GetFiles(Paths.dataPath, "*.xml");
            _levels = new Level[files.Length + 1];

            // random debug level
            var level1Planets = Enumerable.Range(0, 50).
                Select(i => new Planet(new Vector2(Util.random(-100, 5900),
                    Util.random(-1000, 1000)), 100)).ToList();
            _levels[0] = new Level(level1Planets, new Rectangle(-100, -1000, 6000, 2000), 1);

            // file loaded levels
            for (int i = 1; i <= files.Length; i++) {
                _levels[i] = DataSerializer.Deserialize<Level>(files[i-1]);
                _levels[i].setUpSerialized();
            }

            level = _levels[_currentLevel];
        }
    }
}