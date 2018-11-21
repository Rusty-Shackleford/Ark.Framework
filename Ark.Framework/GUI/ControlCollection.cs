using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI
{
    public class ControlCollection : IList<Control>
    {
        #region [ Members ]
        private List<Control> _controls = new List<Control>();
        #endregion


        #region [ Constructor ]
        public ControlCollection()
        {

        }
        #endregion


        #region [ Custom? IList Implements ]
        public void Add(Control item)
        {
            // Refresh the item so that any pending calculations can be finalized,
            // get it's dependents, refresh those too
            // then add the item and dependents to our collection.
            item.Refresh();
            _controls.Add(item);
        }

        public void Insert(int index, Control item)
        {
            ((IList<Control>)_controls).Insert(index, item);
        }

        public bool Remove(Control item)
        {
            return ((IList<Control>)_controls).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Control>)_controls).RemoveAt(index);
        }

        public Control GetControl(Control match)
        {
            if (_controls.Count == 0)
                return null;

            if (_controls.Contains(match))
            {
                for (int i = 0; i < _controls.Count; i++)
                {
                    if (_controls[i] == match)
                    {
                        return match;
                    }
                }
            }
            return null;
        }


        public Control FindControlByName(string name)
        {
            var f = _controls.Find(n => n.Name == name);
            return f ?? null;
        }
        #endregion


        #region [ Queries ]
        public float CalcTotalHeight()
        {
            return Math.Abs(FindBottomControl().Bounds.Y - FindTopControl().Bounds.Top);
        }

        public float CalcTotalWidth()
        {
            return Math.Abs(FindRightmostControl().Bounds.Right - FindLeftmostControl().Bounds.Left);
        }

        public Vector2 CalcTotalSize(Vector2 position)
        {
            return new Vector2(CalcTotalWidth(), CalcTotalHeight());
        }

        public Control GetItemAtPoint(Point point)
        {
            return _controls.LastOrDefault(e => e.InteractiveBounds.Contains(point));
        }

        public List<Control> GetItemsAtPoint(Point point)
        {
            return _controls.FindAll(e => e.InteractiveBounds.Contains(point));
        }

        private Control FindItem(Func<Control, Control, bool> compare)
        {
            if (_controls.Count == 0)
                return default(Control);

            if (_controls.Count == 1)
                return _controls[0];

            Control found = _controls[0];
            for (int i = 1; i < _controls.Count; i++)
            {
                if (compare(_controls[i], found))
                {
                    found = _controls[i];
                }
            }
            return found;
        }

        public List<Control> FindControlsInViewport(Rectangle viewport)
                {
                    List<Control> found = new List<Control>();
                    for (int i = 0; i < _controls.Count; i++)
                    {
                        if (viewport.Contains(_controls[i].Position))
                        {
                            found.Add(_controls[i]);
                        }
                    }
                    return found;
                }

        public Control FindRightmostControl()
        {
            return FindItem((a, b) => a.Bounds.Right > b.Bounds.Right);
        }

        public Control FindLeftmostControl()
        {
            return FindItem((a, b) => a.Bounds.Left < b.Bounds.Left);
        }

        public Control FindTopControl()
        {
            return FindItem((a, b) => a.Bounds.Top < b.Bounds.Top);
        }

        public Control FindBottomControl()
        {
            return FindItem((a, b) => a.Bounds.Bottom > b.Bounds.Bottom);
        }
        #endregion


        #region [ Default IList Implementation ]
        public Control this[int index]
        {
            get => ((IList<Control>)_controls)[index]; set => ((IList<Control>)_controls)[index] = value;
        }

        public int Count => ((IList<Control>)_controls).Count;

        public bool IsReadOnly => ((IList<Control>)_controls).IsReadOnly;

        public void Clear()
        {
            ((IList<Control>)_controls).Clear();
        }

        public bool Contains(Control item)
        {
            return ((IList<Control>)_controls).Contains(item);
        }

        public void CopyTo(Control[] array, int arrayIndex)
        {
            ((IList<Control>)_controls).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Control> GetEnumerator()
        {
            return ((IList<Control>)_controls).GetEnumerator();
        }

        public int IndexOf(Control item)
        {
            return ((IList<Control>)_controls).IndexOf(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Control>)_controls).GetEnumerator();
        }

        #endregion
    }
}
