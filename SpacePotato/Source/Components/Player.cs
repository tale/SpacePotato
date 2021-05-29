using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Runner;
using SpacePotato.Source.Editor;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Player {
        public Texture2D texture;
        public Vector2 pos, vel, dimen;
        public float rot, scale;

        public Grapple grapple;

        public bool dead;

        public Vector2 lastGrav;

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

            float speed = (MainScreen.EditMode) ? 1400 : 700;
            if (keys.down(Keys.A)) pos += deltaTime * Vector2.UnitX * -speed;
            if (keys.down(Keys.D)) pos += deltaTime * Vector2.UnitX * speed;
            if (keys.down(Keys.W)) pos += deltaTime * Vector2.UnitY * -speed;
            if (keys.down(Keys.S)) pos += deltaTime * Vector2.UnitY * speed;
            

            if (MainScreen.EditMode) {
                var planets = MainScreen.GetPlanets();
                Vector2 worldMouse = Util.toWorld(mouse.pos);
                
                if (keys.pressed(Keys.Up)) Editor.nextRadius();
                if (keys.pressed(Keys.Down)) Editor.lastRadius();
                
                if (mouse.leftPressed) {
                    planets.Add(new Planet(worldMouse, Editor.radius));
                }

                if (mouse.rightDown) {
                    for (int i = planets.Count - 1; i >= 0; i--) {
                        var planet = planets[i];
                        if (Collision.pointCircle(worldMouse, planet.pos, planet.radius)) {
                            planets.RemoveAt(i);
                        }
                    }
                }

            }
            else {
                var nearPlanets = MainScreen.GetPlanets().FindAll(planet => Util.mag(pos - planet.pos) < 1000);

                Vector2 grav = fullGrav(nearPlanets);
                vel += grav * deltaTime;
                if (keys.down(Keys.Space)) vel += Vector2.Normalize(grav * -1) * 1000 * deltaTime;
                
                pos += vel * deltaTime;

                foreach (var planet in nearPlanets) {
                    if (Collision.rectCircle(pos, dimen/2, planet.pos, planet.radius)) { // TODO: un-scuff collision
                        dead = true;
                    }
                }

                if (mouse.leftPressed) {
                    float angle = Util.angle(mouse.pos - Camera.screenCenter);
                    grapple = new Grapple(pos, Util.polar(4000, angle)) {player = this};
                }
                if (mouse.leftUnpressed) grapple = null;

                grapple?.Update(deltaTime);
                if (grapple != null && grapple.hit) {
                    vel += Util.polar(1000, Util.angle(grapple.pos - pos)) * deltaTime;
                }

                lastGrav = grav;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            
            grapple?.Draw(spriteBatch);
            
            RenderUtil.drawLine(pos, pos + lastGrav / 3, spriteBatch, Color.Lerp(Color.Green, Color.Transparent, 0.25F), 4);
            
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), Color.White);

            if (MainScreen.EditMode) {

                Rectangle rect = Util.center(Util.toWorld(MainScreen.CurrentMouse.pos),
                    Vector2.One * Editor.radius * 2);
                RenderUtil.drawRect(rect, spriteBatch, Color.Green, 3);
            }
        }
    }
}
