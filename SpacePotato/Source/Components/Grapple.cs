using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Grapple {
        public Vector2 pos, vel;
        public float radius;
        public Player player;
        public Texture2D texture;
        private const short _maxLength = 600;
        
        //if degrade = true then the grapple will cease to exist after a few seconds
        public bool hit, degrade = false;

        public float DegradeTimer { get; private set; } = .5f;

        public Grapple(Vector2 pos, Vector2 vel) {
            texture = Loader.texture("Common/mars_planet");
            
            radius = 10;
            this.pos = pos;
            this.vel = vel;
        }

        public void Update(float deltaTime) {

            if (!hit) {
                if (Util.mag(pos - player.pos) >= _maxLength) {
                    player.Grapple = null;
                }
                
                pos += vel * deltaTime;
                
                var nearPlanets = MainScreen.GetPlanets().FindAll(planet => Util.mag(pos - planet.pos) < 1000);
                foreach (var planet in nearPlanets) {
                    if (planet.GetType() != "Blackhole" && Collision.circle(planet.pos, planet.radius, pos, radius)) {
                        hit = true;
                        pos = planet.pos + Util.polar(planet.radius, Util.angle(pos - planet.pos));
                        if (planet.GetType() == "Star") {
                            degrade = true;
                        }
                    }
                }
            }
            else if (degrade)
                DegradeTimer -= deltaTime;
        }

        public void Render(SpriteBatch spriteBatch) {
            RenderUtil.drawLine(pos, player.pos, spriteBatch, Color.White, 3);
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);

            /*Vector2 diff = pos - player.pos;
            float chainAngle = Util.angle(diff);
            Vector2 chainSize = new Vector2(5, 9) / 8F;

            Vector2 step = Util.polar(chainSize.Y, chainAngle);

            for (int i = 0; i < Util.mag(diff) / chainSize.Y; i++) {
                //Util.render(chain, pos - step * i, chainSize, chainAngle + (float) Math.PI / 2, camera, spriteBatch);
                Vector2 thisPos = pos - step * i;
                spriteBatch.Draw(texture, new Rectangle((int)(thisPos.X - radius), (int)(thisPos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);
            }*/
        }
    }
}