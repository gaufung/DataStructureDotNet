using System;

namespace Graph.GraphMatrix
{
    /// <summary>
    /// 边节点类
    /// </summary>
    /// <typeparam name="Te"></typeparam>
    public class Edge<Te> where Te:IComparable<Te>
    {
        #region 属性

        public Te Data { get;  set; }
        public int Weight { get; set; }
        public EStatus Status { get; set; } 
        #endregion

        public Edge(Te d,int w)
        {
            Data = d;
            Weight = w;
            Status = EStatus.Undetermined;
        }
    }
}
