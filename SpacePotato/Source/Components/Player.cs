using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpacePotato.Source.Editor;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Player {

        private static Texture2D heart, deadHeart;
        public Texture2D texture;
        public Vector2 pos, vel, dimen;
        public float rot, scale;

        public Grapple Grapple;

        private short _health = 3;
        private float _invincibilityTime;
        private const float maxInvincibilityTime = 0.25F;

        public Vector2 lastGrav;

        static Player() {
            heart = Loader.texture("Common/PotatoHeart");
            deadHeart = Loader.texture("Common/PotatoHeartGone");
        }

        public Player(Vector2 pos) {
            texture = Loader.texture("Common/Potato");
            rot = 0F;

            this.pos = pos;
            dimen = new Vector2(10, 16) / 16F * 80;
        }

        public void CollideWithPlanet(float deltaTime, Planet planet, short subtractLives = 1) {

            if (planet.typeIndex == Planet.EndType) {
                LevelManager.NextLevel();
                return;
            }

            if (_invincibilityTime < 0 && !SpacePotatoGame.options.Godmode) {
                Hurt(planet, Util.angle(pos - planet.pos), subtractLives);
            }
            
            float velMag = Util.mag(vel);
            vel = Util.polar(Math.Max(velMag * 0.6F, 300), Util.angle(pos - planet.pos));
        }
        public void CollideWithAsteroid(float deltaTime, AsteroidStream stream, Asteroid asteroid, short subtractLives = 1) {

            
            if (_invincibilityTime < 0 && !SpacePotatoGame.options.Godmode) {
                Hurt(asteroid, Util.angle(pos - asteroid.pos), subtractLives);
            }
            
            float velMag = Util.mag(vel);
            vel = Util.polar(Math.Max(velMag * 0.6F, 300), Util.angle(pos - asteroid.pos));

            stream.Asteroids.Remove(asteroid);
        }

        public void Hurt(Planet planet, float angle, short subtractLives = 1) {
            _health -= subtractLives;
            Grapple = null;
            _invincibilityTime = maxInvincibilityTime;

            if (_health <= 0) {
                _health = 3;
                MainScreen.RecreatePlayer(true);
            }

            if (planet.GetType() != "Blackhole") {
                int particleCount = Util.randInt(10, 20);
                for (int i = 0; i < particleCount; i++) {
                    if (planet.GetType() == "Star")
                        MainScreen.particlesOver.Add(new FireParticle(Util.randomIn(pos, dimen),
                            Util.polar(Util.random(0.4F, 1F) * Util.mag(vel),
                                angle + Maths.PI + Util.randomPN(Maths.PI * 0.35F))));
                    else
                        MainScreen.particlesOver.Add(new HurtParticle(Util.randomIn(pos, dimen),
                            Util.polar(Util.random(0.4F, 1F) * Util.mag(vel),
                                angle + Maths.PI + Util.randomPN(Maths.PI * 0.35F))));
                }
            }
            else {
                //TODO gilberize player
            }
        }
        
        public void Hurt(Asteroid asteroid, float angle, short subtractLives = 1) {
            _health -= subtractLives;
            Grapple = null;
            _invincibilityTime = maxInvincibilityTime;

            if (_health <= 0) {
                _health = 3;
                MainScreen.RecreatePlayer();
            }
            
            int particleCount = Util.randInt(10, 20);
            
            for (int i = 0; i < particleCount; i++) {

                MainScreen.particlesOver.Add(new HurtParticle(Util.randomIn(pos, dimen),
                    Util.polar(Util.random(0.4F, 1F) * Util.mag(vel),
                        angle + Maths.PI + Util.randomPN(Maths.PI * 0.35F))));
                
                MainScreen.particlesOver.Add(new FireParticle(Util.randomIn(pos, dimen),
                    Util.polar(Util.random(0.4F, 1F) * Util.mag(asteroid.vel),
                        Util.angle(asteroid.vel) + Util.randomPN(Maths.PI * 0.35F))));
                

            }
            
        }

        public Vector2 FullGrav(List<Planet> planets) {
            Vector2 grav = Vector2.Zero;

            foreach (var planet in planets) {
                grav += planet.Gravity(pos);
            }

            return grav;
        }

        public void Update(float deltaTime, KeyInfo keys, MouseInfo mouse) {

            _invincibilityTime -= deltaTime;

            if (MainScreen.EditMode) {
                float speed = 1400;
                if (keys.down(Keys.A)) pos += deltaTime * Vector2.UnitX * -speed;
                if (keys.down(Keys.D)) pos += deltaTime * Vector2.UnitX * speed;
                if (keys.down(Keys.W)) pos += deltaTime * Vector2.UnitY * -speed;
                if (keys.down(Keys.S)) pos += deltaTime * Vector2.UnitY * speed;
            }


            if (MainScreen.EditMode) {
                var planets = MainScreen.GetPlanets();
                Vector2 worldMouse = Util.toWorld(mouse.pos);

                if (keys.pressed(Keys.Up)) Editor.nextRadius();
                if (keys.pressed(Keys.Down)) Editor.lastRadius();

                if (mouse.leftPressed) {
                    if (Editor.mode == Editor.EditorMode.Planet) {
                        Planet planet = new Planet(worldMouse, Editor.radius);
                    
                        if (keys.down(Keys.D1)) {
                            planet = new Planet(worldMouse, Planet.StartEndRadius);
                            planet.setTypeIndex(Planet.StartType);
                        } else if (keys.down(Keys.D2)) {
                            planet = new Planet(worldMouse, Planet.StartEndRadius);
                            planet.setTypeIndex(Planet.EndType);
                        }
                    
                        planets.Add(planet);
                        LevelManager.level.findBounds();
                    } else {
                        float rad = Editor.radius * 2;

                        if (!Editor.hasFirstAsteroidPos) {
                            Editor.hasFirstAsteroidPos = true;
                            Editor.firstAsteroidPos = worldMouse;

                        } else {
                            Editor.hasFirstAsteroidPos = false;
                            
                            var asteroidStreams = MainScreen.GetAsteroidStreams();
                            
                            asteroidStreams.Add(new AsteroidStream(Editor.firstAsteroidPos, worldMouse, rad));
                            LevelManager.level.findBounds();
                        }
                    }
                }

                if (mouse.rightDown) {
                    if (Editor.mode == Editor.EditorMode.Planet) {
                        for (int i = planets.Count - 1; i >= 0; i--) {
                            var planet = planets[i];
                            if (Collision.pointCircle(worldMouse, planet.pos, planet.radius)) {
                                planets.RemoveAt(i);
                                LevelManager.level.findBounds();
                            }
                        }
                    } else {
                        var asteroidStreams = MainScreen.GetAsteroidStreams();
                        for (int i = asteroidStreams.Count - 1; i >= 0; i--) {
                            var asteroidStream = asteroidStreams[i];
                            if (Collision.pointCircle(worldMouse, asteroidStream.spawn, asteroidStream.Radius) ||
                                Collision.pointCircle(worldMouse, asteroidStream.despawn, asteroidStream.Radius)) {
                                asteroidStreams.RemoveAt(i);
                                LevelManager.level.findBounds();
                            }
                        }
                    }
                }

            }
            else {
                var nearPlanets = MainScreen.GetPlanets().FindAll(planet => Util.mag(pos - planet.pos) < 1000);

                //cursed but I suck at this type of code
                List<(int, Asteroid)> nearAsteroids = new List<(int, Asteroid)>();
                for (int n = 0; n < MainScreen.GetAsteroidStreams().Count; n++) {
                    for (int n2 = 0; n2 < MainScreen.GetAsteroidStreams()[n].Asteroids.Count; n2++) {
                        if (Util.mag(pos - MainScreen.GetAsteroidStreams()[n].Asteroids[n2].pos) < 1000)
                            nearAsteroids.Add((n, MainScreen.GetAsteroidStreams()[n].Asteroids[n2]));
                    }

                }

                Rectangle bounds = LevelManager.level.bounds;
                if (!Collision.rect(pos, dimen, new Vector2(bounds.Center.X, bounds.Center.Y), new Vector2(bounds.Width, bounds.Height))) {
                    MainScreen.RecreatePlayer(true);
                }

                Vector2 grav = FullGrav(nearPlanets);
                vel += grav * deltaTime;
                if (keys.down(Keys.Space)) {
                    vel += Util.norm(grav) * -1000 * deltaTime;
                    if (Util.chance(deltaTime * 30)) {
                        float angle = Util.angle(grav);

                        MainScreen.particlesOver.Add(new FireParticle(Util.randomIn(pos, dimen), 
                            Util.polar(Util.random(0.5F, 1) * 200, angle + Util.randomPN(Maths.PI * 0.1F))));

                    }
                }

                pos += vel * deltaTime;

                foreach (var planet in nearPlanets) {
                    if (Collision.rectCircle(pos, dimen/3, planet.pos, planet.radius)) { // TODO: un-scuff collision
                        short subtractLives = planet.GetType() == "Blackhole" ? (short)3 : (short)1;
                        CollideWithPlanet(deltaTime, planet, subtractLives);
                    }
                }

                for (int n = 0; n < nearAsteroids.Count; n++) {
                    if (Collision.rectCircle(pos, dimen/2, nearAsteroids[n].Item2.pos, nearAsteroids[n].Item2.radius)) { // TODO: un-scuff collision
                        CollideWithAsteroid(deltaTime, MainScreen.GetAsteroidStreams()[nearAsteroids[n].Item1],nearAsteroids[n].Item2, 1);
                    }
                }

                if (mouse.leftPressed) {
                    float angle = Util.angle(mouse.pos - Camera.screenCenter);
                    Grapple = new Grapple(pos, Util.polar(3000, angle)) {player = this, strength = 1300};
                }
                if (mouse.leftUnpressed && Grapple != null && Grapple.strength > 0) Grapple = null;
                
                if (mouse.rightPressed) {
                    float angle = Util.angle(mouse.pos - Camera.screenCenter);
                    Grapple = new Grapple(pos, Util.polar(3000, angle)) {player = this, strength = -1500};
                }
                if (mouse.rightUnpressed && Grapple != null && Grapple.strength < 0) Grapple = null;

                Grapple?.Update(deltaTime);
                if (Grapple is { hit: true }) {
                    vel += Util.polar(Grapple.strength, Util.angle(Grapple.pos - pos)) * deltaTime;
                    if (Grapple.DegradeTimer < 0)
                        Grapple = null;
                }

                lastGrav = grav;
            }
        }

        public void Render(SpriteBatch spriteBatch) {

            Grapple?.Render(spriteBatch);

            RenderUtil.drawLine(pos, pos + lastGrav / 3, spriteBatch, Color.Lerp(Color.Green, Color.Transparent, 0.25F), 4);

            Vector2 toWorld = MainScreen.Camera.toWorld(MainScreen.CurrentMouse.pos);
            
            SpriteEffects effects = (toWorld.X - pos.X >= 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), (int)dimen.X, (int)dimen.Y), null, Color.White,
                0, Vector2.Zero, effects, 0);

            for (int h = 0; h < 3; h++) {

                Texture2D potatoHeart = (h < _health) ? heart : deadHeart;
                spriteBatch.Draw(potatoHeart, new Rectangle(-530 + h * 100 + (int)Camera.Position.X, 
                    -280 + (int)Camera.Position.Y, 64, 64), Color.White);
            }
            
            if (MainScreen.EditMode) {

                float size = Editor.radius * 2;
                if (Editor.mode == Editor.EditorMode.Asteroid) size *= 2;
                Rectangle rect = Util.center(Util.toWorld(MainScreen.CurrentMouse.pos),
                    Vector2.One * size);
                RenderUtil.drawRect(rect, spriteBatch, Color.Green, 3);

                if (Editor.mode == Editor.EditorMode.Asteroid) {
                    if (Editor.hasFirstAsteroidPos) {
                        Rectangle start = Util.center(Editor.firstAsteroidPos, Vector2.One * size);
                        RenderUtil.drawRect(start, spriteBatch, Color.Red, 3);
                    }

                    var asteroidStreams = MainScreen.GetAsteroidStreams();
                    foreach (var asteroidStream in asteroidStreams) {
                        Rectangle spawn = Util.center(asteroidStream.spawn, Vector2.One * asteroidStream.Radius * 2);
                        Rectangle despawn = Util.center(asteroidStream.despawn, Vector2.One * asteroidStream.Radius * 2);
                        RenderUtil.drawRect(spawn, spriteBatch, Color.Yellow, 3);
                        RenderUtil.drawRect(despawn, spriteBatch, Color.Orange, 3);
                    }
                }
            }
        }
    }
}
