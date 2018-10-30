using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace Ark.Framework.GUI.Controls.Styles
{
    /// <summary>
    /// Font and color information used to render a Label Control.
    /// Note:  BitmapFonts are highly encouraged due to their quality.
    /// Ref: http://dylanwilson.net/bmfont-rendering-with-monogame-extended
    /// </summary>
    public class FontStyle
    {
        #region [ Constructor ]
        public FontStyle(BitmapFont font, Color color)
        {
            Font = font;
            Color = color;
        }
        public FontStyle(SpriteFont font, Color color)
        {
            SFont = font;
            Color = color;
        }
        public FontStyle(SpriteFont font) : this(font, Color.White) { }
        public FontStyle(BitmapFont font) : this(font, Color.White) { }
        #endregion


        #region [ Members ]
        /// <summary>
        /// BitmapFont used to render the label text.
        /// </summary>
        public BitmapFont Font { get; set; }

        /// <summary>
        /// The SpriteFont used to render the label text.
        /// </summary>
        public SpriteFont SFont { get; set; }

        /// <summary>
        /// The Color used to render the label text;
        /// </summary>
        public Color Color { get; set; }
        #endregion
    }
}
