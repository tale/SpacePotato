using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    
    public class Level {

        public Planet[] planets { get; set; }
        public Rectangle bounds { get; set; }
        public int levelID { get; set; }

        public Level(Planet[] planets, Rectangle bounds, int levelID) {
            this.planets = planets;
            this.bounds = bounds;
            this.levelID = levelID;
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            
            foreach (Planet planet in planets) {
                
                planet.Draw(spriteBatch);
            }
        }
    }
}