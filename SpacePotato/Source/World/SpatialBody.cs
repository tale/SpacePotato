using Microsoft.Xna.Framework;

namespace SpacePotato {
    public class SpatialBody {
        public Vector2 pos { get; set; }
        public float radius { get; set; }
        
        public float Diameter => radius * 2;


        public virtual void Update(float deltaTime) {}
    }
}