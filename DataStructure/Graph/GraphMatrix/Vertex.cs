using System;

namespace Graph.GraphMatrix
{
    /// <summary>
    /// 顶点的类
    /// </summary>
    /// <typeparam name="Tv"></typeparam>
    public class Vertex<Tv> where Tv:IComparable<Tv>
    {
        private const  int Intmax = int.MaxValue;
        #region 属性
        
        public Tv Data { get;  set; }
        public int InDegree { get; set; }
        public int OutDegree { get; set; }
        public VStatus Status { get; set; }
        public int DTime { get; set; }
        public int FTime { get; set; }
        public int Parent { get; set; }
        public int Priority { get; set; }

        #endregion

        public Vertex(Tv d)
        {
            Data = d;
            InDegree = 0;
            OutDegree = 0;
            Status=VStatus.Undiscovered;
            DTime = -1;
            FTime = -1;
            Parent = -1;
            Priority = Intmax;
        }

    }
}
