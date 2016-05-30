using System;

namespace Sequence
{
    [Serializable]
    public class HuffChar : IComparable<HuffChar>
    {
        public Char Ch { get; set; }
        public Double Weight { get; set; }
        public HuffChar(char c = '^', double w = 0.0)
        {
            Ch = c;
            Weight = w;
        }

        public int CompareTo(HuffChar other)
        {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return Ch + ":" + Weight;
        }
    }
}
