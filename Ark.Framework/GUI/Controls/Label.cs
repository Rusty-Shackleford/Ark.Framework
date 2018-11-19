using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoGame.Extended.BitmapFonts;
using Ark.Framework.GUI.Anchoring;

namespace Ark.Framework.GUI.Controls
{
    public class Label : Control
    {
        #region [ Members ]
        // As there is a "Text" field within all controls that controls a Label,
        // the actual value of a label needs to be stored in it's own variable.
        private string _value;
        public string Value
        {
            get { return _value; }
            //TODO: Positioning with Anchor is off - need to check timing.
            set
            {
                if (value != _value)
                {
                    Size oldSize = new Size(Width, Height);
                    _value = value;
                    Refresh();
                    OnResized(new AnchorResizedArgs(oldSize, new Size(Width, Height)));
                }
            }
        }
        
        public override int Width => (int)DefaultStyle.Font.MeasureString(_value).Width;
        public override int Height => (int)DefaultStyle.Font.MeasureString(_value).Height;
        protected override ControlStyle CurrentStyle()
        {
            return _currentStyle;
        }
        #endregion


        #region [ Constructor ]
        public Label(ControlStyle style) : base(style) { }
        public Label(ControlStyle style, string text) : base(style)
        {
            _value = text;
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

            if (Visible && !string.IsNullOrEmpty(Value))
            {
                spriteBatch.DrawString(GetCurrentStyle().Font, Value, Position, GetCurrentStyle().FontColor);
            }
        }

        #endregion


    }
}
