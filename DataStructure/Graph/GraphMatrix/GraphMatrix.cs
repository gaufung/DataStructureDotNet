using System;

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
        private readonly IVector<Vertex<TV>> _v;
        private readonly IVector<IVector<Edge<TE, TW>>> _e; 

        public static Graph<TV, TE, TW> GraphFactory()
        {
            return new GraphMatrix<TV, TE, TW>();
        }
        private GraphMatrix()
        {
            N = E = 0;
            _v = Vector<Vertex<TV>>.VectorFactory();
            _e = Vector<IVector<Edge<TE, TW>>>.VectorFactory();
        }
        public override int Insert(TV e)
        {
            for (int i = 0; i < N; i++)
            {
                _e[i].Insert(null);
            }
            ++N;
            _e.Insert(CreateEdges());
            _v.Insert(new Vertex<TV>(e));
            return _v.Size - 1;
        }

        private IVector<Edge<TE, TW>> CreateEdges()
        {
            var edge = Vector<Edge<TE, TW>>.VectorFactory();
            for (int i = 0; i < N; i++)
            {
                edge.Insert(null);
            }
            return edge;
        }

        public override TV Remove(int index)
        {
            //修改结点的进度和初度
            for (int i = 0; i < N; i++)
            {
                if (Exist(index,i))
                {
                    _v[i].InDegree--;
                }
                if (Exist(i,index))
                {
                    _v[i].OutDegree--;
                }
                _e[i].Remove(index);
            }
            TV backup = _v[index].Data;
            _v.Remove(index);
            _e.Remove(index);
            N--;
            return backup;
        }

        public override TV Vertex(int index)
        {
            return _v[index].Data;
        }

        public override void Vertex(int index, TV value)
        {
            _v[index].Data = value;
        }

        public override int InDegree(int index)
        {
            return _v[index].InDegree;
        }

        protected override void InDegree(int index, int value)
        {
            _v[index].InDegree = value;
        }

        public override int OutDegree(int index)
        {
            return _v[index].OutDegree;
        }

        protected override void OutDegree(int index, int value)
        {
            _v[index].OutDegree = value;
        }

        public override VStatus Status(int index)
        {
            return _v[index].Status;
        }

        protected override void Status(int index, VStatus status)
        {
            _v[index].Status = status;
        }

        public override int DTime(int index)
        {
           return  _v[index].DTime;
        }

        protected override void DTime(int index, int dTime)
        {
            _v[index].DTime = dTime;
        }

        public override int FTime(int index)
        {
            return _v[index].FTime;
        }

        protected override void FTime(int index, int fTime)
        {
            _v[index].FTime = fTime;
        }

        public override int Parent(int index)
        {
            return _v[index].Parent;
        }

        protected override void Parent(int index, int parent)
        {
            _v[index].Parent = parent;
        }

        public override int Priority(int index)
        {
            return _v[index].Priority;
        }

        protected override void Priority(int index, int priority)
        {
            _v[index].Priority = priority;
        }

        public override bool Exist(int firVIndex, int secVIndex)
        {
            return  _e[firVIndex][secVIndex] != null;
        }

        public override void Insert(TE e, int firVIndex, int secVindex, TW weight=default(TW))
        {
            _e[firVIndex][secVindex]=new Edge<TE, TW>(e,weight);
            E++;
        }

        public override TE Remove(int firVIndex, int secVIndex)
        {
            TE backup = _e[firVIndex][secVIndex].Data;
            _e[firVIndex][secVIndex] = null;
            _v[firVIndex].OutDegree--;
            _v[secVIndex].InDegree--;
            E--;
            return backup;
        }

        public override EStatus Status(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Status;
        }

        protected override void Status(int firVIndex, int secVindex, EStatus status)
        {
            _e[firVIndex][secVindex].Status = status;
        }

        public override TE Edge(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Data;
        }

        protected override void Edge(int firVIndex, int secVIndex, TE edge)
        {
            _e[firVIndex][secVIndex].Data = edge;
        }

        public override TW Weight(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Weight;
        }

        protected override void Weight(int firVIndex, int secVIndex, TW weight)
        {
            _e[firVIndex][secVIndex].Weight = weight;
        }
    }
}
