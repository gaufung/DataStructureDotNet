using System;

namespace Sequence
{
    /// <summary>
    /// 图结构的抽象类
    /// </summary>
    /// <typeparam name="TV">结点存储的数据</typeparam>
    /// <typeparam name="TE">边存储的数据</typeparam>
    /// <typeparam name="TW">边存储的权重</typeparam>
    [Serializable]
    public abstract class Graph<TV,TE,TW> where TV:IComparable<TV>
        where TE:IComparable<TE> where TW:IComparable<TW>
    {
        #region 顶点相关操作

        /// <summary>
        /// 顶点的个数
        /// </summary>
        public int N { get; protected set; }

        /// <summary>
        /// 插入顶点
        /// </summary>
        /// <param name="e">顶点的值</param>
        /// <returns>返回该顶点的序号</returns>
        public abstract int Insert(TV e);

        /// <summary>
        /// 删除某一个顶点
        /// </summary>
        /// <param name="index">顶点的序号</param>
        /// <returns>顶点内容的值</returns>
        public abstract TV Remove(int index);

        /// <summary>
        /// 获取某个顶点的内容
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract TV Vertex(int index);
        /// <summary>
        /// 设置某个顶点的值
        /// </summary>
        /// <param name="index">顶点的序号</param>
        /// <param name="value">值</param>
        public abstract void Vertex(int index, TV value);
        /// <summary>
        /// 获取某个顶点的入度
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int InDegree(int index);
        /// <summary>
        /// 设置某个顶点的入度
        /// </summary>
        /// <param name="index">顶点的序号</param>
        /// <param name="value">值</param>
        protected abstract void InDegree(int index, int value);

        /// <summary>
        /// 获取某个顶点的出度
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int OutDegree(int index);
        /// <summary>
        /// 设置某个顶点的出度
        /// </summary>
        /// <param name="index">顶点的序号</param>
        /// <param name="value">值</param>
        protected abstract void OutDegree(int index, int value);

        /// <summary>
        /// 获取某个顶点的第一个邻居结点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isDirected"></param>
        /// <returns></returns>
        public int FirstNbr(int index, bool isDirected = true)
        {
            return NextNbr(index, N,isDirected);
        }

        /// <summary>
        /// 获取某个顶点（index）的序号为（preIndex）的下一个邻居结点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="preIndex"></param>
        /// <param name="isDirected"></param>
        /// <returns></returns>
        public int NextNbr(int index, int preIndex,bool isDirected=true)
        {
            while (preIndex > -1 && !Exist(index, --preIndex,isDirected))
            {
            }           
            return preIndex;
        }
        /// <summary>
        /// 获取顶点的状态
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract VStatus Status(int index);
        /// <summary>
        /// 设置顶点的状态
        /// </summary>
        /// <param name="index">顶点的序号</param>
        /// <param name="status">值</param>
        protected abstract void Status(int index, VStatus status);
        /// <summary>
        /// 获取某个顶点的Discover 时间标签
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int DTime(int index);
        /// <summary>
        /// 设置某个顶点的Discover 的时间标签
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dTime"></param>
        protected abstract void DTime(int index, int dTime);
        /// <summary>
        /// 获取某个顶点的Finish 的时间标签
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int FTime(int index);
        /// <summary>
        /// 设置某个顶点的Finish 的时间标签
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fTime"></param>
        protected abstract void FTime(int index, int fTime);
        /// <summary>
        /// 获取某个结点在遍历树中的父节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int Parent(int index);
        /// <summary>
        /// 设置某个结点在遍历树中的父节点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="parent"></param>
        protected abstract void Parent(int index, int parent);
        /// <summary>
        /// 获取顶点在遍历树的中的优先级
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int Priority(int index);
        /// <summary>
        /// 设置顶点在遍历树中优先级
        /// </summary>
        /// <param name="index"></param>
        /// <param name="priority"></param>
        protected abstract void Priority(int index, int priority);

        #endregion

        #region 边的相关操作
        public int E { get; protected set; }

        /// <summary>
        /// 两个顶点是否存在边
        /// </summary>
        /// <param name="firVIndex">第一个顶点</param>
        /// <param name="secVIndex">第二个顶点</param>
        /// <param name="isDirected">是否为有向图</param>
        /// <returns></returns>
        public abstract bool Exist(int firVIndex, int secVIndex,bool isDirected=true);

        public abstract void Insert(TE e, int firVIndex, 
            int secVindex, TW weight=default(TW));

        public abstract TE Remove(int firVIndex, int secVIndex);

        public abstract EStatus Status(int firVIndex, int secVIndex);

        protected abstract void Status(int firVIndex, int secVindex, EStatus status);

        public abstract TE Edge(int firVIndex, int secVIndex);

        protected abstract void Edge(int firVIndex, int secVIndex, TE edge);

        public abstract TW Weight(int firVIndex, int secVIndex);
        protected abstract void Weight(int firVIndex, int secVIndex, TW weight);

        #endregion

        #region 通用操作

        protected void Reset()
        {
            for (int i = 0; i < N; i++)
            {
                Status(i, VStatus.Undiscovered);
                DTime(i, -1);
                FTime(i, -1);
                Parent(i, -1);
                Priority(i, int.MaxValue);
                for (int j = 0; j < N; j++)
                {
                    if (Exist(i,j))
                    {
                        Status(i,j,EStatus.Undetermined);
                    }
                }
            }
        }

        #region 广度优先搜索

        /// <summary>
        /// 广度优先搜索
        /// </summary>
        /// <param name="startIndex"></param>
        public void Bfs(int startIndex = 0)
        {
            Reset();
            int clock = 0;
            int v = startIndex;
            do
            {
                if (Status(v % N) == VStatus.Undiscovered)
                {
                    Bfs(v, ref clock);
                }
            } while (startIndex != (v=(++v % N)));
        }

        private void Bfs(int startIndex, ref int clock)
        {
            Queue<Int32> queue = QueueListImpl<Int32>.QueueFacotry();
            Status(startIndex, VStatus.Discovered);
            queue.Enqueue(startIndex);
            DTime(startIndex, ++clock);
            while (queue.Size != 0)
            {
                int v = queue.Dequeue();
                for (int u = FirstNbr(v); u > -1; u = NextNbr(v, u))
                {
                    if (Status(u) == VStatus.Undiscovered)
                    {
                        Status(u, VStatus.Discovered);
                        Status(v, u, EStatus.Tree);
                        queue.Enqueue(u);
                        Parent(u, v);
                    }
                    else
                    {
                        Status(v, u, EStatus.Cross);
                    }
                }
                FTime(v, ++clock);
                Status(v, VStatus.Visited);
            }
        } 
        #endregion

        #region 深度优先搜索

        /// <summary>
        /// 深度优先搜索
        /// </summary>
        /// <param name="startIndex"></param>
        public void Dfs(int startIndex = 0)
        {
            Reset();
            int v = startIndex;
            int clock = 0;
            do
            {
                if (Status(v) == VStatus.Undiscovered)
                {
                    Dfs(v, ref clock);
                }
            } while (startIndex != (v = (++v % N)));
        }

        private void Dfs(int startIndex, ref int clock)
        {
            Status(startIndex, VStatus.Discovered);
            DTime(startIndex, ++clock);
            for (int u = FirstNbr(startIndex); u > -1; u = NextNbr(startIndex, u))
            {
                switch (Status(u))
                {
                    case VStatus.Undiscovered:
                        Status(startIndex,u,EStatus.Tree);
                        Parent(u, startIndex);
                        Dfs(u,ref clock);
                        break;
                    case VStatus.Discovered:
                        Status(startIndex, u, EStatus.Backward);
                        break;
                    default:
                        Status(startIndex, u, DTime(startIndex) < DTime(u) ?
                            EStatus.Forward : EStatus.Cross);
                        break;
                }
            }
            Status(startIndex, VStatus.Visited);
            FTime(startIndex, ++clock);
        }
        #endregion

        #region 拓扑排序

        /// <summary>
        /// 拓扑排序
        /// </summary>
        /// <param name="s"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Stack<TV> TopoSort(int s)
        {
            Reset();
            int clock = 0;
            int v = s;
            Stack<TV> stack = StackVectorImpl<TV>.StackFactory();
            do
            {
                if (VStatus.Undiscovered==Status(v))
                {
                    if (!TopoSort(v,ref clock,stack))
                    {
                        while (stack.Size!=0)
                        {
                            stack.Pop();
                        }
                        break;
                    }
                }
            } while (s!=(v=(++v%N)));
            return stack;
        }

        private bool TopoSort(int v, ref int clock, Stack<TV> s)
        {
            DTime(v, ++clock);
            Status(v, VStatus.Discovered);
            for (int u = FirstNbr(v); -1< u; u=NextNbr(v,u))
            {
                switch (Status(u))
                {
                   case VStatus.Undiscovered:
                        Parent(u, v);
                        Status(v,u,EStatus.Tree);
                        if (!TopoSort(u,ref clock,s))
                        {
                            return false;
                        }
                        break;
                    //Find DAG
                    case VStatus.Discovered:
                        Status(v,u,EStatus.Backward);
                        return false;
                    default:
                        Status(v, u, DTime(v) < DTime(u)
                            ? EStatus.Forward
                            : EStatus.Cross);
                        break;
                }           
            }
            Status(v, VStatus.Visited);
            s.Push(Vertex(v));
            return true;
        }
        #endregion

        #region 优先级搜索

        public void Pfs(Action<Graph<TV, TE, TW>,int,int> prioUpdater, int startIndex = 0)
        {
            Reset();
            int v = startIndex;
            do
            {
                if (VStatus.Undiscovered == Status(v))
                    pfs(prioUpdater,v);
            } while (startIndex!=(v=(++v%N)));
        }

        private void pfs(Action<Graph<TV, TE, TW>,int,int> prioUpdater, int startIndex)
        {
            Priority(startIndex, 0);
            Status(startIndex, VStatus.Visited);
            Parent(startIndex, -1);
            while (true)
            {
                for (int w = 0; -1<w; w=NextNbr(startIndex,w))
                {
                    prioUpdater(this, startIndex, w);
                    int shortest;
                    for (shortest = Int32.MaxValue , w=0;w<N; w++)
                    {
                        if(Status(w)==VStatus.Undiscovered)
                            if (shortest > Priority(w))
                            {
                                shortest = Priority(w);
                                startIndex = w;
                            }
                        if(VStatus.Visited==Status(startIndex))break;
                        Status(startIndex,VStatus.Visited);
                        Status(Parent(startIndex),startIndex,EStatus.Tree);
                        
                    }
                }
            }
        }
        
        #endregion

        #region minimun spanning tree

        #region Prim算法 针对是无向图

        public void Prime(int s)
        {
            Reset();
            int  v= s;
            do
            {
                if (Status(v) == VStatus.Undiscovered)
                {
                    prime(v);
                }
            } while (s!=(v=(++v%N)));

        }

        private void prime(int index)
        {
            IVector<int> vertexSet = Vector<int>.VectorFactory();
            vertexSet.Insert(index);
            IVector<int> vertexCutSet = CaluCutSet(index);
            Status(index,VStatus.Visited);
            while (true)
            {
                Tuple<int, int> edge = MimimumEdge(vertexSet, vertexCutSet);
                if(edge==null) break;
                vertexSet.Insert(edge.Item2);
                vertexCutSet.Remove(vertexCutSet.Find(edge.Item2));
                Status(edge.Item2, VStatus.Visited);
                if (Exist(edge.Item1, edge.Item2))
                {
                    Status(edge.Item1,edge.Item2,EStatus.Tree);
                }
                else
                {
                    Status(edge.Item2, edge.Item1, EStatus.Tree);
                }
            }
        }

        /// <summary>
        /// 获取顶点的补集
        /// </summary>
        /// <param name="index">起始的位置</param>
        /// <returns>返回</returns>
        private IVector<int> CaluCutSet(int index)
        {
            IVector<int> cutSet=Vector<int>.VectorFactory();
            for (int i = 0; i < N; i++)
            {
                if (i != index && Status(i) != VStatus.Visited)
                {
                    cutSet.Insert(i);
                }
            }
            return cutSet;
        }

        /// <summary>
        /// 两个集合之间的权重最小的边
        /// </summary>
        /// <param name="vertexSet">集合</param>
        /// <param name="vertexCutSet">另一个集合</param>
        /// <returns></returns>
        private Tuple<int, int> MimimumEdge(IVector<int> vertexSet, IVector<int> vertexCutSet)
        {
            int set = -1;
            int cutSet = -1;
            //if 
            for (int i = 0; i < vertexSet.Size; i++)
            {
                for (int j = 0; j < vertexCutSet.Size; j++)
                {
                    if (Exist(vertexSet[i], vertexCutSet[j], false))
                    {
                        set = vertexSet[i];
                        cutSet = vertexCutSet[j];
                    }
                }
            }
            if (set == -1 || cutSet == -1) return null;
            TW edge = Exist(set, cutSet) ? Weight(set, cutSet) : Weight(cutSet, set);
            for (int i = 0; i < vertexSet.Size; i++)
            {
                for (int j = 0; j < vertexCutSet.Size; j++)
                {
                    if (Exist(vertexSet[i], vertexCutSet[j], false))
                    {
                        TW newEdge = Exist(vertexSet[i], vertexCutSet[j]) ? Weight(vertexSet[i], vertexCutSet[j]) : Weight(vertexCutSet[j],vertexSet[i]);
                        if (newEdge.CompareTo(edge) == -1)
                        {
                            edge = newEdge;
                            set = vertexSet[i];
                            cutSet = vertexCutSet[j];
                        }
                    }
                }
            }
            return new Tuple<int, int>(set,cutSet);
        }

        #endregion


        #region  Kruskal 算法
        #endregion

        /// <summary>
        /// Krukal 最小支撑树
        /// <remarks>
        /// 要求
        /// <list type="Dot">
        /// 无向图
        /// </list>
        /// <list type="Dot">
        /// 单连通
        /// </list>
        /// </remarks>
        /// </summary>
        public void Kruskal()
        {
            Reset();
            IPriorityQueue<KruskalEdge> edges = BuildEdgeHeap();
            IVector<IVector<int>> vertexs = BuildVertex();
            while (vertexs.Size!=1)
            {
                KruskalEdge edge = edges.DelMax();
                int from = -1;
                int to = -1;
                for (int i = 0; i < vertexs.Size; i++)
                {
                    if (vertexs[i].Find(edge.From)!=-1)
                    {
                        from = i;
                    }
                    if (vertexs[i].Find(edge.To) != -1)
                    {
                        to = i;
                    }
                }
                if (from!=to)
                {
                    for (int i = 0; i < vertexs[to].Size; i++)
                    {
                        vertexs[from].Insert(vertexs[to][i]);
                    }
                    vertexs.Remove(to);
                    if (Exist(edge.From,edge.To))
                    {
                        Status(edge.From,edge.To,EStatus.Tree);
                    }
                    else
                    {
                        Status(edge.To,edge.From,EStatus.Tree);
                    }
                }
            }
        }


        private IPriorityQueue<KruskalEdge> BuildEdgeHeap()
        {
            IPriorityQueue<KruskalEdge> edges = new CompleteHeap<KruskalEdge>();
            for (int i = 0; i < N-1; i++)
            {
                for (int j = i+1; j < N; j++)
                {
                    if (Exist(i,j,false))
                    {
                        edges.Insert(new KruskalEdge()
                        {
                            From = i,To = j,Weight = Exist(i,j)?Weight(i,j):Weight(j,i)
                        });
                    }
                }
            }
            return edges;
        }

        private IVector<IVector<int>> BuildVertex()
        {
            IVector<IVector<int>> vertexs = Vector<IVector<int>>.VectorFactory();
            for (int i = 0; i < N; i++)
            {
                IVector<int> vertex=Vector<int>.VectorFactory();
                vertex.Insert(i);
                vertexs.Insert(vertex);
            }
            return vertexs;
        }

        private class KruskalEdge:IComparable<KruskalEdge>
        {
            /// <summary>
            /// 边的权重
            /// </summary>
            public TW Weight { get; set; }

            /// <summary>
            /// 边的开始点
            /// </summary>
            public int From { get; set; }

            /// <summary>
            /// 边的结束点
            /// </summary>
            public int To { get; set; }

            private bool _isMin;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="isMin">是否取权重最小的</param>
            public KruskalEdge(bool isMin=true)
            {
                _isMin = isMin;
            }
            public int CompareTo(KruskalEdge other)
            {
                return _isMin ? 
                    -1*Weight.CompareTo(other.Weight) : 
                    Weight.CompareTo(other.Weight);
            }
        }
        #endregion


        #region  Dijkstra算法

        public TW[] Dijkstra(TW maxWeight,int startIndex=0)
        {
            Reset();    
            TW[] weights=new TW[N];
            for (int i = 0; i < N; i++)
            {
                weights[i] = maxWeight;
            }
            weights[startIndex] = default(TW);
            Status(startIndex, VStatus.Discovered);
            IPriorityQueue<DijkstraNode> nodes = BuildDijkstraNodes(weights);
            while (nodes.Count!=0)
            {
                DijkstraNode node = nodes.DelMax();
                Status(node.Index, VStatus.Visited);
                for (int u = FirstNbr(node.Index); -1 < u; u = NextNbr(node.Index, u))
                {
                    if (Status(u)!=VStatus.Visited)
                    {
                        Status(u, VStatus.Discovered);
                        if (Sum(weights[node.Index],Weight(node.Index,u)).CompareTo(weights[u])<0)
                        {
                            weights[u] = Sum(weights[node.Index], Weight(node.Index, u));
                        }
                    }
                }
                nodes = BuildDijkstraNodes(weights);
            }
            return weights;
        }
        private  TW Sum(TW a,TW b)
        {
            return (dynamic)a + (dynamic)b;
        }

        private IPriorityQueue<DijkstraNode> BuildDijkstraNodes(TW[] weights)
        {
            IPriorityQueue<DijkstraNode> nodes = new CompleteHeap<DijkstraNode>();
            for (int i = 0; i < N; i++)
            {
                if (Status(i)==VStatus.Discovered)
                {
                   nodes.Insert(new DijkstraNode(){Cost = weights[i],Index=i});
                }
            }
            return nodes;
        }

        private class DijkstraNode : IComparable<DijkstraNode>
        {
            public int Index { get; set; }
            public TW  Cost { get; set; }
            public int CompareTo(DijkstraNode other)
            {
                return -1*Cost.CompareTo(other.Cost);
            }
        }
        #endregion
        #endregion
    }
}
