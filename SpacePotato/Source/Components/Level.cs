using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace SpacePotato {
    
    public class Level {
        
        private Planet[] _planets;
        private Rectangle _bounds;
        private int _levelId;

        public Level(Planet[] planets, Rectangle bounds, int levelId) {
            this._planets = planets;
            this._bounds = bounds;
            this._levelId = levelId;
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            
            foreach (Planet planet in _planets) {
                
                planet.Draw(spriteBatch);
            }
        }
        
        
        
    }
}