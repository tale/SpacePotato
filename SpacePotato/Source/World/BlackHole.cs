using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato.Source.World {
    public class BlackHole : Planet {
        
        private const float BlackHoleMass = 30000;
        private static readonly Texture2D BlackHoleText;

        static BlackHole() {
            BlackHoleText = Loader.texture("Common/BlackHole");
        }
        public BlackHole(Vector2 pos, float radius, float mass = BlackHoleMass, float angle = 0) {
            
            this.radius = radius;
            this.pos = pos;

            this.mass = mass;
            this.angle = angle;
        }
        
        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(BlackHoleText, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);
        }
        public override string getType() {
            return "Blackhole";
        }
    }
}