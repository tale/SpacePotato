using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato.Source.World {
    public class AsteroidStream {

        private float asteroidCheck = 0;
        
        private Vector2 _spawn, _despawn;
        private float _radius;

        private float _averageVelocity, _velocitySTD, _averageSpawnRate, _spawnRateSTD;

        private List<Asteroid> _asteroids = new List<Asteroid>();
        public AsteroidStream(Vector2 spawn, Vector2 despawn, float radius,
            float averageVelocity, float velocitySTD, float averageSpawnRate, float spawnRateSTD) {

            this._spawn = spawn;
            this._despawn = despawn;
            this._radius = radius;
            this._averageVelocity = averageVelocity;
            this._velocitySTD = velocitySTD;
            this._averageSpawnRate = averageSpawnRate;
            this._spawnRateSTD = spawnRateSTD;

        }

        public void Update(float deltaTime) {

            asteroidCheck += deltaTime;
            

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

            return SpacePotato.Util.mag(new Vector2(
                asteroid.Position.X - _despawn.X,
                asteroid.Position.Y - _despawn.Y)) < 
                   asteroid.Radius + _radius;
        }
    }
}