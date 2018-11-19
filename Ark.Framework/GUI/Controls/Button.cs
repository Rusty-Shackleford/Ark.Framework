using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Framework.GUI.Anchoring;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;

namespace Ark.Framework.GUI.Controls
{
    public class Button : Control, IClickable
    {
        #region [ MakeClone ]
        public override Control MakeClone()
        {
            Button c = new Button(DefaultStyle);
            c.HoveredStyle = HoveredStyle;
            c.PressedStyle = PressedStyle;
            c.Position = new Vector2(Position.X + 10, Position.Y + 10);
            return c;
        }
        #endregion


        #region [ Constructor ]
        public Button(ControlStyle style) : this(style, Vector2.Zero) { }
        public Button(ControlStyle style, string label) : this(style, Vector2.Zero, label) { }
        public Button(ControlStyle style, Vector2 position, string label = "") : base(style)
        {
            Position = position;
            Label = new Label(GetCurrentStyle(), label);
            Label.AnchorTo(this, AnchorAlignment.Inside_Middle_Center, PositionOffset.Zero);
        }


        public override void Refresh()
        {
            Label.Refresh();
            base.Refresh();
        }
        #endregion


        #region [ Members ]
        public Label Label { get; protected set; }
        protected override ControlStyle CurrentStyle()
        {
            return currentStyle;
        }
        #endregion


        #region [ Anchoring ]
        public override Rectangle GetAnchorBounds()
        {
            return GetCurrentStyle().AnchoringOffset.Apply(Position, GetCurrentStyle().Size);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(GetCurrentStyle().Texture, Position, Color.White);
                Label.Draw(spriteBatch);
            }
        }
        #endregion
    }
}