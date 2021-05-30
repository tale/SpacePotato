using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.World;

namespace SpacePotato {
    
    public class Level {

        public List<Planet> Planets { get; set; }
        public Rectangle Bounds { get; set; }
        public int LevelID { get; set; }

        private ParallaxLayer _layer1, _layer2, _starLayer;

        public Level() {}

        public void setUpSerialized() {
            foreach (var planet in Planets) {
                planet.setUpSerialized();
            }
            Rectangle bounds2 = new Rectangle(Bounds.Left - 500, Bounds.Top - 500,
                Bounds.Width + 500, Bounds.Height + 1000);
            _layer1 = new ParallaxLayer(bounds2, 2, 75);
            _layer2 = new ParallaxLayer(bounds2, 3, 100);
            _starLayer = new ParallaxLayer(bounds2, 5, 150, true);
        }

        public Level(List<Planet> planets, Rectangle bounds, int levelID) {
            this.Planets = planets;
            this.Bounds = bounds;
            this.LevelID = levelID;

            Rectangle bounds2 = new Rectangle(bounds.Left - 500, bounds.Top - 500,
                bounds.Width + 500, bounds.Height + 1000);
            _layer1 = new ParallaxLayer(bounds2, 2, 100);
            _layer2 = new ParallaxLayer(bounds2, 3, 100);
            _starLayer = new ParallaxLayer(bounds2, 5, 100, true);
        }

        public Planet StartPlanet() {
            return Planets.Find(planet => planet.typeIndex == Planet.StartType);
        }
        
        public Planet EndPlanet() {
            return Planets.Find(planet => planet.typeIndex == Planet.EndType);
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch) {
            
            _starLayer.Render(spriteBatch);
            _layer2.Render(spriteBatch);
            _layer1.Render(spriteBatch);
            
            
            foreach (Planet planet in Planets) {
                
                planet.Render(spriteBatch);
            }
            

            RenderUtil.drawRect(Bounds, spriteBatch, Color.White, 10);
        }
    }
}