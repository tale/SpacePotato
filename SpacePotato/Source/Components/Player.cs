using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Runner;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Player {
        public Texture2D texture;
        public Vector2 pos, vel, dimen;
        public float rot, scale;

        public Player(Vector2 pos) {
            texture = Loader.texture("Common/spinner");
            rot = 0F;

            this.pos = pos;
            dimen = new Vector2(50, 50);
        }


        public Vector2 fullGrav(List<Planet> planets) {
            Vector2 grav = Vector2.Zero;

            foreach (var planet in planets) {
                grav += planet.Gravity(pos);
            }
            
            return grav;
        }

        public void Update(float deltaTime, KeyInfo keys) {

            const float speed = 700;
            if (keys.down(Keys.A)) pos += deltaTime * Vector2.UnitX * -speed;
            if (keys.down(Keys.D)) pos += deltaTime * Vector2.UnitX * speed;
            if (keys.down(Keys.W)) pos += deltaTime * Vector2.UnitY * -speed;
            if (keys.down(Keys.S)) pos += deltaTime * Vector2.UnitY * speed;
            
            var nearPlanets = MainScreen.planets.FindAll(planet => Util.mag(pos - planet.pos) < 1000);

            vel += fullGrav(nearPlanets) * deltaTime;
            pos += vel * deltaTime;

            foreach (var planet in nearPlanets) {
                if (Collision.rectCircle(pos, dimen, planet.pos, planet.radius)) { // TODO: un-scuff collision
                    pos = Vector2.Zero;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            
            //spriteBatch.Draw(texture, pos, null, Color.White, rot, new Vector2(texture.Width / 2F, texture.Height / 2F), scale, SpriteEffects.None, 0);
            
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), Color.White);
        }
    }
}
