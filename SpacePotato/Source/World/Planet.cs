using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Planet {
        
        private const float GravityStrengthModifier = .065f, GravityFalloffModifier = .0083f;

        private float _planetMass = 10000;

        public Texture2D texture;

        public Vector2 pos;

        public float radius;
        public float Diameter => radius * 2;

        public float X => pos.X;

        public float Y => pos.Y;

        public Planet(Vector2 pos, float radius, float mass = 10000, float changeInAngle = 0) {

            this.radius = radius;
            this.pos = pos;
            //_sprite = SpriteManager.MakePlanetTexture();

            texture = Loader.texture("Common/mars_planet");
        }
        
        public void Tick() {
            //does nothing

        }

        public Vector2 Gravity(Vector2 objectPosition) {

            var (x, y) = objectPosition;

            if (Math.Sqrt(Math.Pow(pos.X - x, 2) + Math.Pow(pos.Y - y, 2)) > 1000)
                return Vector2.Zero;

            float acceleration = (float)(GravityStrengthModifier * _planetMass /
                                         (Math.Pow((pos.X - x) * GravityFalloffModifier, 2) + 
                                          Math.Pow((pos.Y - y) * GravityFalloffModifier, 2)));

            float angle = (float)(Math.Atan((pos.Y - y) / (pos.X - x)));

            if (pos.X - x < 0 && pos.Y - y > 0 || pos.X - x < 0 && pos.Y - y < 0) {

                angle += (float)Math.PI;
                angle %= (float)Math.PI * 2;
            }

            return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);
        }
    }
}