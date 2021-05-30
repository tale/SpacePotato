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

        private static Texture2D heart, deadHeart;
        public Texture2D texture;
        public Vector2 pos, vel, dimen;
        public float rot, scale;

        private Grapple _grapple;

        private short _health = 3;
        private float _invincibilityTime;
        private const float maxInvincibilityTime = 0.1F;

        public Vector2 lastGrav;

        static Player() {
            heart = Loader.texture("Common/PotatoHeart");
            deadHeart = Loader.texture("Common/PotatoHeartGone");
        }

        public Player(Vector2 pos) {
            texture = Loader.texture("Common/image");
            rot = 0F;

            this.pos = pos;
            dimen = new Vector2(50, 50);
        }

        public void CollideWithPlanet(float deltaTime, Planet planet) {
            float velMag = Util.mag(vel);
            vel = Util.polar(Math.Max(velMag * 0.5F, 300), Util.angle(pos - planet.pos));

            if (_invincibilityTime < 0) {
                _health--;
                _grapple = null;
                _invincibilityTime = maxInvincibilityTime;

                if (_health == 0) {
                    _health = 3;
                    MainScreen.RecreatePlayer();
                }
            };
        }

        public Vector2 fullGrav(List<Planet> planets) {
            Vector2 grav = Vector2.Zero;

            foreach (var planet in planets) {
                grav += planet.Gravity(pos);
            }

            return grav;
        }

        public void Update(float deltaTime, KeyInfo keys, MouseInfo mouse) {

            _invincibilityTime -= deltaTime;
            
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
                        CollideWithPlanet(deltaTime, planet);
                    }
                }

                if (mouse.leftPressed) {
                    float angle = Util.angle(mouse.pos - Camera.screenCenter);
                    _grapple = new Grapple(pos, Util.polar(3000, angle)) {player = this};
                }
                if (mouse.leftUnpressed) _grapple = null;

                _grapple?.Update(deltaTime);
                if (_grapple is { hit: true }) {
                    vel += Util.polar(1000, Util.angle(_grapple.pos - pos)) * deltaTime;
                }

                lastGrav = grav;
            }
        }

        public void Render(SpriteBatch spriteBatch) {

            _grapple?.Render(spriteBatch);

            RenderUtil.drawLine(pos, pos + lastGrav / 3, spriteBatch, Color.Lerp(Color.Green, Color.Transparent, 0.25F), 4);

            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), Color.White);

            for (int h = 0; h < 3; h++) {

                Texture2D potatoHeart = (h < _health) ? heart : deadHeart;
                spriteBatch.Draw(potatoHeart, new Rectangle(-530 + h * 100 + (int)Camera.Position.X, 
                    -280 + (int)Camera.Position.Y, 64, 64), Color.White);
            }
            
            if (MainScreen.EditMode) {

                Rectangle rect = Util.center(Util.toWorld(MainScreen.CurrentMouse.pos),
                    Vector2.One * Editor.radius * 2);
                RenderUtil.drawRect(rect, spriteBatch, Color.Green, 3);
            }
        }
    }
}
