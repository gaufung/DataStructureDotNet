
using System.Threading;

namespace List
{
    /// <summary>
    /// 列表的节点类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListNode<T>
    {
        private T _data;
        private ListNode<T> _pred;
        private ListNode<T> _succ;

        #region 构造函数

        public ListNode()
        {
            _data = default(T);
            _pred = _succ = null;
        }

        public ListNode(T data, ListNode<T> pred = null, ListNode<T> succ = null)
        {
            _data = data;
            _pred = pred;
            _succ = succ;
        }

        #endregion

        public ListNode<T> Succ
        {
            get { return _succ; }
            set { _succ = value; }
        }

        public ListNode<T> Pred
        {
            get { return _pred; }
            set { _pred = value; }
        }

        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }
        #region 插入函数

        /// <summary>
        /// 当前节点插入前面
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ListNode<T> InsertAsPred(T e)
        {
            var pre = new ListNode<T>(e, null, null);
            pre.Pred = this.Pred;
            pre.Pred.Succ = pre;
            this.Pred = pre;
            pre.Succ = this;
            return pre;
        }

        public ListNode<T> InsertAsSucc(T e)
        {
            var s = new ListNode<T>(e, null, null);
            s.Succ = this.Succ;
            s.Succ.Pred = s;
            this.Succ = s;
            s.Pred = this;
            return s;
        }

        public override string ToString()
        {
            return string.Format("The Node Data Value is {0}", _data.ToString());
        }

        #endregion
    }
}
