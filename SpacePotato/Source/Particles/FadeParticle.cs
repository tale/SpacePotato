using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class FadeParticle : CollisionParticle {

        public float startFade = 0.1F;
        
        public FadeParticle(Color color, Vector2 pos, Vector2 vel) : base(color, pos, vel) {
            
        }
        
        public override void Update(float deltaTime) {
            base.Update(deltaTime);
            if (timeLeft <= startFade) {
                color.A = (byte) (255 * timeLeft / startFade);
            }
        }
        
    }
}