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

        private ParallaxLayer layer1;
        private ParallaxLayer layer2;
        public Level(List<Planet> planets, Rectangle bounds, int levelID) {
            this.Planets = planets;
            this.Bounds = bounds;
            this.LevelID = levelID;

            Rectangle bounds2 = new Rectangle(bounds.Left - 500, bounds.Top - 500,
                bounds.Width + 500, bounds.Height + 1000);
            
            layer1 = new ParallaxLayer(bounds2, 2, 100);
            layer2 = new ParallaxLayer(bounds2, 3, 100);

        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            
            layer2.Draw(spriteBatch);
            layer1.Draw(spriteBatch);
            
            
            foreach (Planet planet in Planets) {
                
                planet.Draw(spriteBatch);
            }
            
            
            RenderUtil.drawLineScreen(new Vector2(Bounds.Left, Bounds.Top), 
                new Vector2(Bounds.Right, Bounds.Top), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLineScreen(new Vector2(Bounds.Left, Bounds.Bottom), 
                new Vector2(Bounds.Right, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLineScreen(new Vector2(Bounds.Left, Bounds.Top), 
                new Vector2(Bounds.Left, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLineScreen(new Vector2(Bounds.Right, Bounds.Top), 
                new Vector2(Bounds.Right, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
        }
    }
}