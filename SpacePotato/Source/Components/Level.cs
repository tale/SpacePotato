using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    
    public class Level {

        public List<Planet> Planets { get; set; }
        public Rectangle Bounds { get; set; }
        public int LevelID { get; set; }

        public Level(List<Planet> planets, Rectangle bounds, int levelID) {
            this.Planets = planets;
            this.Bounds = bounds;
            this.LevelID = levelID;
            
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            
            foreach (Planet planet in Planets) {
                
                planet.Draw(spriteBatch);
            }
            
            RenderUtil.drawLine(new Vector2(Bounds.Left, Bounds.Top), 
                new Vector2(Bounds.Right, Bounds.Top), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLine(new Vector2(Bounds.Left, Bounds.Bottom), 
                new Vector2(Bounds.Right, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLine(new Vector2(Bounds.Left, Bounds.Top), 
                new Vector2(Bounds.Left, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
            RenderUtil.drawLine(new Vector2(Bounds.Right, Bounds.Top), 
                new Vector2(Bounds.Right, Bounds.Bottom), spriteBatch, Color.White, thickness:10);
        }
    }
}