using System;

namespace Sequence.GraphList
{
    /// <summary>
    /// 顶点类扩展，增加了指向第一条边的引用
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    /// <typeparam name="TE"></typeparam>
    /// <typeparam name="TW"></typeparam>
    internal class VertexEx<TV, TE, TW> : Vertex<TV> 
        where TV : IComparable<TV>
        where TE : IComparable<TE>
        where TW : IComparable<TW>
    {

        public IList<EdgeEx<TE,TW>> Edges { get; set; }
        public VertexEx(TV d, int inDegree = 0, 
            int outDegree = 0, VStatus status = VStatus.Undiscovered, 
            int dTime = -1, int fTime = -1, int parent = -1, 
            int priority = Int32.MaxValue) : 
            base(d, inDegree, outDegree, status, dTime, fTime, parent, priority)
        {
            Edges = List<EdgeEx<TE, TW>>.ListFactory();
        }
    }
}
