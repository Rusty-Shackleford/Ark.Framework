using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoGame.Extended.BitmapFonts;


namespace Ark.Framework.GUI.Controls
{
    public class Label : Control
    {
        #region [ MakeClone ]
        public override Control MakeClone()
        {
            throw new NotImplementedException();
        }
        #endregion



        #region [ Members ]
        private string _text;
        public string Text
        {
            get { return _text; }
            //TODO: Positioning with Anchor is off - need to check timing.
            set
            {
                if (value != _text)
                {
                    _text = value;
                    Refresh();
                }
            }
        }

        public override int Width => (int)CurrentStyle.Font.MeasureString(_text).Width;
        public override int Height => (int)CurrentStyle.Font.MeasureString(_text).Height;
        #endregion


        #region [ Constructor ]
        public Label(ControlStyle style) : base(style) { }
        public Label(ControlStyle style, string text) : base(style)
        {
            _text = text;
        }
        #endregion


        #region [ GetAnchorBounds ]
        public override Rectangle GetAnchorBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && !string.IsNullOrEmpty(Text))
            {
                spriteBatch.DrawString(CurrentStyle.Font, Text, Position, CurrentStyle.FontColor);
            }
        }

        #endregion


    }
}
