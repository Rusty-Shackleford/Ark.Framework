using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    #region [ AnchorMovedArgs ]
    public class AnchorMovedArgs : EventArgs
    {
        public Vector2 DistanceMoved { get; private set; }

        public AnchorMovedArgs(Vector2 distanceMoved)
        {
            DistanceMoved = distanceMoved;
        }
    }
    #endregion

    #region [ AnchorResizedArgs ]
    public class AnchorResizedArgs : EventArgs
    {
        public Size OldSize { get; private set; }
        public Size NewSize { get; private set; }
        
        public AnchorResizedArgs(Size oldSize, Size newSize)
        {
            OldSize = oldSize;
            NewSize = newSize;
        }
    }
    #endregion

}
