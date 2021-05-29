using System;
using Microsoft.Xna.Framework;

namespace SpacePotato.Source.World {
    public class Planet {
        
        private const float GravityStrengthModifier = .0000015f, GravityFalloffModifier = .0083f;

        private float _planetMass = 10000;

        public Planet(Vector2 position, float radius, float mass, float changeInAngle) {
            
            Radius = radius;
            Position = position;
            //_sprite = SpriteManager.MakePlanetTexture();
        }

        private Vector2 Position { get; }

        public float Radius { get; set; }

        public float X => Position.X;

        public float Y => Position.Y;
        public void Tick() {
            
            //does nothing

        }

        public Vector2 Gravity(GameTime gametime, Vector2 objectPosition) {

            var (x, y) = objectPosition;

            if (Math.Sqrt(Math.Pow(Position.X - x, 2) + Math.Pow(Position.Y - y, 2)) > 1000)
                return Vector2.Zero;

            float acceleration = (float)(GravityStrengthModifier * _planetMass /
                                         (Math.Pow((Position.X - x) * GravityFalloffModifier, 2) + 
                                          Math.Pow((Position.Y - y) * GravityFalloffModifier, 2)));

            float angle = (float)(Math.Atan((Position.Y - y) / (Position.X - x)));

            if (Position.X - x < 0 && Position.Y - y > 0 || Position.X - x < 0 && Position.Y - y < 0) {

                angle += (float)Math.PI;
                angle %= (float)Math.PI * 2;
            }

            return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));
        }
    }
}