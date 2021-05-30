using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.World;

namespace SpacePotato {
    
    public class Level {

        public List<Planet> Planets { get; set; }
        public List<AsteroidStream> AsteroidStreams { get; set; }
        public Rectangle bounds;
        public int LevelID { get; set; }

        private ParallaxLayer _layer1, _layer2, _starLayer;

        public Level() {}

        public void setUpSerialized() {
            findBounds();
            foreach (var planet in Planets) {
                planet.setUpSerialized();
            }
            foreach (var asteroidStream in AsteroidStreams) {
                asteroidStream.setUpSerialized();
            }
            Rectangle bounds2 = new Rectangle(bounds.Left - 500, bounds.Top - 500,
                bounds.Width + 500, bounds.Height + 1000);
            _layer1 = new ParallaxLayer(bounds2, 2, 75);
            _layer2 = new ParallaxLayer(bounds2, 3, 100);
            _starLayer = new ParallaxLayer(bounds2, 5, 150, true);
        }

        public void findBounds() {
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            bool trigger = false;
            
            void apply(float x, float y, float radius) {
                minX = Math.Min(minX, x - radius);
                minY = Math.Min(minY, y - radius);
                maxX = Math.Max(maxX, x + radius);
                maxY = Math.Max(maxY, y + radius);
                trigger = true;
            }

            foreach (var planet in Planets) {
                apply(planet.pos.X,planet.pos.Y, planet.radius);
            }

            foreach (var asteroidStream in AsteroidStreams) {
                apply(asteroidStream.spawn.X, asteroidStream.spawn.Y, asteroidStream.Radius);
                apply(asteroidStream.despawn.X, asteroidStream.despawn.Y, asteroidStream.Radius);
            }

            if (!trigger) {
                minX = 0;
                maxX = 0;
                minY = 0;
                maxY = 0;
            }

            int diff = 1000;
            minX -= diff;
            minY -= diff;
            maxX += diff;
            maxY += diff;

            bounds = new Rectangle((int) minX, (int) minY, (int) (maxX - minX), (int) (maxY - minY));
        }

        public Level(List<Planet> planets, Rectangle bounds, int levelID) {
            this.Planets = planets;
            this.bounds = bounds;
            this.LevelID = levelID;
            AsteroidStreams = new List<AsteroidStream>();

            Rectangle bounds2 = new Rectangle(bounds.Left - 500, bounds.Top - 500,
                bounds.Width + 500, bounds.Height + 1000);
            _layer1 = new ParallaxLayer(bounds2, 2, 100);
            _layer2 = new ParallaxLayer(bounds2, 3, 100);
            _starLayer = new ParallaxLayer(bounds2, 5, 100, true);
            findBounds();
        }

        public Planet StartPlanet() {
            return Planets.Find(planet => planet.typeIndex == Planet.StartType);
        }
        
        public Planet EndPlanet() {
            return Planets.Find(planet => planet.typeIndex == Planet.EndType);
        }

        public void Update(float deltaTime) {
            foreach (var asteroidStream in AsteroidStreams) {
                asteroidStream.Update(deltaTime);
            }
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch) {
            
            _starLayer.Render(spriteBatch);
            _layer2.Render(spriteBatch);
            _layer1.Render(spriteBatch);
            
            
            foreach (var planet in Planets) {
                planet.Render(spriteBatch);
            }
            
            foreach (var asteroidStream in AsteroidStreams) {
                asteroidStream.Render(spriteBatch);
            }
            

            RenderUtil.drawRect(bounds, spriteBatch, Color.White, 10);
        }
    }
}