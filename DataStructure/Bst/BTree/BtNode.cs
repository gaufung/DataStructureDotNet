using System;
using System.Collections.Generic;

namespace Sequence.BTree
{
    /// <summary>
    /// 超级节点的定义
    /// </summary>
    /// <typeparam name="T">关键码</typeparam>
    public class BtNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public BtNode<T> Parent { get; set; }//父节点

        /// <summary>
        /// 关键码向量
        /// </summary>
        public List<T> Key { get; private set; }

        /// <summary>
        /// 孩子结点指针向量
        /// <remarks>孩子结点指针向量的数量始终关键码向量的数量多1</remarks>
        /// </summary>
        public List<BtNode<T>> Child { get; private set; }
        /// <summary>
        /// no parameter constructor
        /// </summary>
        public BtNode()
        {
            Parent = null;
            Key = new List<T>();
            Child = new List<BtNode<T>>();
            Child.Insert(0, null);
        }


        /// <summary>
        /// the constructor
        /// </summary>
        /// <param name="e">the value</param>
        /// <param name="lc">left child</param>
        /// <param name="rc">right child</param>
        public BtNode(T e, BtNode<T> lc = null, BtNode<T> rc = null):this()
        {
            Key.Insert(0, e);
            Child[0] = lc;
            Child.Insert(1, rc);
            if (Child[0] != null)
            {
                Child[0].Parent = this;
            }
            if (Child[1] != null)
            {
                Child[1].Parent = this;
            }
        }
    }
}
