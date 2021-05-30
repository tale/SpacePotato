using System;
using System.IO;

namespace SpacePotato {
    public static class Paths {

        public static string solutionPath, dataPath;

        static Paths() {
            dataPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent!.Parent!.FullName!, "Data");
        }

    }
}