using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class Camera {

        public static Vector2 screenCenter, screenDimen;
        
        public Camera(Viewport viewport) {
            Rotation = 0;
            Zoom = 0.5F;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;

            screenCenter = Origin;
            screenDimen = Origin * 2;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Matrix CalculateViewMatrix() {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public Vector2 toWorld(Vector2 mousePos) {
            return (mousePos - Origin) / Zoom + Position + Origin;
        }
    }
}
