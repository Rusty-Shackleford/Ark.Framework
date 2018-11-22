using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;


namespace Ark.Framework.GUI.Controls
{
    public class Label : Control
    {
        #region [ Members ]
        // As there is a "Text" field within all controls that controls a Label,
        // the actual value of a label needs to be stored in it's own variable.
        private string _text;
        public new string Text
        {
            get { return _text; }
            //TODO: Positioning with Anchor is off - need to check timing.
            set
            {
                if (value != _text)
                {
                    Size oldSize = new Size(Width, Height);
                    _text = value;
                    Refresh();
                    OnResized(new AnchorResizedArgs(oldSize, new Size(Width, Height)));
                }
            }
        }
        
        public override int Width => (int)DefaultStyle.Font.MeasureString(_text).Width;
        public override int Height => (int)DefaultStyle.Font.MeasureString(_text).Height;
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
