using System;

namespace Sequence
{
    class WeightBinNode<T> : IComparable<WeightBinNode<T>> where T : IComparable<T>
    {
        public BinNode<T> Node { get; set; }

        public Double Weight { get; set; }

        public int CompareTo(WeightBinNode<T> other)
        {
            return Node.CompareTo(other.Node);
        }
    }
}
