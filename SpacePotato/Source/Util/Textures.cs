using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePotato.Source.Util;

namespace SpacePotato {
    public static class Textures {

        public static Texture2D rect = genRect(Color.White);
        
        public static Texture2D genRect(Color rectColor) {
            Texture2D rect = new Texture2D(SpacePotatoGame.getGraphicsDevice(), 1, 1);
            rect.SetData(new[] {rectColor});
            return rect;
        }

        public static Texture2D genPlanetShade() {
            Texture2D texture = Loader.texture("Common/ShaderTexture");

            return grayscale(texture);
        }

        public static Texture2D scaleDarken(Texture2D texture) {

            var colorData = new Color[texture.Width * texture.Height];
            texture.GetData(colorData);
            float min = 1;
            for (int i = 0; i < colorData.Length; i++) {
                Color color = colorData[i];
                float mag = color.R/255F;
                min = Math.Min(min, mag);
            }
            
            for (int i = 0; i < colorData.Length; i++) {
                Color color = colorData[i];
                float col = color.R / 255F;
                col = (col - min) / min;
                colorData[i] = new Color(col, col, col, color.A/255F);
            }
            
            Texture2D newTexture = new Texture2D(SpacePotatoGame.getGraphicsDevice(), texture.Width, texture.Height);
            newTexture.SetData(colorData);
            return newTexture;
        }
        
        public static Texture2D grayscale(Texture2D texture) {

            var colorData = new Color[texture.Width * texture.Height];
            texture.GetData(colorData);
            for (int i = 0; i < colorData.Length; i++) {
                Color color = colorData[i];
                float gray = (color.R/255F + color.B/255F + color.G/255F) / 3F;
                colorData[i] = Color.Lerp(Color.White, new Color(gray, gray, gray), color.A/255F);
            }
            
            Texture2D newTexture = new Texture2D(SpacePotatoGame.getGraphicsDevice(), texture.Width, texture.Height);
            newTexture.SetData(colorData);
            return newTexture;
        }
    }
}