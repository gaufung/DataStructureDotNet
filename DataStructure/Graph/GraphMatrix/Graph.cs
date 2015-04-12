using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace Graph.GraphMatrix
{
    /// <summary>
    /// 用邻接矩阵表示的图结构
    /// </summary>
    /// <typeparam name="Tv"></typeparam>
    /// <typeparam name="Te"></typeparam>
    /// <typeparam name="Tp"></typeparam>
    public class Graph<Tv, Te> : IGraph<Tv, Te>
        where Tv : IComparable<Tv>
        where Te : IComparable<Te>
    {
        private List<Vertex<Tv>> _v;
        private List<List<Edge<Te>>> _e;

        public Graph()
        {
            N = E = 0;
            _v=new List<Vertex<Tv>>();
            _e=new List<List<Edge<Te>>>();
        }

        /// <summary>
        /// 清空邻接矩阵
        /// </summary>
        private void Reset()
        {
            for (int i = 0; i < N; i++)
            {
                Status(i, VStatus.Undiscovered);
                DTime(i, -1);
                FTime(i, -1);
                Parent(i, -1);
                Priority(i,int.MaxValue);
                for (int j = 0; j < N; j++)
                {
                    if (Exist(i,j))
                    {
                        Status(i,j,EStatus.Undetermined);
                    }
                }
            }
        }
        public int N
        {
            get; set;
        }

        public int Insert(Tv e)
        {
            for (int i = 0; i < N; i++)
            {
                _e[i].Add(null);
            }
            N++;
            _e.Add(CreteEdges(N));
            _v.Add(new Vertex<Tv>(e));
            return _v.Count - 1;
        }
        /// <summary>
        /// 创建一组空的边矩阵
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private List<Edge<Te>> CreteEdges(int num)
        {
            List<Edge<Te>> edge=new List<Edge<Te>>(num);
            for (int i = 0; i < num; i++)
            {
                edge.Add(null);
            }
            return edge;
        }

        #region 边和点的插入删除操作

        public Tv Remove(int vIndex)
        {
            for (int i = 0; i < N; i++)
            {
                if (Exist(vIndex, i))
                {
                    _v[i].InDegree--;
                }
            }
            _e.RemoveAt(vIndex);
            N--;
            for (int i = 0; i < N; i++)
            {
                if (Exist(i, vIndex))
                {
                    _v[i].OutDegree--;
                }
                _e[i].RemoveAt(vIndex);
            }
            Tv eBak = _v[vIndex].Data;
            _v.RemoveAt(vIndex);
            return eBak;
        }

        #region 获取和设置操作

        public Tv Vertex(int vIndex)
        {
            return _v[vIndex].Data;
        }

        public void Vertex(int vIndex, Tv vValue)
        {
            _v[vIndex].Data = vValue;
        }

        public int InDegree(int n)
        {
            return _v[n].InDegree;
        }

        public int OutDegree(int n)
        {
            return _v[n].OutDegree;
        }

        public VStatus Status(int vIndex)
        {
            return _v[vIndex].Status;
        }

        public void Status(int vIndex, VStatus status)
        {
            _v[vIndex].Status = status;
        }

        public int DTime(int vIndex)
        {
            return _v[vIndex].DTime;
        }

        public void DTime(int vIndex, int dtime)
        {
            _v[vIndex].DTime = dtime;
        }

        public int FTime(int vIndex)
        {
            return _v[vIndex].FTime;
        }

        public void FTime(int vIndex, int ftime)
        {
            _v[vIndex].FTime = ftime;
        }

        public int Parent(int vIndex)
        {
            return _v[vIndex].Parent;
        }

        public void Parent(int vIndex, int parent)
        {
            _v[vIndex].Parent = parent;
        }

        public int Priority(int vIndex)
        {
            return _v[vIndex].Priority;
        }

        public void Priority(int vIndex, int priority)
        {
            _v[vIndex].Priority = priority;
        }

        public int E
        {
            get;
            set;
        }

        public EStatus Status(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Status;
        }

        public void Status(int firVIndex, int secVIndex, EStatus status)
        {
            _e[firVIndex][secVIndex].Status = status;
        }

        public Te Edge(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Data;
        }

        public void Edge(int firVIndex, int secVIndex, Te data)
        {
            _e[firVIndex][secVIndex].Data = data;
        }

        public int Weight(int firVIndex, int secVIndex)
        {
            return _e[firVIndex][secVIndex].Weight;
        }

        public void Weight(int firVIndex, int secVIndex, int weight)
        {
            _e[firVIndex][secVIndex].Weight = weight;
        }

        #endregion


        public int FirstNbr(int vIndex)
        {
            return NextNbr(vIndex, N);
        }

        /// <summary>
        /// 查找下一个邻居
        /// </summary>
        /// <param name="vIndex"></param>
        /// <param name="preIndex"></param>
        /// <returns></returns>
        public int NextNbr(int vIndex, int preIndex)
        {
            while (preIndex > -1 && !Exist(vIndex, --preIndex))
            {

            }
            return preIndex;
        }



        public bool Exist(int firVIndex, int secVIndex)
        {
            return firVIndex < N && secVIndex < N && 0 <= firVIndex && 0 <= secVIndex &&
                   _e[firVIndex][secVIndex] != null;
        }

        public void Insert(Te e, int firVIndex, int secVIndex, int weight)
        {
            if (!Exist(firVIndex, secVIndex))
            {
                _e[firVIndex][secVIndex] = new Edge<Te>(e, weight);
                E++;
                _v[firVIndex].OutDegree++;
                _v[secVIndex].InDegree++;
            }
        }

        /// <summary>
        /// 先判断该边的确存在
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        public Te Remove(int firVIndex, int secVIndex)
        {
            Te back = Edge(firVIndex, secVIndex);
            _e[firVIndex][secVIndex] = null;
            _v[firVIndex].OutDegree--;
            _v[secVIndex].InDegree--;
            E--;
            return back;
        }

        #endregion

        #region 优先级搜索

        public void Pfs(Action<Graph<Tv, Te>, int, int> prioUpdater)
        {
            Pfs(0, prioUpdater);
        }

        public void Pfs(int s, Action<Graph<Tv, Te>, int, int> prioUpdater)
        {
            Reset();
            int v = s;
            do
            {
                if (Status(v) == VStatus.Undiscovered)
                {
                    PFS(v, prioUpdater);
                }
            } while (s != (++v % N));
        }

        private void PFS(int s, Action<Graph<Tv, Te>, int, int> prioUpdater)
        {

            Priority(s, 0);
            Status(s, VStatus.Visited);
            Parent(s, -1);
            while (true)
            {
                for (int w = FirstNbr(s); -1 < w; w = NextNbr(s, w))
                {
                    //提前判断是否存在边
                    prioUpdater(this, s, w);
                }
                for (int shortest = int.MaxValue, w = 0; w < N; w++)
                {
                    if (Status(w) == VStatus.Undiscovered)
                    {
                        if (shortest > Priority(w))
                        {
                            shortest = Priority(w);
                            s = w;
                        }
                    }
                }
                if (Status(s) == VStatus.Visited)
                {
                    break;
                }
                Status(s, VStatus.Visited);
                Status(Parent(s), s, EStatus.Tree);
            }
        }

        #endregion

        #region 广度优先

        public void Bfs()
        {
            Bfs(0);
        }

        public void Bfs(int stratIndex)
        {
            Reset();
            int clock = 0;
            int v = stratIndex;
            do
            {
                if (Status(v%N) == VStatus.Undiscovered)
                {
                    Bfs(v, ref clock);
                }
            } while (stratIndex != (++v%N));
        }

        private void Bfs(int startIndex, ref int clock)
        {
            Queue<int> queue = new Queue<int>();
            Status(startIndex, VStatus.Discovered);
            queue.Enqueue(startIndex);
            DTime(startIndex, ++clock);
            while (queue.Count != 0)
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

        #region 深度优先

        public void Dfs()
        {
            Dfs(0);
        }

        public void Dfs(int startIndex)
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
            } while (startIndex != (++v%N));
        }

        public void Dfs(int startIndex, ref int clock)
        {
            Status(startIndex, VStatus.Discovered);
            DTime(startIndex, ++clock);
            for (int u = FirstNbr(startIndex); u > -1; u = NextNbr(startIndex, u))
            {
                if (Status(u) == VStatus.Undiscovered)
                {
                    Status(startIndex, u, EStatus.Tree);
                    Parent(u, startIndex);
                    Dfs(u, ref clock);
                }
                if (Status(u) == VStatus.Discovered)
                {
                    Status(startIndex, u, EStatus.Backward);
                }
                else//Status(u) == VStatus.Visited
                {
                    Status(startIndex, u, DTime(startIndex) < DTime(u) ? EStatus.Forward : EStatus.Cross);
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
        /// <returns></returns>
        public Stack<Tv> Tsort()
        {
            return Tsort(0);
        }

        public Stack<Tv> Tsort(int n)
        {
            Reset();
            int clock = 0;
            int v = n;
            Stack<Tv> s = new Stack<Tv>();
            do
            {
                if (_v[v].Status == VStatus.Undiscovered)
                {
                    //如果出现闭环
                    if (!Tsort(v, s, ref clock))
                    {
                        while (s.Count != 0)
                        {
                            s.Pop();
                        }
                        break;
                    }
                }
            } while (n != (++v%N));
            return s;
        }

        private bool Tsort(int v, Stack<Tv> s, ref int clock)
        {
            DTime(v, ++clock);
            Status(v, VStatus.Discovered);
            for (int u = v; u > -1; u = NextNbr(v, u))
            {
                switch (_v[u].Status)
                {
                    case VStatus.Undiscovered:
                        Parent(u, v);
                        Status(u, v, EStatus.Tree);
                        if (!Tsort(u, s, ref clock))
                        {
                            return false;
                        }
                        break;
                    case VStatus.Discovered: //环路
                        Status(v, u, EStatus.Backward);
                        return false;
                        break;
                    case VStatus.Visited:
                        Status(v, u, DTime(v) < DTime(u) ? EStatus.Forward:EStatus.Cross);
                        break;
                }
            }
            Status(v, VStatus.Visited);
            s.Push(_v[v].Data);
            return true;
        }

        #endregion

        #region prim生成最小树算法

        /// <summary>
        /// 无向图的prim算法
        /// </summary>
        /// <returns></returns>
        public List<PrimEdge> Prim()
        {
            var e = new List<PrimEdge>();
            if (!Prim(e, 0))
            {
                e.Clear();
            }
            return e;
        }

        private bool Prim(List<PrimEdge> e, int n)
        {
            var addSub = new List<int>();
            var unAddSub = new List<int>();
            DivideVertexs(addSub, unAddSub, n);
           
            while (addSub.Count != N)
            {
                var edges = new List<PrimEdge>();
                for (int i=0; i < addSub.Count; i++)
                {
                    for (int j=0; j < unAddSub.Count; j++)
                    {
                        if (EdgeExist(addSub[i], unAddSub[j]))
                        {
                            edges.Add(new PrimEdge()
                            {
                                FirstVertex = addSub[i],
                                SecondVertex = unAddSub[j],
                                Weight = EdgeWeight(addSub[i], unAddSub[j])
                            });
                        }
                    }
                }                               
                if (edges.Count!= 0)
                {
                    edges.Sort();                  
                    addSub.Add(edges[0].SecondVertex);
                    unAddSub.Remove(edges[0].SecondVertex);
                    e.Add(edges[0]);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 将所有的顶点分为加入的分集和未分开的集合
        /// </summary>
        /// <param name="addedSub"></param>
        /// <param name="unAddedSub"></param>
        /// <param name="n">开始的分集</param>
        private void DivideVertexs(List<int> addedSub, List<int> unAddedSub, int n)
        {
            addedSub.Add(n);
            for (int i = 0; i < n; i++)
            {
                unAddedSub.Add(i);
            }
            for (int i = n + 1; i < N; i++)
            {
                unAddedSub.Add(i);
            }
        }

        /// <summary>
        /// 判断连个点之间是否存在边（无向图）
        /// </summary>
        /// <param name="v">顶点1</param>
        /// <param name="u">顶点2</param>
        /// <returns>是否存在</returns>
        private bool EdgeExist(int v, int u)
        {
            return Exist(v, u) || Exist(u, v);
        }

        private int EdgeWeight(int v, int u)
        {
            return _e[v][u] == null ? _e[u][v].Weight : _e[v][u].Weight;
        }

        #endregion

        #region Dijkstra算法

        public void Dijkstra()
        {
            Dijkstra(0);
        }

        public void Dijkstra(int v)
        {
            Pfs(v, DijkstraAcion());
        }

        private Action<Graph<Tv, Te>, int, int> DijkstraAcion()
        {
            return (Graph<Tv, Te> g, int s, int w) =>
            {
                if (g.Status(w) != VStatus.Undiscovered)
                {
                    return;
                }
                if (g.Priority(w) > g.Priority(s) + g.Weight(s, w))
                {
                    g.Priority(w, g.Priority(s) + g.Weight(s, w));
                    g.Parent(w, s);
                }
            };
        }

        #endregion

    }
}
