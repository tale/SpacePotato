using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class ParticleSystem {
        public List<Particle> particles = new List<Particle>(100);

        // don't make any of these foreach loops
        public void Update(float deltaTime) {
            for (int i = particles.Count - 1; i >= 0; i--) {
                Particle particle = particles[i];
                particle.Update(deltaTime);
                if (particle.delete) particles.RemoveAt(i);
            }
        }

        public void Render(SpriteBatch spriteBatch) {
            for (int i = 0; i < particles.Count; i++) {
                particles[i].Render(spriteBatch);
            }
        }

        public void Add(Particle particle) {
            particles.Add(particle);
        }

        public void Clear() {
            particles.Clear();
        }
    }
}