using Microsoft.Xna.Framework;

namespace SpacePotato.Source.Editor {
    public static class Editor {

        public static float[] radii = {
            100,
            150,
            200,
            250,
            300,
            50,
        };

        private static int currRadius;
        public static float radius = radii[currRadius];
        public static EditorMode mode = EditorMode.Planet;

        public static bool hasFirstAsteroidPos;
        public static Vector2 firstAsteroidPos;

        public enum EditorMode {
            Planet, Asteroid
        }
        
        //public static int typeIndex = 0;

        public static void nextRadius() {
            currRadius = (currRadius + 1) % radii.Length;
            radius = radii[currRadius];
        }
        
        public static void lastRadius() {
            currRadius--;
            if (currRadius == -1) currRadius = radii.Length - 1;
            radius = radii[currRadius];
        }

        public static void toggleMode() {
            if (mode == EditorMode.Planet) {
                mode = EditorMode.Asteroid;
            } else if (mode == EditorMode.Asteroid) {
                mode = EditorMode.Planet;
                hasFirstAsteroidPos = false;
            }
            
            Logger.log(mode.ToString());
        }
    }
}