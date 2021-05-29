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

                this._texture = texture;
            }
            public void Draw(SpriteBatch spriteBatch, int scaler = 1) {
                
                    spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(
                        (int)((_position.X - _dimensions.X / 2 + Camera.Position.X / scaler)),
                        (int)((_position.Y - _dimensions.Y / 2 + Camera.Position.Y / scaler)),
                        (int)(_dimensions.X),
                        (int)(_dimensions.Y)), Color.White);
            }
    }
}
    