using System;

namespace VectorTest
{
    public class Coordinate:IComparable<Coordinate>
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public int CompareTo(Coordinate other)
        {
            if (X.CompareTo(other.X) != 0) return X.CompareTo(Y);
            return Y.CompareTo(other.Y);
        }
    }
}
