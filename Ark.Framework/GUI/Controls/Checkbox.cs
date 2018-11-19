using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace Ark.Framework.GUI.Controls
{
    public class Checkbox : Control
    {
        #region [ Members ]
        protected override ControlStyle CurrentStyle()
        {
            return _currentStyle;
            //if (Hovered && IsChecked)
            //    return CheckedHoveredStyle;
            //if (IsChecked)
            //    return CheckedStyle;
            //if (Hovered)
            //    return HoveredStyle;
            //return DefaultStyle;
        }
        public ControlStyle CheckedStyle { get; set; }
        public ControlStyle CheckedHoveredStyle { get; set; }
        #endregion


        #region [ Constructor ]
        public Checkbox(ControlStyle style, ControlStyle ckStyle, ControlStyle ckHovStyle) : base(style)
        {
            CheckedStyle = ckStyle;
            CheckedHoveredStyle = ckHovStyle;
        }
        #endregion


        #region [ Check Events ]
        public bool IsChecked { get; protected set; }

        public event EventHandler Checked;
        public event EventHandler Unchecked;

        public override void OnMouseUp(MouseEventArgs e)
        {
            // if mouse is within interactive bounds and pressed and enabled
            // if we WERE checked
            // TODO: Left off Here!  The Styles and events aren't right for checkbox.
            // Part of the problem is CurrentStyle() function... disagreements between that and
            // the OnEvent methods as well as the ones offered by base class... those 
            // may need to be overwritten.
            if (Enabled && Pressed)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    Pressed = false;
                    if (IsChecked)
                    {
                        IsChecked = false;
                        SetCurrentStyle(HoveredStyle);
                        Unchecked?.Invoke(this, EventArgs.Empty);
                        return;
                    }

                    if (!IsChecked)
                    {
                        IsChecked = true;
                        SetCurrentStyle(CheckedHoveredStyle);
                        Checked?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }
            }
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(GetCurrentStyle().Texture, Position, Color.White);
                Label?.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
