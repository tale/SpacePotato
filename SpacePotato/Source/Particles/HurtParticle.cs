using System;
using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class HurtParticle : FadeParticle {

        public HurtParticle(Vector2 pos, Vector2 vel) : base(new Color(199/255F, 149/255F, 0), pos, vel) {
            bonkMult = -0.3F;
            timeLeft = Util.random(1F, 1.3F);
            startFade = 0.3F;
        }
    }
}