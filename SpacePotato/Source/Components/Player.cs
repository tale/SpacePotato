using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class Player : Component {
        public Texture2D texture;
        public Vector2 pos, vel, dimen;
        public float rot, scale;

        public Player(Vector2 pos) {
            texture = Loader.texture("Common/image");
            rot = 0F;

            this.pos = pos;
            dimen = new Vector2(100, 100);
        }

        public override void Update(GameTime gameTime) {
            
        }

        public override void Draw(SpriteBatch spriteBatch) {
            
            //spriteBatch.Draw(texture, pos, null, Color.White, rot, new Vector2(texture.Width / 2F, texture.Height / 2F), scale, SpriteEffects.None, 0);
            
            spriteBatch.Draw(texture, new Rectangle((int)(pos.X - dimen.X / 2F), (int)(pos.Y - dimen.Y / 2F), 100, 100), Color.White);
        }
    }
}
