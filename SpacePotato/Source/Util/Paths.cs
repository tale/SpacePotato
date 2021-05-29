using System.IO;

namespace SpacePotato {
    public static class Paths {

        public static string solutionPath, dataPath;

        static Paths() {
            string path = Path.GetFullPath("hi");
            solutionPath = path.Substring(0, path.IndexOf("bin\\Debug"));
            dataPath = solutionPath + "Data\\";
        }

    }
}