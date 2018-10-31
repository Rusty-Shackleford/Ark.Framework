using Microsoft.Xna.Framework;

namespace Ark.Framework.GUI.Anchoring
{
    public class AnchorMovedArgs
    {
        public Vector2 DistanceMoved { get; private set; }

        public AnchorMovedArgs(Vector2 distanceMoved)
        {
            DistanceMoved = distanceMoved;
        }
    }
}
