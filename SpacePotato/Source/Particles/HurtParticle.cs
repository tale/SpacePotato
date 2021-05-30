using System;
using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class HurtParticle : CollisionParticle {

        public HurtParticle(Vector2 pos, Vector2 vel) : base(Color.Red, pos, vel) {
            bonkMult = -0.3F;
            timeLeft = Util.random(1F, 1.3F);
        }

        public override void Update(float deltaTime) {
            base.Update(deltaTime);
            if (timeLeft <= 0.3F) {
                color.A = (byte) (255 * timeLeft / 0.3F);
            }
        }
    }
}