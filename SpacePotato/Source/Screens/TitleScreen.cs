using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class TitleScreen : GameScreen {
        private SpriteFont titleFont, font;
        public Texture2D potato;
        public float rot, timePassed, sinAmount;

        public List<UIElement> uiElements;

        public TitleScreen(Game game, int screenId) : base(game, screenId) {
            titleFont = ContentManager.Load<SpriteFont>("Fonts/Title");
            font = ContentManager.Load<SpriteFont>("Fonts/Normal");
            potato = Loader.texture("Common/Potato");

            uiElements = new List<UIElement> {
                new UIButton(() => ScreenManager.SwitchScreen(1), Camera.screenDimen * 0.5F, new Vector2(200, 70), "Start") {
                    color = Color.Lerp(Color.White, Color.Black, 0.9F),
                },
                new UIButton(() => ScreenManager.SwitchScreen(3), Camera.screenDimen * 0.5F + Vector2.UnitY * 90, new Vector2(200, 70), "Levels") {
                    color = Color.Lerp(Color.White, Color.Black, 0.9F),
                },
                new UIButton(SpacePotatoGame.ExitGame, Camera.screenDimen * 0.5F + Vector2.UnitY * 90 * 2, new Vector2(200, 70), "Exit") {
                    color = Color.Lerp(Color.White, Color.Black, 0.9F),
                },
            };
        }

        public override void Update(GameTime gameTime, KeyInfo keys, MouseInfo mouse) {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            timePassed += deltaTime;
            sinAmount = (float) Math.Sin(timePassed * Maths.halfPI);
            rot = sinAmount * Maths.PI * 0.2F;

            foreach (var uiElement in uiElements) {
                uiElement.Update(mouse, keys, deltaTime);
            }
        }

        public override void BasicDraw(GameTime gameTime, SpriteBatch spriteBatch) {
            var (strX, _) = titleFont.MeasureString("Grapple");
            var viewport = spriteBatch.GraphicsDevice.Viewport;

            spriteBatch.DrawString(titleFont, "Grapple", new Vector2(viewport.Width / 2F, 0),
                Color.White, 0, new Vector2(strX / 2, 0), Vector2.One, SpriteEffects.None, 0);
            
            
            spriteBatch.Draw(potato, new Vector2(viewport.Width * 0.15F, viewport.Height * 0.7F) + Vector2.UnitX * 100 * sinAmount, null, Color.White, rot,
                new Vector2(potato.Width / 2F, potato.Height / 2F), 10, SpriteEffects.None, 0);
            
            spriteBatch.Draw(potato, new Vector2(viewport.Width * 0.85F, viewport.Height * 0.7F) + Vector2.UnitX * 100 * sinAmount, null, Color.White, rot,
                new Vector2(potato.Width / 2F, potato.Height / 2F), 10, SpriteEffects.FlipHorizontally, 0);

            foreach (var uiElement in uiElements) {
                uiElement.Render(spriteBatch);
            }
        }
    }
}