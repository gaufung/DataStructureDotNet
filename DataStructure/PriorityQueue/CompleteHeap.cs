using System;
using System.Diagnostics;

namespace Sequence
{
    /// <summary>
    /// Complete binary Tree 实现二叉堆
    /// </summary>
    /// <typeparam name="T">类型参数</typeparam>
    [DebuggerDisplay("Max=getMax()")]
    [DebuggerDisplay("Size=Count")]
    [Serializable]
    public class CompleteHeap<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        public void Insert(T e)
        {
            _vector.Insert(Count, e);
            PercolateUp(Count - 1);
        }

        public T GetMax()
        {
            return _vector[0];
        }

        public T DelMax()
        {
            T backup = _vector[0];
            _vector[0] = _vector[Count - 1];
            _vector.Remove(Count-1);
            PercolateDown(Count, 0);
            return backup;
        }

        public int Count
        {
            get { return _vector.Size; }
        }

        #region 私有成员

        private readonly IVector<T> _vector;

        public CompleteHeap()
        {
            _vector=Vector<T>.VectorFactory();
        }

        public CompleteHeap(T[] source)
        {
            _vector = Vector<T>.VectorFactory(source,source.Length);
            Heapfiy(Count);
        }
        #endregion

        #region 辅助操作
        /// <summary>
        /// 下滤操作
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        protected int PercolateDown(int n,int i)
        {
            int j;
            while (i != (j = ProperParent(n, i)))
            {
                Swap(i,j);
                i = j;
            }
            return i;
        }

        /// <summary>
        /// 上滤操作
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        protected int PercolateUp(int i)
        {
            while (ParentValid(i))
            {
                int j = Parent(i);
                if(Lt(_vector[i],_vector[j]))break;
                Swap(i,j);
                i=j;
            }
            return i;
        }

        protected void Heapfiy(int n)
        {
            for (int i = LastInternal(Count); InHeap(Count, i); i--)
            {
                PercolateDown(Count, i);
            }
        }

        #endregion


        #region 辅助的一些操作

        #region 比较操作

        private static bool Gt(T a, T b)
        {
            return a.CompareTo(b) > 0;
        }

        private static bool Eq(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        private static bool Lt(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }
        #endregion


        /// <summary>
        /// 父节点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>父节点的索引</returns>
        private static int Parent(int i)
        {
            return (i - 1) >> 1;
        }
        /// <summary>
        /// 左孩子节点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>左孩子节点的索引</returns>
        private static int LChild(int i)
        {
            return 1 + (i << 1);
        }
        /// <summary>
        /// 右孩子结点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>右孩子结点的索引</returns>
        private static int RChild(int i)
        {
            return (1 + i) << 1;
        }
        /// <summary>
        /// 判断某个索引是否在堆中
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool InHeap(int n, int i)
        {
            return i > -1 && i < n;
        }
        /// <summary>
        /// 二叉堆中非叶子结点的最大索引
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int LastInternal(int n)
        {
            return Parent(n-1);
        }
        /// <summary>
        /// 父节点是否有效
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool ParentValid(int i)
        {
            return 0 < i;
        }
        /// <summary>
        /// 左孩子是否有效
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool LChildValid(int n, int i)
        {
            return InHeap(n,LChild(i));
        }
        /// <summary>
        /// 右孩子是否有效
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool RChildValid(int n, int i)
        {
            return InHeap(n,RChild(i));
        }

        /// <summary>
        /// 两者中最大者的序号
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private int Bigger(int i, int j)
        {
            return _vector[i].CompareTo(_vector[j]) >= 0
                ? i
                : j;
        }
        /// <summary>
        /// 父子（最多三个）结点中最大的值的序号（相等时父节点优先）
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int ProperParent(int n,int i)
        {
            if (RChildValid(n,i))
            {
                return Bigger(Bigger(i, LChild(i)), RChild(i));
            }
            if (LChildValid(n, i))
            {
                return Bigger(i,LChild(i));
            }
            return i;
        }

        private void Swap(int i, int j)
        {
            T temp = _vector[i];
            _vector[i] = _vector[j];
            _vector[j] = temp;
        }
        #endregion
     
    }
}
