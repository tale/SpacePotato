using System;
using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class CollisionParticle : TintParticle {
        
        private const float collisionStep = 0.1F;
        public bool hasCollision = false; // defaults to point-collision
        public float bonkMult = 0;

        public CollisionParticle(Color color, Vector2 pos, Vector2 vel) : base(color, pos, vel) {}

        public override void Update(float deltaTime) {
            collisionMove(vel * deltaTime);
            
            timeLeft -= deltaTime;
            if (timeLeft <= 0) delete = true;
        }

        public bool collidesAt(Vector2 thisPos) {

            foreach (var planet in MainScreen.GetPlanets()) {
                if (Collision.pointCircle(thisPos, planet.pos, planet.radius)) return true;
            }
            return false;
        }

        protected void collisionMove(Vector2 fullDiff) {
            if (!collidesAt(pos + fullDiff)) { // TODO: improve (can result in clipping [due to initial skip])
                pos += fullDiff;
            } else {

                float diffX = 0, diffY = 0;
                float stepX = collisionStep * Math.Sign(fullDiff.X), stepY = collisionStep * Math.Sign(fullDiff.Y);
                
                // x-component
                if (!collidesAt(pos + Vector2.UnitX * fullDiff.X)) {
                    pos += Vector2.UnitX * fullDiff.X;
                } else {
                    for (int i = 0; i < Math.Abs(fullDiff.X) / collisionStep; i++) {
                        diffX += stepX;
                        if (collidesAt(pos + Vector2.UnitX * diffX)) {

                            diffX -= stepX;
                            vel.X *= bonkMult; // bonking
                            break;
                        }
                    }

                    pos += Vector2.UnitX * diffX;
                }

                // y-component
                if (!collidesAt(pos + Vector2.UnitY * fullDiff.Y)) {
                    pos += Vector2.UnitY * fullDiff.Y;
                } else {
                    for (int i = 0; i < Math.Abs(fullDiff.Y) / collisionStep; i++) {
                        diffY += stepY;
                        if (collidesAt(pos + Vector2.UnitY * diffY)) {
                            diffY -= stepY;
                            vel.Y *= bonkMult; // bonking
                            break;
                        }
                    }

                    pos += Vector2.UnitY * diffY;
                }
            }
        }
    }
}