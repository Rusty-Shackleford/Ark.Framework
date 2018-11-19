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
        #region [ Constructor ]
        public Button(ControlStyle style) : base(style)
        {

        }

        public override void Refresh()
        {
            Label.Refresh();
            base.Refresh();
        }
        #endregion


        #region [ Members ]
        protected override ControlStyle CurrentStyle()
        {
            return _currentStyle;
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