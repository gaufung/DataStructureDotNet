using System;

namespace Sequence
{
    /// <summary>
    /// 边节点类
    /// </summary>
    /// <typeparam name="TE">边存储的数据</typeparam>
    /// <typeparam name="TW">边的权重</typeparam>
    internal class Edge<TE, TW> : IComparable<Edge<TE, TW>>
        where TE : IComparable<TE>
        where TW : IComparable<TW>
    {
        #region 属性

        public TE Data { get;  set; }
        public TW Weight { get; set; }
        public EStatus Status { get; set; } 
        #endregion

        public Edge(TE d=default(TE), TW w=default(TW))
        {
            Data = d;
            Weight = w;
            Status = EStatus.Undetermined;
        }

        public int CompareTo(Edge<TE, TW> other)
        {
            return Weight.CompareTo(other.Weight);
        }
    }
}
