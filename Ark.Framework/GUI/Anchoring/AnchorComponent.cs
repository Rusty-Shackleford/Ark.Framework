using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public class AnchorComponent
    {
        public AnchorComponent(IAnchorable target, IAnchorable owner, AnchorType anchorType, PositionType positionType, Size2 offset)
        {
            _target = target;
            _owner = owner;
            _anchorType = anchorType;
            _positionType = positionType;
            _anchorOffset = offset;
            _target.OnPositionChanged += AnchorMoved;
            _target.OnDimmensionChanged += DimmensionChange;
            InitialPlacement();
        }

        private IAnchorable _owner;
        private IAnchorable _target;

        private PositionType _positionType;
        public PositionType PositionType
        {
            get { return _positionType; }
        }

        private AnchorType _anchorType;
        public AnchorType AnchorType
        {
            get { return _anchorType; }
        }

        private Size2 _anchorOffset;
        public Size2 AnchorOffset
        {
            get { return _anchorOffset; }
        }

        private Rectangle _targetRectangle
        {
            get
            {
                return _target.Bounds;

                //switch (_anchorType)
                //{
                //    case AnchorType.Bounds:
                //        return _target.Bounds;
                //    case AnchorType.VirtualBounds:
                //        return _target.VirtualBounds;
                //    default:
                //        return _target.Bounds;
                //}
            }
        }

        public Vector2 AnchoredPosition()
        {
            return Anchoring.GetPosition(_targetRectangle, _owner.Bounds, _positionType, _anchorOffset);
        }

        /// <summary>
        /// Moved the anchored item the same distance its anchor moved.
        /// </summary>
        /// <param name="sender">Anchor</param>
        /// <param name="e">PositionChangedArgs</param>
        private void AnchorMoved(object sender, PositionChangedArgs e)
        {
            _owner.Move(e.DistanceMoved);
        }

        private void DimmensionChange(object sender, EventArgs e)
        {
            //NOTE: TEST THIS - not sure if correct!
            InitialPlacement();
        }

        public void InitialPlacement()
        {
            _owner.MoveTo(AnchoredPosition());
        }

        public void RemoveAnchor()
        {
            _target.OnPositionChanged -= AnchorMoved;
        }



    }
}
