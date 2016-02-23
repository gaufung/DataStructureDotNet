using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequence
{
    public class ComplHeap<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        public void Insert(T e)
        {
            throw new NotImplementedException();
        }

        public T GetMax()
        {
            throw new NotImplementedException();
        }

        public T DelMax()
        {
            throw new NotImplementedException();
        }

        #region 私有成员

        private IList<T> _list;

        public ComplHeap()
        {
            _list=new List<T>();
        }

        #endregion

        #region 辅助操作

        protected int PercolateDown(int n,int i)
        {
            throw new NotImplementedException();
        }

        protected int PercolateUp(int i)
        {
            throw new NotImplementedException();
        }

        protected void Heapfiy(int n)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region 辅助的一些操作

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
            return i + (i << 1);
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
        #endregion
     
    }
}
