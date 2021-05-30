using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class AsteroidStream {

        private static readonly Random Random = new Random();
        private float _asteroidCheck;
        private float _asteroidVelocityAngle;

        public Vector2 spawn { get; set; }
        public Vector2 despawn { get; set; }
        public float Radius { get; set; }
        public float AverageVelocity { get; set; }
        public float VelocityStd { get; set; }
        public float AverageSpawnRate { get; set; }

        public List<Asteroid> Asteroids {get; set; } = new List<Asteroid>(); 
        public AsteroidStream(Vector2 spawn, Vector2 despawn, float radius,
            float averageVelocity = 500F, float velocitySTD = 100F, float averageSpawnRate = 1F) {

            this.spawn = spawn;
            this.despawn = despawn;
            this.Radius = radius;
            this.AverageVelocity = averageVelocity;
            this.VelocityStd = velocitySTD;
            this.AverageSpawnRate = averageSpawnRate;

            _asteroidVelocityAngle = Util.angle(despawn - spawn);
        }

        public AsteroidStream() {}

        public void setUpSerialized() {
            _asteroidVelocityAngle = Util.angle(despawn - spawn);
        }

        public void Update(float deltaTime) {
            
            _asteroidCheck += deltaTime;

            while (_asteroidCheck > 1) {

                _asteroidCheck--;

                //if (Spawn() && Random.NextDouble() > AverageSpawnRate ) {

                    float angle = (float)(Random.NextDouble() * Math.PI * 2);
                    float range = (float) (Random.NextDouble() * Radius);
                    //float velocityMagnitude = (float)(AverageVelocity + (Random.NextDouble() - .5f) * 2 * VelocityStd);
                    float velocityMagnitude = 500;
                        
                    Asteroids.Add(new Asteroid(new Vector2(
                        spawn.X + (float)(range * Math.Cos(angle)), 
                        spawn.Y + (float)(range * Math.Sin(angle))), Util.polar(velocityMagnitude, _asteroidVelocityAngle), 
                        24));
                //}
            }

            for (int n = 0; n < Asteroids.Count; n++) {

                Asteroids[n].Update(deltaTime);
                
                if (Despawn(Asteroids[n])) {
                    Asteroids.RemoveAt(n);
                    n--;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch) {

            foreach (Asteroid asteroid in Asteroids) {
                asteroid.Render(spriteBatch);
            }
        }

        private bool Despawn(Asteroid asteroid) {

            return Collision.circle(asteroid.pos, asteroid.radius, despawn, Radius);
        }

        private bool Spawn() {

            foreach (Asteroid asteroid in Asteroids) {

                if (Util.mag(new Vector2(
                        asteroid.pos.X - despawn.X,
                        asteroid.pos.Y - despawn.Y)) <
                    asteroid.radius + Radius)
                    return false;

            }

            return true;
        }
    }
}