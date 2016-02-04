using System;
using System.Collections.Generic;

namespace Sequence
{
    /// <summary>
    /// 图结构的抽象类
    /// </summary>
    /// <typeparam name="TV">结点存储的数据</typeparam>
    /// <typeparam name="TE">边存储的数据</typeparam>
    /// <typeparam name="TW">边存储的权重</typeparam>
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
        /// <returns></returns>
        public int FirstNbr(int index)
        {
            return NextNbr(index, N);
        }

        /// <summary>
        /// 获取某个顶点（index）的序号为（preIndex）的下一个邻居结点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="preIndex"></param>
        /// <returns></returns>
        public int NextNbr(int index, int preIndex)
        {
            while (preIndex > -1 && !Exist(index, --preIndex))
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
        /// 
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        public abstract bool Exist(int firVIndex, int secVIndex);

        public abstract void Insert(TE e, int firVIndex, 
            int secVindex, TW weight);

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
            } while (startIndex != (++v % N));
        }

        private void Bfs(int startIndex, ref int clock)
        {
            var queue = new Queue<int>();
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

        #region 深度优先搜索

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
            } while (startIndex != (++v % N));
        }

        private void Dfs(int startIndex, ref int clock)
        {
            Status(startIndex, VStatus.Discovered);
            DTime(startIndex, ++clock);
            for (int u = FirstNbr(startIndex); u > -1; u = NextNbr(startIndex, u))
            {
                if (Status(u) == VStatus.Undiscovered)
                {
                    Status(startIndex,u,EStatus.Tree);
                    Parent(u, startIndex);
                    Dfs(u,ref clock);
                }
                if (Status(u) == VStatus.Discovered)
                {
                    Status(startIndex,u,EStatus.Backward);
                }
                else//Status(u) == VStatus.Visited
                {
                    Status(startIndex, u, DTime(startIndex) < DTime(u) ?
                        EStatus.Forward : EStatus.Cross);
                }
            }
            Status(startIndex, VStatus.Visited);
            FTime(startIndex, ++clock);
        }
        #endregion
        #endregion
    }
}
