using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato.Source.World {
    [XmlInclude(typeof(BlackHole))]

    public class BlackHole : Planet {
        
        private const float BlackHoleMass = 50000;
        private static readonly Texture2D BlackHoleTexture;

        static BlackHole() {
            BlackHoleTexture = Loader.texture("Common/BlackHole");
        }

        public BlackHole() {
            
            GravityStrengthModifier = .1f;
            GravityFalloffModifier = .003f;
        }

        public BlackHole(Vector2 pos, float radius, float mass = BlackHoleMass, float angle = 0) {
            
            this.radius = radius;
            this.pos = pos;

            this.mass = mass;
            this.angle = angle;

            typeIndex = -1;

            GravityStrengthModifier = .1f;
            GravityFalloffModifier = .01f;
        }

        public override void setUpSerialized() {
            texture = BlackHoleTexture;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(BlackHoleTexture, new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), (int) (radius * 2), (int) (radius * 2)), Color.White);
        }
        public override string GetType() {
            return "Blackhole";
        }
        
    }
}