
  using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
  using SpacePotato.Source.Util;


  namespace SpacePotato.Source.World {
    public class ParallaxLayer {

        private static readonly Random Random = new Random();
        
        private readonly int _scaler, _density;

        private readonly Rectangle _bounds;
        
        public Image[] ParallaxImages { get; }

        private static Texture2D[] _textures;

        public ParallaxLayer(Rectangle bounds, int scaler, int density) {
            
            _scaler = scaler;
            _density = density;
            _bounds = bounds;

            _textures = new[] {
                Loader.texture("Common/Planet1"),
                Loader.texture("Common/Planet2"),
                Loader.texture("Common/Planet3"),
                Loader.texture("Common/Planet4"),
                Loader.texture("Common/Planet5"),
                Loader.texture("Common/Planet6"),
            };
            
            ParallaxImages = GeneratePlanetImageMap();
        }

        private Image[] GeneratePlanetImageMap() {

            var array = new Image[_density];

            int iteration = 1000000;
            int number = 0;

            List<(int, int)> previous = new List<(int, int)>();

            do {

                var x = Random.Next(_bounds.Width);
                var y = Random.Next(_bounds.Height);

                int text = (int) (Random.NextDouble() * 6);
                Texture2D image = _textures[text];

                if (CheckPrevious(x, y, previous)) {
                    
                    array[number] = new Image(_bounds.Left + x, _bounds.Top + y, 84f / _scaler, 84f / _scaler, image);

                    previous.Add((x, y));
                    number++;
                }

                iteration--;

            } while (iteration > 0 && number < _density);

            return array;


        }


        private bool CheckPrevious(int x, int y, List<(int, int)> previous) {

            foreach ((int, int) prev in previous) {

                var (x2, y2) = prev;

                if (Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2)) < 300.0 / _scaler)
                    return false;
            }
            return true;
        }


        public void Draw(SpriteBatch spritebatch) {

            foreach (var image in ParallaxImages) {
                image?.Draw(spritebatch, scaler:_scaler);
            }
        }
    }
}
