using System;
using System.Collections.Generic;
using Graph.GraphMatrix;


namespace Graph
{
    public interface IGraph<TV,TE> 
        where TV:IComparable<TV> where TE:IComparable<TE>
    {
        /*  
         * 顶点 相关操作
         */
        /// <summary>
        /// 顶点数目
        /// </summary>
        int N { get;  set; }

        /// <summary>
        /// 插入顶点
        /// </summary>
        /// <param name="e">顶点的值</param>
        /// <returns>顶点的编号</returns>
        int Insert(TV e);

        /// <summary>
        /// 删除顶点
        /// </summary>
        /// <param name="vIndex">顶点的序号</param>
        /// <returns>顶点的值</returns>
        TV Remove(int vIndex);

        /// <summary>
        /// 获取顶点的值
        /// </summary>
        /// <param name="vIndex">顶点的序号</param>
        /// <returns>顶点的值</returns>
        TV Vertex(int vIndex);
        /// <summary>
        /// 设置顶点的值
        /// </summary>
        /// <param name="vIndex"></param>
        /// <param name="vValue"></param>
        void Vertex(int vIndex, TV vValue);
        /// <summary>
        /// 顶点的入度
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        int InDegree(int n);
        /// <summary>
        /// 顶点的出度
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        int OutDegree(int n);

        /// <summary>
        /// 第一个相邻顶点
        /// </summary>
        /// <param name="vIndex"></param>
        /// <returns></returns>
        int FirstNbr(int vIndex);
        /// <summary>
        /// 下一个邻节点，即preIndex+1序号的节点
        /// </summary>
        /// <param name="vIndex"></param>
        /// <param name="preIndex"></param>
        /// <returns></returns>
        int NextNbr(int vIndex, int preIndex);

        /// <summary>
        /// 顶点的状态
        /// </summary>
        /// <param name="vIndex"></param>
        /// <returns></returns>
        VStatus Status(int vIndex);

        /// <summary>
        /// 设置顶点的状态
        /// </summary>
        /// <param name="vIndex"></param>
        /// <param name="status"></param>
        void Status(int vIndex, VStatus status);
        

        int DTime(int vIndex);
        void DTime(int vIndex, int dtime);
        int FTime(int vIndex);
        void FTime(int vIndex, int ftime);
        int Parent(int vIndex);
        void Parent(int vIndex, int parent);
        int Priority(int vIndex);
        void Priority(int vIndex, int priority);

        /*
         * 边的相关操作
         */
        /// <summary>
        /// 边的数目
        /// </summary>
        int E { get; set; }
        /// <summary>
        /// 边是否存在
        /// </summary>
        /// <param name="firVIndex">第一个顶点的序号</param>
        /// <param name="secVIndex">第二个顶点的序号</param>
        /// <returns></returns>
        bool Exist(int firVIndex, int secVIndex);
        /// <summary>
        /// 插入某条边
        /// </summary>
        /// <param name="e">顶点的值</param>
        /// <param name="firVIndex">第一个顶点的序号</param>
        /// <param name="secVIndex">第二个顶点的序号</param>
        /// <param name="weight">权重</param>
        void Insert(TE e, int firVIndex, int secVIndex, int weight);
        /// <summary>
        ///删除某条边
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        TE Remove(int firVIndex, int secVIndex);
        /// <summary>
        /// 边的状态
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        EStatus Status(int firVIndex, int secVIndex);
        /// <summary>
        /// 边的状态设值
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <param name="status"></param>
        void Status(int firVIndex, int secVIndex, EStatus status);

        /// <summary>
        /// 获取边的数据
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        TE Edge(int firVIndex, int secVIndex);
        /// <summary>
        /// 设置边的值
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <param name="data"></param>
        void Edge(int firVIndex, int secVIndex,TE data);
        /// <summary>
        /// 获取边的权重
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <returns></returns>
        int Weight(int firVIndex, int secVIndex);
        /// <summary>
        /// 设置边的权重
        /// </summary>
        /// <param name="firVIndex"></param>
        /// <param name="secVIndex"></param>
        /// <param name="weight"></param>
        void Weight(int firVIndex, int secVIndex, int weight);
        /*
         * 算法
         */
        /// <summary>
        /// 广度优先
        /// </summary>
        void Bfs();

        void Bfs(int n);
        /// <summary>
        /// 深度优先
        /// </summary>
        void Dfs();

        void Dfs(int n);
        /// <summary>
        /// 拓扑排序
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        Stack<TV> Tsort(int n);

        Stack<TV> Tsort();
        /// <summary>
        /// 最小支撑树
        /// </summary>
        void Prim();
        /// <summary>
        /// 最短路径算法
        /// </summary>
        void Dijkstra();

        void Dijkstra(int n);

        /// <summary>
        /// 优先级搜索框架
        /// </summary>
        /// <param name="prioUpdater"></param>
        void Pfs(Action<Graph<TV, TE>, int, int> prioUpdater);

        void Pfs(int s, Action<Graph<TV, TE>, int, int> prioUpdater);
    }
}
