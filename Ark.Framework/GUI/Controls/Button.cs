using Ark.Framework.GUI.Controls.Styles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;


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
        public Button(TextureControlStyle style) : base(style) { }
        public Button(TextureControlStyle style, Vector2 position) : base(style)
        {
            Position = position;
        }
        #endregion


        #region [ Members ]
        // public Label Label { get; set; }
        #endregion


        #region [ Events ]

        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(CurrentStyle.Texture, Position, Color.White);
            }
        }
        #endregion
    }
}