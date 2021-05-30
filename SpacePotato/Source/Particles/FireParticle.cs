using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class FireParticle : FadeParticle {
        public FireParticle(Vector2 pos, Vector2 vel) : base(findColor(), pos, vel) {
            bonkMult = -0.2F;
            timeLeft = Util.random(0.4F, 0.7F);
            startFade = 0.2F;
        }

        public static Color findColor() {
            return new Color(1F, Util.random(), 0F);
        }
    }
}