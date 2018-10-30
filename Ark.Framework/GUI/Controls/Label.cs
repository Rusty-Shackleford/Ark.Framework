using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Ark.Framework.GUI.Controls.Styles;

namespace Ark.Framework.GUI.Controls
{
    public class Label : Control
    {
        #region [ Constructor ]
        public Label(FontStyle style) : this("", style) { }
        public Label(string text, FontStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A FontStyle must be provided for this Label.");
            }
            Text = text;
            FontStyle = style;
            _render = new TextRenderer(this);
        }
        #endregion


        #region [ Members ]
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                }
            }
        }
        public FontStyle FontStyle { get; set; }
        private TextRenderer _render;


        public override int Height
        {
            get
            {
                return Bounds.Height;
            }
        }

        public override int Width
        {
            get
            {
                return Bounds.Width;
            }
        }

        public override Rectangle Bounds
        {
            get
            {
                var size = FontStyle.Font.MeasureString(Text);
                return new Rectangle((int)Position.X, (int)Position.Y, (int)size.Width, (int)size.Height);
            }
        }

        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && !string.IsNullOrEmpty(Text))
            {
                spriteBatch.Draw()
            }
        }
        #endregion
    }
}
