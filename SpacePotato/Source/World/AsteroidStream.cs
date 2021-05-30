using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class AsteroidStream {

        private static readonly Random Random = new Random();
        private float _asteroidCheck;
        
        private readonly Vector2 _spawn, _despawn;
        private readonly float _radius, _asteroidVelocityAngle;
        private readonly float _averageVelocity, _velocitySTD, _averageSpawnRate;

        private List<Asteroid> _asteroids = new List<Asteroid>();
        public AsteroidStream(Vector2 spawn, Vector2 despawn, float radius,
            float averageVelocity, float velocitySTD, float averageSpawnRate) {

            this._spawn = spawn;
            this._despawn = despawn;
            this._radius = radius;
            this._averageVelocity = averageVelocity;
            this._velocitySTD = velocitySTD;
            this._averageSpawnRate = averageSpawnRate;

            _asteroidVelocityAngle = Util.angle(spawn - despawn);
        }

        public void Update(float deltaTime) {

            _asteroidCheck += deltaTime;

            while (_asteroidCheck > 1) {

                _asteroidCheck--;

                if (Spawn() && Random.NextDouble() > _averageSpawnRate ) {

                    float angle = (float)(Random.NextDouble() * Math.PI * 2);
                    float range = (float) (Random.NextDouble() * _radius);
                    float velocityMagnitude = (float)(_averageVelocity + (Random.NextDouble() - .5f) * 2 * _velocitySTD);
                        
                    _asteroids.Add(new Asteroid(new Vector2(
                        _spawn.X + (float)(range * Math.Cos(angle)), 
                        _spawn.Y + (float)(range * Math.Sin(angle))), new Vector2(
                        (float)(velocityMagnitude * Math.Cos(_asteroidVelocityAngle)), 
                        (float)(velocityMagnitude * Math.Sin(_asteroidVelocityAngle))), 
                        24));
                }
            }

            for (int n = 0; n < _asteroids.Count; n++) {

                if (Despawn(_asteroids[n])) {
                    _asteroids.Remove(_asteroids[n]);
                    n--;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch) {

            foreach (Asteroid asteroid in _asteroids) {
                asteroid.Render(spriteBatch);
            }
        }

        private bool Despawn(Asteroid asteroid) {

            return Util.mag(new Vector2(
                asteroid.Position.X - _despawn.X,
                asteroid.Position.Y - _despawn.Y)) < 
                   asteroid.Radius + _radius;
        }

        private bool Spawn() {

            foreach (Asteroid asteroid in _asteroids) {

                if (Util.mag(new Vector2(
                        asteroid.Position.X - _despawn.X,
                        asteroid.Position.Y - _despawn.Y)) <
                    asteroid.Radius + _radius)
                    return true;

            }

            return false;
        }
    }
}