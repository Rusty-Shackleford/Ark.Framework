using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Ark.Framework.Graphics
{
    public class TextureMaker
    {
        #region [ Constructor ]
        public TextureMaker(GraphicsDevice graphics)
        {
            _graphics = graphics;
        }
        #endregion


        #region [ Members ]
        private GraphicsDevice _graphics;
        #endregion


        #region [ Method: MakeTexture]
        public Texture2D MakeTexture(Color color, byte alpha, int width, int height)
        {
            Color c = new Color(color.R, color.G, color.B, alpha);
            Texture2D texture = new Texture2D(_graphics, width, height);
            Color[] textureData = new Color[width * height];
            for (int i = 0; i < textureData.Length; i++) textureData[i] = c;
            texture.SetData(textureData);
            return texture ?? null;
        }
        #endregion

        #region [ MakeBorder ]
        public Texture2D MakeBorder(Color color, byte alpha, Rectangle border)
        {
            // width = cols
            // height = rows
            Color borderColor = new Color(color.R, color.G, color.B, alpha);
            Color transparent = Color.Transparent;
            Texture2D texture = new Texture2D(_graphics, border.Width, border.Height);
            Color[] textureData = new Color[border.Width * border.Height];

            int idx = 0;

            // textureData[i] = borderColor;
            // textureData[i] = Color.Black;
            for (int row = 0; row < border.Height; row++)
            {
                for (int col = 0; col < border.Width; col++)
                {
                    if (row == 0 || row == border.Height - 1)
                    {
                        textureData[idx] = borderColor;
                        idx++;
                        continue;
                    }
                    if (col == 0 || col == border.Width - 1)
                    {
                        textureData[idx] = borderColor;
                        idx++;
                        continue;
                    }
                    textureData[idx] = transparent;
                    idx++;
                }
            }

            texture.SetData(textureData);
            return texture ?? null;
        }
        #endregion


        #region [ Method: MakeTextCursor]
        //public Texture2D MakeTextCursor(Color color, byte alpha, int width, int height)
        //{
        //    Color c = new Color(color.R, color.G, color.B, alpha);
        //    Color b = new Color(color.R, color.G, color.B, 0);
        //    Texture2D texture = new Texture2D(_graphics, width, height);
        //    Color[] textureData = new Color[width * height];

        //    for (int i = 0; i < textureData.Length; ++i)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            textureData[i] = c;
        //        }
        //        else
        //        {
        //            textureData[i] = b;
        //        }
        //    }
        //    texture.SetData(textureData);

        //    return (texture != null) ? texture : null;
        //}
        #endregion

    }
}
