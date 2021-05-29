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

        public Grapple grapple;

        public bool dead;

        public Player(Vector2 pos) {
            texture = Loader.texture("Common/image");
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

        public void Update(float deltaTime, KeyInfo keys, MouseInfo mouse) {

            float speed = (MainScreen.editMode) ? 1400 : 700;
            if (keys.down(Keys.A)) pos += deltaTime * Vector2.UnitX * -speed;
            if (keys.down(Keys.D)) pos += deltaTime * Vector2.UnitX * speed;
            if (keys.down(Keys.W)) pos += deltaTime * Vector2.UnitY * -speed;
            if (keys.down(Keys.S)) pos += deltaTime * Vector2.UnitY * speed;
            

            if (MainScreen.editMode) {
                
                
                
            }
            else {
                var nearPlanets = MainScreen.planets.FindAll(planet => Util.mag(pos - planet.pos) < 1000);
                
                vel += fullGrav(nearPlanets) * deltaTime;
                pos += vel * deltaTime;
                
                foreach (var planet in nearPlanets) {
                    if (Collision.rectCircle(pos, dimen/2, planet.pos, planet.radius)) { // TODO: un-scuff collision
                        dead = true;
                    }
                }

                if (mouse.leftPressed) {
                    float angle = Util.angle(mouse.pos - Camera.screenCenter);
                    grapple = new Grapple(pos, Util.polar(3000, angle)) {player = this};
                }
                if (mouse.leftUnpressed) grapple = null;

                grapple?.Update(deltaTime);
                if (grapple != null && grapple.hit) {
                    vel += Util.polar(1000, Util.angle(grapple.pos - pos)) * deltaTime;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            
            //spriteBatch.Draw(texture, pos, null, Color.White, rot, new Vector2(texture.Width / 2F, texture.Height / 2F), scale, SpriteEffects.None, 0);

            grapple?.Draw(spriteBatch);
            
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), Color.White);

            if (MainScreen.editMode) {
                
                
                
            }
        }
    }
}
