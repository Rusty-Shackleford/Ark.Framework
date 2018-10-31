using Microsoft.Xna.Framework;

namespace Ark.Framework.GUI.Anchoring
{
    //public class PositionChangedArgs
    //{
    //    public Vector2 OldPosition { get; private set; }
    //    public Vector2 NewPosition { get; private set; }
    //    public Vector2 DistanceMoved => NewPosition - OldPosition;

    //    public PositionChangedArgs(Vector2 oldPosition, Vector2 newPosition)
    //    {
    //        OldPosition = oldPosition;
    //        NewPosition = newPosition;

    //    }
    //}
    public class AnchorMovedArgs
    {
        public Vector2 DistanceMoved { get; private set; }

        public AnchorMovedArgs(Vector2 distanceMoved)
        {
            DistanceMoved = distanceMoved;
        }
    }
}
