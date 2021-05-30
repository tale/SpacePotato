using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpacePotato {
    public class Spinner : Component {
        private readonly Texture2D _texture;
        private Vector2 _position;
        private float _rotation;
        private float _scale;

        public Spinner(ContentManager manager) {
            _texture = manager.Load<Texture2D>("Images/Common/mars_planet");
            _rotation = 0F;
        }

        public override void Update(GameTime gameTime) {
            if (Math.Abs(_rotation - 360F) < 0) _rotation = 0F;
            _rotation+= 0.1F;
        }

        public void Render(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float scale) {
            _position = position;
            _scale = scale;
            Render(spriteBatch);
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, null, Color.White, _rotation,
                new Vector2(_texture.Width / 2F, _texture.Height / 2F), _scale, SpriteEffects.None, 0);
        }
    }
}
