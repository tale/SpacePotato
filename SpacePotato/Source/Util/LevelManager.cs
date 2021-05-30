using System.IO;

namespace SpacePotato.Source.Util {
    public class LevelManager {
        
        // Static level management
        private static Level[] _levels;
        private static short _currentLevel;
        public static Level level;

        public static void NextLevel() {
            _currentLevel++;
        }

        public static void PreviousLevel() {
            _currentLevel++;
        }

        public static void ResetLevel() {
            _currentLevel = 0;
        }
        public static void LoadLevels() {
            string[] files = Directory.GetFiles(Paths.dataPath, "*.xml");
            _levels = new Level[files.Length];

            for (int i = 0; i < files.Length; i++) {
                _levels[i] = DataSerializer.Deserialize<Level>(files[i]);
                _levels[i].setUpSerialized();
            }

            level = _levels[_currentLevel];
        }
    }
}