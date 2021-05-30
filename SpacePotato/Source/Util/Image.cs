using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato.Source.Util {
    
        public class Image {

            private Texture2D _texture;

            private Vector2 _position, _dimensions;

            public Image(float x, float y, float width, float height, Texture2D texture) {

                _position.X = x;
                _position.Y = y;

                _dimensions.X = width;
                _dimensions.Y = height;

                _texture = texture;
            }
            public void Render(SpriteBatch spriteBatch, int scaler = 1) {
                
                int moveScalar = (scaler == 2) ? 3 : 2;
                spriteBatch.Draw(_texture, new Rectangle(
                    (int)(_position.X - _dimensions.X / 2 + Camera.Position.X / moveScalar),
                    (int)(_position.Y - _dimensions.Y / 2 + Camera.Position.Y / moveScalar),
                    (int)(_dimensions.X), (int)(_dimensions.Y)), scalarToTint(scaler));
            }

            public static Color scalarToTint(int scalar) {

                if (scalar == 2) return Color.Lerp(Color.White, Color.Black, 0.3F);
                if (scalar == 3) return Color.Lerp(Color.White, Color.Black, 0.5F);
                
                return Color.White;
            }
        }
}
    