using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public class AnchorComponent
    {
        public AnchorComponent(IAnchorable target, IAnchorable owner, AnchorType anchorType, PositionType positionType, PositionOffset offset)
        {
            _target = target;
            _owner = owner;
            _anchorType = anchorType;
            _positionType = positionType;
            _anchorOffset = offset;
            _target.OnPositionChanged += AnchorMoved;
        }

        private IAnchorable _owner;
        private IAnchorable _target;

        private readonly PositionType _positionType;
        public PositionType PositionType
        {
            get { return _positionType; }
        }

        private readonly AnchorType _anchorType;
        public AnchorType AnchorType
        {
            get { return _anchorType; }
        }

        private readonly PositionOffset _anchorOffset;
        public PositionOffset AnchorOffset
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
        private void AnchorMoved(object sender, AnchorMovedArgs e)
        {
            _owner.Move(e.DistanceMoved);
        }


        public void RemoveAnchor()
        {
            _target.OnPositionChanged -= AnchorMoved;
        }



    }
}
