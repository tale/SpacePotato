using System;
using System.Reflection;
using System.Text;
using CommandLine;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace SpacePotato {
    public static class Program {
        [STAThread]
        private static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options => {
                var title = Assembly.GetExecutingAssembly().GetName().Name;
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                var initialOutput = new StringBuilder();
                initialOutput.AppendLine($"{title} {version}");
                initialOutput.AppendLine("===== Launch Options =====");
                initialOutput.AppendLine($"Fullscreen Windowing Mode: {options.Fullscreen}");
                initialOutput.AppendLine($"Operating Resolution: {options.Resolution ?? "1280x720"}");
                Console.WriteLine(initialOutput);
                using var game = new SpacePotatoGame(options);
                game.Run();
            });
        }

        private static string GetAssemblyAttribute<T>(Func<T, string> value) where T : Attribute {
            var attribute = (T) Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }

    public class Options {
        [Option("fullscreen", Required = false, HelpText = "Set the game to run in fullscreen windowing mode.")]
        public bool Fullscreen { get; set; }

        [Option("resolution", Required = false,
            HelpText = "Set the game window resolution (overriden by Fullscreen if enabled)")]
        public string Resolution { get; set; }
        
        [Option("godmode", Required = false,
            HelpText = "Set the player in god mode so that they can no longer die")]
        public bool Godmode { get; set; }
    }
}
