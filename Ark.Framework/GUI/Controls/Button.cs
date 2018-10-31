using Microsoft.Xna.Framework.Graphics;
using System;


namespace Ark.Framework.GUI.Controls
{
    public class Button : Control
    {
        #region [ Constructor ]
        public Button(ControlStyle style) : this("", style) { }

        public Button(string label, ControlStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this button.");
            }

            DefaultStyle = style;

            _render = new ControlRenderer(this);
            if (!string.IsNullOrEmpty(label) && style.FontStyle != null)
            {
                Label = new Label(label, style.FontStyle);
                Label.AnchorTo(this, Anchoring.AnchorAlignment.Inside_Middle_Center, 0, 0, Anchoring.AnchorType.Bounds);
            }
        }
        #endregion


        #region [ Members ]
        private ControlRenderer _render;
        public Label Label { get; set; }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                _render.Draw(spriteBatch);
                if (Label != null)
                {
                    Label.Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}
