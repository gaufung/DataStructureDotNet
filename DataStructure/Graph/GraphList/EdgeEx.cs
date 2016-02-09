using System;

namespace Sequence.GraphList
{
    /// <summary>
    /// 边类的扩展,增加了指向下一个边的指针,和Destination结点的序号
    /// </summary>
    /// <typeparam name="TE"></typeparam>
    /// <typeparam name="TW"></typeparam>
    internal class EdgeEx<TE,TW>:
        Edge<TE,TW> where TE:IComparable<TE> 
        where TW:IComparable<TW>
    {
        /// <summary>
        /// 指向下一个边的指针
        /// </summary>
        public EdgeEx<TE,TW> NextEdge { get; set; }

        public int Destination { get; set; }

        public EdgeEx(int destination=-1,TE d = default(TE), TW w = default(TW)):base(d,w)
        {
            Destination = destination;
            NextEdge = null;
        }
    }
}
