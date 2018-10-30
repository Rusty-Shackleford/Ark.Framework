using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    public class Size : IEquatable<Size>
    {
        #region [ Members ]
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion


        #region [ Constructor ]
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion


        #region [ IEquatable<Size> ]
        public bool Equals(Size other)
        {
            if (Width == other.Width && Height == other.Height)
                return true;
            return false;
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }


        public static bool operator ==(Size s1, Size s2)
        {
            return s1.Equals(s2);
        }


        public static bool operator !=(Size s1, Size s2)
        {
            return !s1.Equals(s2);
        }
        #endregion


    }
}
