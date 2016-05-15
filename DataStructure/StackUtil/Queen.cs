using System;

namespace Sequence
{
    public class Queen:IComparable<Queen>
    {
        public int X { get;  set; }
        public int Y { get;  set; }

        public Queen(int x, int y)
        {
            X = x;
            Y = y;  
        }

        public int CompareTo(Queen other)
        {
            if (X == other.X || Y == other.Y || X + Y == other.X + other.Y
                || (X - Y) == (other.X - other.Y))
                return 0;
            return -1;
        }

        public override string ToString()
        {
            return "X:" + X + " Y:" + Y;
        }
    }
}
