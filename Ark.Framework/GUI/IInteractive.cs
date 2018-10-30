using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI
{
    public interface IInteractive
    {
        event EventHandler OnGainedFocus;
        event EventHandler OnLostFocus;
        event EventHandler OnClicked;
        event EventHandler OnDimmensionChanged;
        event EventHandler OnMouseOver;
        event EventHandler OnMouseOut;

        void Press(MouseEventArgs e);
        void Click(MouseEventArgs e);
        void MouseOut(MouseEventArgs e);
        void MouseOver(MouseEventArgs e);
    }
}
