namespace SpacePotato.Source.Editor {
    public static class Editor {

        public static float[] radii = new float[] {
            100,
            150,
            200,
            250,
            300,
            50,
        };

        private static int currRadius = 0;
        public static float radius = radii[currRadius];
        public static int typeIndex = 0;

        public static void nextRadius() {
            currRadius = (currRadius + 1) % radii.Length;
            radius = radii[currRadius];
        }
        
        public static void lastRadius() {
            currRadius--;
            if (currRadius == -1) currRadius = radii.Length - 1;
            radius = radii[currRadius];
        }
    }
}