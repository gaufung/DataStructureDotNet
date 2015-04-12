using System;

namespace Graph
{
    /// <summary>
    /// 边表示方式，在Prim算法中使用
    /// </summary>
    public class PrimEdge:IComparable
    {
        public int FirstVertex { get; set; }
        public int SecondVertex { get; set; }

        public int Weight { get; set; }
        public override string ToString()
        {
            return string.Format("边：第一个点序号{0}，第二点序号{1},权重{2}", FirstVertex, SecondVertex,Weight);
        }

        public int CompareTo(object obj)
        {

            return Weight.CompareTo(((PrimEdge) obj).Weight);
        }
    }
}
