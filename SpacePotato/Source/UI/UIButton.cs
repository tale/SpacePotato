using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class UIButton : UIElement {
        
        public Action clickFunc;

        public bool hoverGrow;
        public float hoverTime;
        public const float hoverSpeed = 5;
        public const float hoverMult = 1.1F;
        public string text;
        public Color color = Color.Lerp(Color.White, Color.Black, 0.9F);
        public Color textColor = Color.White;
        public SpriteFont font = Fonts.arial;

        public bool tintHover = true;
        public Color hoverTint = Color.White;
        public float hoverTintAmount = 0.1F, tintAmount;

        public UIButton(Action clickFunc, Vector2 pos, Vector2 dimen, string text = "") {
            this.clickFunc = clickFunc;
            this.pos = pos;
            this.dimen = dimen;
            this.text = text;
            
            startPos = pos;
            startDimen = dimen;
            
            texture = Textures.rect;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, drawRect(), Color.Lerp(color, hoverTint, tintAmount));
            Vector2 nameDimen = font.MeasureString(text);
            spriteBatch.DrawString(font, text, pos - nameDimen / 2, textColor);
        }

        public override void Update(MouseInfo mouse, KeyInfo keys, float deltaTime) {

            hoverTime = Math.Clamp(hoverTime, 0, 1);
            if (hoverGrow) {
                dimen = Util.sinLerp(hoverTime, startDimen, startDimen * hoverMult);
            }

            if (tintHover) {
                tintAmount = Util.sinSmooth(Math.Clamp(hoverTime * 2, 0, 1), 1) * hoverTintAmount;
            }

            base.Update(mouse, keys, deltaTime);
        }

        public override void hovered(MouseInfo mouse, KeyInfo keys, float deltaTime) {
            hoverTime += deltaTime * hoverSpeed;
        }

        public override void notHovered(MouseInfo mouse, KeyInfo keys, float deltaTime) {
            hoverTime -= deltaTime * hoverSpeed;
        }
        
        public override void clicked(MouseInfo mouse, KeyInfo keys, float deltaTime) {
            clickFunc?.Invoke();
        }
    }
}