using Microsoft.Xna.Framework;

namespace SpacePotato.Source.Editor {
    public class PlanetData {
        public Vector2 pos { get; set; }
        public float radius { get; set; }
        public float mass { get; set; }
        public float angle { get; set; }
        public int typeIndex { get; set; }

        public PlanetData(Planet planet) {
            pos = planet.pos;
            radius = planet.radius;
            mass = planet.mass;
            angle = planet.angle;
            typeIndex = planet.typeIndex;
        }

        public Planet create() {
            return new Planet(pos, radius, mass, angle) { typeIndex = typeIndex, texture = Planet.textures[typeIndex]};
        }
    }
}