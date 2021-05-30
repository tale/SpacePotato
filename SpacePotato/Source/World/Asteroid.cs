using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato.Source.World {
    public class Asteroid {
        private Vector2 _velocity;

        public Vector2 Position { get; private set; }
        
        public float Radius { get; private set; }

        private static Texture2D temp;

        static Asteroid() {
            temp = Loader.texture("Common/mars_planet");
        }
        public Asteroid(Vector2 position, Vector2 velocity, float radius) {

            this.Position = position;
            this._velocity = velocity;
            this.Radius = radius;
        }

        public void Update(float gameTime) {

            Position += _velocity;

        }

        public void Render(SpriteBatch spriteBatch) {
            
            spriteBatch.Draw(temp, new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), 
                (int)(Radius * 2), (int)(Radius * 2)), Color.White);
        }
    }
}