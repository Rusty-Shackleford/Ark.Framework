using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI.Controls.States
{
    public abstract class ControlState<T> where T : Control
    {
        #region [ Members ]
        protected T _owner;
        #endregion

        #region [ Constructor ]
        protected ControlState(T owner) { _owner = owner; }
        #endregion

        #region [ Rule ]
        public abstract bool Applies();
        #endregion
    }


    public class DefaultState : ControlState<Control>
    {
        public DefaultState(Control owner) : base(owner) { }

        public override bool Applies()
        {
            if (_owner.Hovered == false && _owner.Pressed == false)
                return true;
            return false;
        }
    }

    public class CheckedState : ControlState<Checkbox>
    {
        public CheckedState(Checkbox owner) : base(owner) { }

        public override bool Applies()
        {
            if ( _owner.IsChecked == true && 
                _owner.Hovered == false && 
                _owner.Pressed == false)
            {
                return true;
            }
            return false;
        }
    }
}
