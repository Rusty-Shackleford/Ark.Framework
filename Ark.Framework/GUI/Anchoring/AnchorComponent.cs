using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public class AnchorComponent
    {
        #region [ Members ]
        private IAnchorable _owner;
        private IAnchorable _target;

        public AnchorAlignment Alignment { get; set; }
        public PositionOffset Offset { get; set; }

        public Vector2 AnchoredPosition
        {
            get { return Anchor.GetPosition(_target.GetAnchorBounds(), _owner.GetAnchorBounds(), Alignment, Offset); }
        }
        #endregion


        #region [ Constructor ]
        /// <summary>
        /// Anchor this control to another.
        /// </summary>
        /// <param name="target">anchor to</param>
        /// <param name="owner">this object</param>
        /// <param name="alignment">alignment type</param>
        /// <param name="offset">position offset</param>
        public AnchorComponent(IAnchorable target, IAnchorable owner, AnchorAlignment alignment, PositionOffset offset)
        {
            _target = target;
            _owner = owner;
            Alignment = alignment;
            Offset = offset;
            _target.OnPositionChanged += OnAnchorMoved;
        }
        #endregion


        #region [ AnchorMoved ]
        /// <summary>
        /// When the anchor moves, move it's children the same distance.
        /// </summary>
        /// <param name="sender">Anchor</param>
        /// <param name="e">PositionChangedArgs</param>
        private void OnAnchorMoved(object sender, AnchorMovedArgs e)
        {
            _owner.Position += e.DistanceMoved;
        }
        #endregion


        #region [ RemoveAnchor ]
        /// <summary>
        /// Unsubscribe this object from Anchoring Events
        /// </summary>
        public void RemoveAnchor()
        {
            _target.OnPositionChanged -= OnAnchorMoved;
        }
        #endregion


        #region [ ToString ]
        public override string ToString()
        {
            string type = GetType().Name;
            return $"[Type: {type}|" +
                $"OwnedBy: {_owner.Name}|" +
                $"AnchoredTo: {_target.Name}|" +
                $"Align: {Alignment.ToString()}" +
                $"Offset: {Offset.ToString()}|]";
        }
        #endregion


    }
}
