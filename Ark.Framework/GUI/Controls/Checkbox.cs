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
        public ControlStyle CheckedStyle { get; set; }
        public ControlStyle HoveredChecked { get; set; }
        public ControlStyle HoveredCheckedPressedStyle { get; set; }
        #endregion


        #region [ Constructor ]
        public Checkbox(ControlStyle style, ControlStyle chk, ControlStyle chkHov, ControlStyle chkHovPress) : base(style)
        {
            CheckedStyle = chk;
            HoveredChecked = chkHov;
            HoveredCheckedPressedStyle = chkHovPress;
        }
        #endregion


        public override void UpdateStyle()
        {
            // Handle default styles:
            base.UpdateStyle();

            // Apply custom styles if needed:
            // Checked
            if (IsChecked && !Hovered && !Pressed)
                CurrentStyle = CheckedStyle;
            // HoveredChecked
            if (IsChecked && Hovered && !Pressed)
                CurrentStyle = HoveredChecked;
            // HoveredCheckedPressed
            if (IsChecked && Hovered && Pressed)
                CurrentStyle = HoveredCheckedPressedStyle;
        }


        #region [ Check Events ]
        public bool IsChecked { get; protected set; }

        public event EventHandler Checked;
        public event EventHandler Unchecked;


        public override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled && Pressed)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    Pressed = false;
                    if (IsChecked)
                    {
                        IsChecked = false;
                        UpdateStyle();
                        Unchecked?.Invoke(this, EventArgs.Empty);
                        return;
                    }

                    if (!IsChecked)
                    {
                        IsChecked = true;
                        UpdateStyle();
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
                spriteBatch.Draw(CurrentStyle.Texture, Position, Color.White);
                Label?.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
