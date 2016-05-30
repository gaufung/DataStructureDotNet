using System;

namespace Sequence.GraphList
{
    /// <summary>
    /// 使用邻接表表达的图
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    /// <typeparam name="TE"></typeparam>
    /// <typeparam name="TW"></typeparam>
    [Serializable]
    public class GraphList<TV, TE, TW>:Graph<TV,TE,TW>
        where TV : IComparable<TV>
        where TE : IComparable<TE>
        where TW : IComparable<TW>
    {
       // private readonly List<VertexEx<TV, TE, TW>> _list;
        private readonly IList<VertexEx<TV, TE, TW>> _list; 

        public static Graph<TV, TE, TW> GraphFactory()
        {
            return new GraphList<TV, TE, TW>();
        }
        private GraphList()
        {
            N = E = 0;
            _list = List<VertexEx<TV, TE, TW>>.ListFactory();
        }

        public override int Insert(TV e)
        {
            _list.InsertAsLast(new VertexEx<TV, TE, TW>(e));
            ++N;
            return _list.Size - 1;
        }

        public override TV Remove(int index)
        {
            TV backup = _list[index].Data;
            RemoveSource(index);
            RemoveDestination(index);
            N--;
            return backup;
        }

        /// <summary>
        /// 删除以结点为出发点的边
        /// <list type="Bullet">
        /// <item>
        /// 删除每个结点
        /// </item>
        /// <item>
        /// 修改结点的相关结点的入度
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="index"></param>
        private void RemoveSource(int index)
        {
            VertexEx<TV, TE, TW> vertex = _list[index];
            IList<EdgeEx<TE, TW>> edge = vertex.Edges;
            edge.Foreach(RemoveSource);
            _list.Remove(index);
        }

        private void RemoveSource(EdgeEx<TE, TW> item)
        {
            _list[item.Destination].InDegree--;
            E--;
        }

        /// <summary>
        /// 删除以结点为目标结点的边，并修改序号
        /// <remarks> 需要做的工作
        /// <list type="Bullet">
        /// <item>
        /// 删除结点
        /// </item>
        /// <item>
        /// 修改后续结点的目标结点的序号，如果
        /// 比要删除的结点的序号大，则将其序号减1
        /// </item>
        /// </list>
        /// </remarks>
        /// </summary>
        /// <param name="index"></param>
        private void RemoveDestination(int index)
        {
            //foreach (var vertex in _list)
            //{
            //    RemoveDestination(vertex,index);
            //}
            _list.Foreach(item=>RemoveDestination(item,index));
        }

        private void RemoveDestination(VertexEx<TV, TE, TW> vertex,int index)
        {
            var edge = vertex.Edges;
            for (int i = 0; i < edge.Size;)
            {
                if (edge[i].Destination>index)
                {
                    edge[i].Destination--;
                    i++;
                    continue;
                }
                if (edge[i].Destination==index)
                {
                    E--;
                    edge.Remove(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public override TV Vertex(int index)
        {
            return _list[index].Data;
        }

        public override void Vertex(int index, TV value)
        {
            _list[index].Data = value;
        }

        public override int InDegree(int index)
        {
            return _list[index].InDegree;
        }

        protected override void InDegree(int index, int value)
        {
            _list[index].InDegree = value;
        }

        public override int OutDegree(int index)
        {
            return _list[index].OutDegree;
        }

        protected override void OutDegree(int index, int value)
        {
            _list[index].OutDegree = value;
        }

        public override VStatus Status(int index)
        {
            return _list[index].Status;
        }

        protected override void Status(int index, VStatus status)
        {
            _list[index].Status = status;
        }

        public override int DTime(int index)
        {
            return _list[index].DTime;
        }

        protected override void DTime(int index, int dTime)
        {
            _list[index].DTime = dTime;
        }

        public override int FTime(int index)
        {
            return _list[index].FTime;
        }

        protected override void FTime(int index, int fTime)
        {
            _list[index].FTime = fTime;
        }

        public override int Parent(int index)
        {
            return _list[index].Parent;
        }

        protected override void Parent(int index, int parent)
        {
            _list[index].Parent = parent;
        }

        public override int Priority(int index)
        {
            return _list[index].Priority;
        }

        protected override void Priority(int index, int priority)
        {
            _list[index].Priority = priority;
        }

        public override bool Exist(int firVIndex, int secVIndex, bool isDirected = true)
        {
            if (isDirected)
            {
                var edges = _list[firVIndex].Edges;
                return edges.Any(item => item.Destination == secVIndex);
            }
            else
            {
                var edge1=_list[firVIndex].Edges;
                var edge2 = _list[secVIndex].Edges;
                return edge1.Any(item => item.Destination == secVIndex)
                       || edge2.Any(item => item.Destination == firVIndex);
            }
          
        }

        public override void Insert(TE e, int firVIndex, int secVindex, TW weight=default(TW))
        {
            var edges = _list[firVIndex].Edges;
            edges.InsertAsLast(new EdgeEx<TE, TW>(secVindex,e,weight));
            _list[firVIndex].OutDegree++;
            _list[secVindex].InDegree++;
            E++;
        }

        public override TE Remove(int firVIndex, int secVIndex)
        {
            var edges = _list[firVIndex].Edges;
            EdgeEx<TE,TW> backup = edges.FirstItme(item => item.Destination == secVIndex);
            edges.Remove(edges.Find(backup));
            _list[firVIndex].OutDegree--;
            _list[secVIndex].InDegree--;
            E--;
            return backup.Data;
        }

        public override EStatus Status(int firVIndex, int secVIndex)
        {
            return _list[firVIndex].
                Edges.FirstItme(item => item.Destination == secVIndex).Status;
        }

        protected override void Status(int firVIndex, int secVindex, EStatus status)
        {
            _list[firVIndex].
                Edges.FirstItme(item => item.Destination == secVindex).Status = status;
        }

        public override TE Edge(int firVIndex, int secVIndex)
        {
            return _list[firVIndex].Edges.
                FirstItme(item => item.Destination == secVIndex).Data;
        }

        protected override void Edge(int firVIndex, int secVIndex, TE edge)
        {
            _list[firVIndex].Edges.
                FirstItme(item => item.Destination == secVIndex).Data = edge;
        }

        public override TW Weight(int firVIndex, int secVIndex)
        {
            return _list[firVIndex].Edges.
                FirstItme(item => item.Destination == secVIndex).Weight;
        }

        protected override void Weight(int firVIndex, int secVIndex, TW weight)
        {
             _list[firVIndex].Edges.
                    FirstItme(item => item.Destination == secVIndex).Weight = weight;
        }
    }
}
