using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato{
    public class Asteroid : SpatialBody {
        public Vector2 vel;
        private static Texture2D texture;

        static Asteroid() {
            texture = Loader.texture("Common/Asteroid");
        }
        public Asteroid(Vector2 position, Vector2 vel, float radius) {

            this.pos = position;
            this.radius = radius;
            this.vel = vel;
        }

        public override void Update(float deltaTime) {
            pos += vel * deltaTime;
        }

        public void Render(SpriteBatch spriteBatch) {
            
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), 
                (int)(Diameter), (int)(Diameter)), Color.White);
        }
    }
}