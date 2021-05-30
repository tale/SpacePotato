using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Planet : SpatialBody {
        
        private const float GravityStrengthModifier = .065f, GravityFalloffModifier = .0083f;

        private const float PlanetMass = 10000;

        [XmlIgnore]
        public Texture2D texture;
        
        public float mass { get; set; }
        public float angle { get; set; }
        public int typeIndex { get; set; }
        
        public float X => pos.X;

        public float Y => pos.Y;


        public static Texture2D[] textures;
        public const int StartType = 0, EndType = 1;
        public const float StartEndRadius = 300;
        
        static Planet() {
            const int count = 8;
            textures = new Texture2D[count];
            for (int i = 0; i < count; i++) {
                textures[i] = Loader.texture($"Common/Planet{i}");
            }
        }

        public Planet(Vector2 pos, float radius, float mass = PlanetMass, float angle = 0) {

            this.radius = radius;
            this.pos = pos;

            this.mass = mass;
            this.angle = angle;

            typeIndex = Util.randInt(2, 8);
            texture = textures[typeIndex];
        }

        public Planet() {}

        public void setUpSerialized() {
            texture = textures[typeIndex];
        }

        public void setTypeIndex(int index) {
            typeIndex = index;
            texture = textures[typeIndex];
        }

        public Vector2 Gravity(Vector2 objectPosition) {

            var (x, y) = objectPosition;

            if (Math.Sqrt(Math.Pow(pos.X - x, 2) + Math.Pow(pos.Y - y, 2)) > 1000)
                return Vector2.Zero;

            float acceleration = (float)(GravityStrengthModifier * PlanetMass /
                                         (Math.Pow((pos.X - x) * GravityFalloffModifier, 2) + 
                                          Math.Pow((pos.Y - y) * GravityFalloffModifier, 2)));

            float angle = (float)(Math.Atan((pos.Y - y) / (pos.X - x)));

            if (pos.X - x < 0 && pos.Y - y > 0 || pos.X - x < 0 && pos.Y - y < 0) {

                angle += (float)Math.PI;
                angle %= (float)Math.PI * 2;
            }

            return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));
        }

        public virtual void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture,
                new Rectangle((int) (pos.X - radius), (int) (pos.Y - radius), (int) (Diameter), (int) (Diameter)),
                Color.White);
        }


        public virtual string GetType() {
            return "Planet";
        }
    }
}