
using System;

namespace Sequence
{
    /// <summary>
    /// 列表的节点类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 
    [Serializable]
    public class ListNode<T>
    {
        #region 构造函数

        /// <summary>
        /// Constructor
        /// </summary>
        public ListNode()
        {
            Data = default(T);
            Pred = Succ = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="pred">pred</param>
        /// <param name="succ">succ</param>
        public ListNode(T data, ListNode<T> pred = null, ListNode<T> succ = null)
        {
            Data = data;
            Pred = pred;
            Succ = succ;
        }

        #endregion

        public ListNode<T> Succ { get; set; }

        public ListNode<T> Pred { get; set; }

        public T Data { get; set; }

        #region 插入函数

        /// <summary>
        /// 当前节点插入前面
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ListNode<T> InsertAsPred(T e)
        {
            var pre = new ListNode<T>(e) {Pred = Pred};
            pre.Pred.Succ = pre;
            Pred = pre;
            pre.Succ = this;
            return pre;
        }
        /// <summary>
        /// 当前节点后面插入
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ListNode<T> InsertAsSucc(T e)
        {
            var s = new ListNode<T>(e) {Succ = Succ};
            s.Succ.Pred = s;
            Succ = s;
            s.Pred = this;
            return s;
        }

        public override string ToString()
        {
            return string.Format("The Node Data Value is {0}", Data);
        }

        #endregion
    }
}
