using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato.Source.World {
    [XmlInclude(typeof(Star))]
    public class Star : Planet {
        
        private const float StarMass = 20000;
        private static readonly Texture2D StarTexture;

        static Star() {
            StarTexture = Loader.texture("Common/Sun");
        }

        public Star() {}

        public Star(Vector2 pos, float radius, float mass = StarMass, float angle = 0) {
            
            this.radius = radius;
            this.pos = pos;

            this.mass = mass;
            this.angle = angle;
            
            typeIndex = -1;
        }

        public override void setUpSerialized() {
            texture = StarTexture;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(StarTexture, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);
        }
        
        public override string GetType() {
            return "Star";
        }
    }
}