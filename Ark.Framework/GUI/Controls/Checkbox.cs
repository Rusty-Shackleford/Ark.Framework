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
            set
            {
                if (value != _text)
                {
                    _text = value;
                    Label = new Label(DefaultStyle, _text);
                    Label.AnchorTo(this, Anchoring.AnchorAlignment.Outside_Right_Middle, new PositionOffset(10, 0));
                }
            }
        }
        private Label Label { get; set; }
        protected override ControlStyle CurrentStyle()
        {
            if (Hovered && IsChecked)
                return CheckedHoveredStyle;
            if (IsChecked)
                return CheckedStyle;
            if (Hovered)
                return HoveredStyle;
            return DefaultStyle;
        }
        public ControlStyle CheckedStyle { get; set; }
        public ControlStyle CheckedHoveredStyle { get; set; }
        #endregion


        #region [ Constructor ]
        public Checkbox(ControlStyle style, ControlStyle checkedStyle) : base(style)
        {
            CheckedStyle = checkedStyle;
            CheckedHoveredStyle = HoveredStyle;
        }
        #endregion


        #region [ Check Events ]
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked && !value)
                {
                    Unchecked?.Invoke(this, EventArgs.Empty);
                    SetCurrentStyle(DefaultStyle);
                }
                if (!_isChecked && value)
                {
                    Checked?.Invoke(this, EventArgs.Empty);
                    SetCurrentStyle(CheckedStyle);
                }
                _isChecked = value;
            }
        }

        public event EventHandler Checked;
        public event EventHandler Unchecked;

        public override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled && Pressed)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    IsChecked = !IsChecked;
                }
            }
            base.OnMouseUp(e);
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
