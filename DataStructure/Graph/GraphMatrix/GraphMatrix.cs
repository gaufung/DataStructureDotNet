using System;
using System.Collections.Generic;

namespace Sequence.GraphMatrix
{
    /// <summary>
    /// 使用邻接矩阵表达
    /// </summary>
    /// <typeparam name="TV">顶点包含的数据</typeparam>
    /// <typeparam name="TE">边包含的数据</typeparam>
    /// <typeparam name="TW">权重</typeparam>
    public class GraphMatrix<TV,TE,TW>:Graph<TV,TE,TW>
        where TV : IComparable<TV>
        where TE : IComparable<TE>
        where TW : IComparable<TW>
    {
        private readonly List<Vertex<TV>> _v;
        private readonly List<List<Edge<TE,TW>>> _e;

        public static Graph<TV, TE, TW> GraphFactory()
        {
            return new GraphMatrix<TV, TE, TW>();
        }
        private GraphMatrix()
        {
            N = E = 0;
            _v=new List<Vertex<TV>>();
            _e=new List<List<Edge<TE, TW>>>();
        }
        public override int Insert(TV e)
        {
            throw new NotImplementedException();
        }

        public override TV Remove(int index)
        {
            throw new NotImplementedException();
        }

        public override TV Vertex(int index)
        {
            throw new NotImplementedException();
        }

        public override void Vertex(int index, TV value)
        {
            throw new NotImplementedException();
        }

        public override int InDegree(int index)
        {
            throw new NotImplementedException();
        }

        protected override void InDegree(int index, int value)
        {
            throw new NotImplementedException();
        }

        public override int OutDegree(int index)
        {
            throw new NotImplementedException();
        }

        protected override void OutDegree(int index, int value)
        {
            throw new NotImplementedException();
        }

        public override VStatus Status(int index)
        {
            throw new NotImplementedException();
        }

        protected override void Status(int index, VStatus status)
        {
            throw new NotImplementedException();
        }

        public override int DTime(int index)
        {
            throw new NotImplementedException();
        }

        protected override void DTime(int index, int dTime)
        {
            throw new NotImplementedException();
        }

        public override int FTime(int index)
        {
            throw new NotImplementedException();
        }

        protected override void FTime(int index, int fTime)
        {
            throw new NotImplementedException();
        }

        public override int Parent(int index)
        {
            throw new NotImplementedException();
        }

        protected override void Parent(int index, int parent)
        {
            throw new NotImplementedException();
        }

        public override int Priority(int index)
        {
            throw new NotImplementedException();
        }

        protected override void Priority(int index, int priority)
        {
            throw new NotImplementedException();
        }

        public override bool Exist(int firVIndex, int secVIndex)
        {
            throw new NotImplementedException();
        }

        public override void Insert(TE e, int firVIndex, int secVindex, TW weight)
        {
            throw new NotImplementedException();
        }

        public override TE Remove(int firVIndex, int secVIndex)
        {
            throw new NotImplementedException();
        }

        public override EStatus Status(int firVIndex, int secVIndex)
        {
            throw new NotImplementedException();
        }

        protected override void Status(int firVIndex, int secVindex, EStatus status)
        {
            throw new NotImplementedException();
        }

        public override TE Edge(int firVIndex, int secVIndex)
        {
            throw new NotImplementedException();
        }

        protected override void Edge(int firVIndex, int secVIndex, TE edge)
        {
            throw new NotImplementedException();
        }

        public override TW Weight(int firVIndex, int secVIndex)
        {
            throw new NotImplementedException();
        }

        protected override void Weight(int firVIndex, int secVIndex, TW weight)
        {
            throw new NotImplementedException();
        }
    }
}
