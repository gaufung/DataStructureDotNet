using System;
using System.Collections.Generic;
namespace Graph.GraphMatrix
{
    /// <summary>
    /// 用邻接矩阵表示的图结构
    /// </summary>
    /// <typeparam name="Tv"></typeparam>
    /// <typeparam name="Te"></typeparam>
    /// <typeparam name="Tp"></typeparam>
    public class Graph<Tv,Te,Tp>:IGraph<Tv,Te,Tp> where Tv:IComparable<Tv> where Te:IComparable<Te>
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
                Prority(i,int.MaxValue);
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

        public Tv Remove(int vIndex)
        {
            for (int i = 0; i < N; i++)
            {
                if (Exist(vIndex,i))
                {
                    _v[i].InDegree--;
                }
            }
            _e.RemoveAt(vIndex);
            N--;
            for (int i = 0; i < N; i++)
            {
                if (Exist(i,vIndex))
                {
                    _v[i].OutDegree--;
                }
                _e[i].RemoveAt(vIndex);
            }
            Tv eBak = _v[vIndex].Data;
            _v.RemoveAt(vIndex);
            return eBak;
        }

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
            while (preIndex>-1&&!Exist(vIndex,--preIndex))
            {
                
            }
            return preIndex;
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

        public void Prority(int vIndex, int priority)
        {
            _v[vIndex].Priority = priority;
        }

        public int E
        {
            get; set;
        }

        public bool Exist(int firVIndex, int secVIndex)
        {
            return firVIndex < N && secVIndex < N && 0 <= firVIndex && 0 <= secVIndex &&
                   _e[firVIndex][secVIndex] != null;
        }

        public void Insert(Te e, int firVIndex, int secVIndex, int weight)
        {
            if (!Exist(firVIndex,secVIndex))
            {
                _e[firVIndex][secVIndex]=new Edge<Te>(e,weight);
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

        public void Bfs()
        {
            throw new NotImplementedException();
        }

        public void Dfs()
        {
            throw new NotImplementedException();
        }

        public void Bcc()
        {
            throw new NotImplementedException();
        }

        public Stack<Tv> Tsort(int n)
        {
            throw new NotImplementedException();
        }

        public void Prim()
        {
            throw new NotImplementedException();
        }

        public void Dijkstra()
        {
            throw new NotImplementedException();
        }

        public void Pfs(int n, Tp p)
        {
            throw new NotImplementedException();
        }
    }
}
