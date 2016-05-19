using System;

namespace Sequence
{
    /// <summary>
    /// 顶点的类
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    internal class Vertex<TV> : IComparable<Vertex<TV>> where TV : IComparable<TV>
    {
        #region 属性
        
        public TV Data { get;  set; }
        public int InDegree { get; set; }
        public int OutDegree { get; set; }
        public VStatus Status { get; set; }
        public int DTime { get; set; }
        public int FTime { get; set; }
        public int Parent { get; set; }
        public int Priority { get; set; }

        #endregion

        public Vertex(TV d,int inDegree=0,int outDegree=0,
            VStatus status=VStatus.Undiscovered,
            int dTime=-1,int fTime=-1,int parent=-1,int priority=Int32.MaxValue)
        {
            Data = d;
            InDegree = inDegree;
            OutDegree = outDegree;
            Status=status;
            DTime = dTime;
            FTime = fTime;
            Parent = parent;
            Priority = priority;
        }


        public int CompareTo(Vertex<TV> other)
        {
            return Data.CompareTo(other.Data);
        }
    }
}
