using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework
{
    /// <summary>
    /// Used to describe offsets to a rectangle's Left, Top, Right and Bottom attributes.
    /// </summary>
    public struct RectangleOffset : IEquatable<RectangleOffset>
    {
        #region [ Members ]
        /// <summary>
        /// Distance from Rectangle.Left
        /// </summary>
        public int Left_Offset { get; set; }
        /// <summary>
        /// Distance from a Rectangle.Top
        /// </summary>
        public int Top_Offset { get; set; }
        /// <summary>
        /// Distance from a Rectangle.Right
        /// </summary>
        public int Right_Offset { get; set; }
        /// <summary>
        /// Distance from a Rectangle.Bottom
        /// </summary>
        public int Bottom_Offset { get; set; }
        #endregion


        #region [ Constructor ]
        public RectangleOffset(int left, int top, int right, int bottom)
        {
            Left_Offset = left;
            Top_Offset = top;
            Right_Offset = right;
            Bottom_Offset = bottom;
        }
        public RectangleOffset(int allSides) : this(allSides, allSides, allSides, allSides) { }
        #endregion


        #region [ ApplyToRectangle ]
        public Rectangle Apply(Rectangle rect)
        {
            return new Rectangle(
                rect.X + Left_Offset,
                rect.Y + Top_Offset,
                rect.Width + (Left_Offset + Right_Offset),
                rect.Height + (Top_Offset + Bottom_Offset)
                );
        }

        public Rectangle Apply(Vector2 position, Rectangle rect)
        {
            return new Rectangle(
                (int)position.X + Left_Offset,
                (int)position.Y + Top_Offset,
                rect.Width + (Left_Offset + Right_Offset),
                rect.Height + (Top_Offset + Bottom_Offset)
                );
        }

        public Rectangle Apply(Vector2 position, Size size)
        {
            return new Rectangle(
                (int)position.X + Left_Offset,
                (int)position.Y + Top_Offset,
                size.Width + (Left_Offset + Right_Offset),
                size.Height + (Top_Offset + Bottom_Offset)
                );
        }
        #endregion


        #region [ Static Helpers ]
        public static RectangleOffset Zero
        {
            get { return new RectangleOffset(0, 0, 0, 0); }
        }

        public static RectangleOffset One
        {
            get { return new RectangleOffset(1, 1, 1, 1); }
        }
        #endregion


        #region [ ContainedWithin ]
        /// <summary>
        /// Determine if this RectangleOffset's application to a given Rectangle will be contained within it.
        /// </summary>
        /// <param name="r">Rectangle to test</param>
        /// <returns></returns>
        public bool ContainedWithin(Rectangle r)
        {
            Rectangle testRect = new Rectangle(
                r.X + Left_Offset,
                r.Y + Top_Offset,
                r.Right + Right_Offset,
                r.Bottom + Bottom_Offset
                );

            return r.Contains(testRect);
        }
        #endregion


        #region [ IEquatable ]
        public bool Equals(RectangleOffset other)
        {
            return other.Left_Offset == Left_Offset &&
                other.Top_Offset == Top_Offset &&
                other.Right_Offset == Right_Offset &&
                other.Bottom_Offset == Bottom_Offset;
        }

        public override int GetHashCode() { return base.GetHashCode(); }
        public static bool operator ==(RectangleOffset r1, RectangleOffset r2) { return r1.Equals(r2); }
        public static bool operator !=(RectangleOffset r1, RectangleOffset r2) { return !r1.Equals(r2); }
        public override bool Equals(object obj) { return GetHashCode() == obj.GetHashCode(); }
        #endregion

    }
}
