using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public class LevelScreen : GameScreen {
        private SpriteFont titleFont, font;
        public Texture2D potato;
        public float rot, timePassed, sinAmount;

        public List<UIElement> uiElements;

        public LevelScreen(Game game, int screenId) : base(game, screenId) {
            titleFont = ContentManager.Load<SpriteFont>("Fonts/Title");
            font = ContentManager.Load<SpriteFont>("Fonts/Normal");
            potato = Loader.texture("Common/Potato");

            uiElements = new List<UIElement> {
                new UIButton(() => ScreenManager.SwitchScreen(2), Camera.screenDimen * new Vector2(0, 1F) + new Vector2(80, -80), new Vector2(70, 70), "Back")
            };
            
            LevelManager.LoadLevels();

            Action Func(short i) {
                Action func = () => {
                    LevelManager.SetLevel(i);
                    ScreenManager.SwitchScreen(1);
                };
                return func;
            }

            const int cols = 3;
            for (short i = 0; i < LevelManager._levels.Length; i++) {
                int col = i % cols;
                int row = i / cols;
                uiElements.Add(new UIButton(Func(i), new Vector2(Camera.screenCenter.X + (col - (cols - 1) / 2F) * 120, 100 + row * 120), Vector2.One * 100, $"Level {i+1}"));
            }
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
            
            foreach (var uiElement in uiElements) {
                uiElement.Render(spriteBatch);
            }
        }
    }
}