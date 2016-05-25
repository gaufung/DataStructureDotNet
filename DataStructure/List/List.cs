using System;
using System.Diagnostics;

namespace Sequence
{
    [DebuggerDisplay("Size={Size}")]
    [DebuggerDisplay("Empty={Empty}")]
    [DebuggerDisplay("First={First.Data}")]
    [DebuggerDisplay("Last={Last.Data}")]
    [DebuggerTypeProxy(typeof(List<>.ListDebugView))]
    public class List<T> : IList<T> where T : IComparable<T>
    {
        #region 私有字段

        private int _size;
        private ListNode<T> _header;
        private ListNode<T> _tailer;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _header=new ListNode<T>();
            _tailer=new ListNode<T>();
            _header.Succ = _tailer;
            _tailer.Pred = _header;
            _size = 0;
        }
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            while (_size>0)
            {
                Remove(_header.Succ);
            }
        }

        private void CopyNode(ListNode<T> p, int num)
        {
            Init();
            while (num>0)
            {
                InsertAsLast(p.Data);
                p = p.Succ;
                num--;
            }
        }

        private List()
        {
            Init();
        }

        private List(IList<T> l)
        {
            CopyNode(l.First,l.Size);
        }

        private List(IList<T> l, int from, int num)
        {
            ListNode<T> p = l.First;
            while (from>0)
            {
                p = p.Succ;
                from--;
            }
            CopyNode(p,num);
        }

        private List(ListNode<T> p, int num)
        {
            CopyNode(p,num);
        }
        #endregion

        #region 抽象工厂模式

        public static IList<T> ListFactory()
        {
            return new List<T>();
        }

        public static IList<T> ListFactory(IList<T> list)
        {
            return new List<T>(list);
        }

        public static IList<T> ListFactory(IList<T> list, int from, int num)
        {
            return new List<T>(list, from, num);
        }

        public static IList<T> ListFactory(ListNode<T> p, int num)
        {
            return new List<T>(p,num);
        }
        #endregion


        #region 比较操作符
        /// <summary>
        /// Greater Than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool Gt(T a, T b)
        {

            return a.CompareTo(b) == 1;

        }
        /// <summary>
        /// Equal to
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool Eq(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }
        /// <summary>
        /// Litter than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool Lt(T a, T b)
        {
            return a.CompareTo(b) == -1;
        }
        #endregion
        public int Size
        {
            get { return _size; }
        }

        public bool Empty
        {
            get { return _size == 0; }
        }

        public T this[int index]
        {
            get
            {
                if(index>=_size) throw new IndexOutOfRangeException("索引超出边界");
                ListNode<T> p = First;
                while (index>0)
                {
                    p = p.Succ;
                    index--;
                }
                return p.Data;
            }
            set
            {
                if (index >= _size) throw new IndexOutOfRangeException("索引超出边界");
                ListNode<T> p = First;
                while (index>0)
                {
                    p = p.Succ;
                    index--;
                }
                p.Data = value;
            }
        }

        public ListNode<T> First
        {
            get { return _header.Succ; }
        }

        public ListNode<T> Last
        {
            get { return _tailer.Pred; }
        }

        public ListNode<T> InsertAsFirst(T e)
        {
            _size++;
            return _header.InsertAsSucc(e);
            
        }

        public ListNode<T> InsertAsLast(T e)
        {
            _size++;
            return _tailer.InsertAsPred(e);
        }

        public ListNode<T> InsertAsBefore(ListNode<T> p, T e)
        {
            _size++;
            return p.InsertAsPred(e);
        }

        public ListNode<T> InsertAsAfter(ListNode<T> p, T e)
        {
            _size++;
            return p.InsertAsSucc(e);
        }

        public T Remove(ListNode<T> p)
        {
            _size--;
            p.Succ.Pred = p.Pred;
            p.Pred.Succ = p.Succ;
            return p.Data;
        }

        public int DisOrder()
        {
            ListNode<T> current = _header.Succ;
            int counter = 0;
            while (current != _tailer)
            {
                ListNode<T> p = _header.Succ;
                while (p!=current)
                {
                    if (Lt(current.Data, p.Data)) counter++;
                    p = p.Succ;
                }
                current = current.Succ;
            }
            return counter;
        }

        /// <summary>
        /// 无序查找，从节点p开始，向前查找
        /// </summary>
        /// <param name="e">查找的元素</param>
        /// <param name="n">个数</param>
        /// <param name="p">节点</param>
        /// <returns>该节点的位置</returns>
        public ListNode<T> Find(T e, int n, ListNode<T> p)
        {
            while (n>0)
            {
                if (Eq(p.Pred.Data,e))
                {
                    return p.Pred;
                }
                p = p.Pred;
                n--;
            }
            return null;   
        } 
        public ListNode<T> Find(T e)
        {
            return Find(e, _size, _tailer);
        }
        /// <summary>
        /// 有序查找
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ListNode<T> Search(T e, int n, ListNode<T> p)
        {
            while (n>0)
            {
                if (!Gt(p.Pred.Data,e))
                {
                    break;
                }
                n--;
                p = p.Pred;
            }
            return p.Pred;
        } 
        public ListNode<T> Search(T e)
        {
            return Search(e, _size, _tailer);
        }

        public int Deduplicate()
        {
            if (_size < 2) return 0;
            int oldSize = _size;
            ListNode<T> p = First;
            int r = 0;
            while (p!=_tailer)
            {
                ListNode<T> q = Find(p.Data, r, p);
                if (q==null)
                {
                    r++;             
                }
                else
                {
                    Remove(q);
                }
                p = p.Succ;
            }
            return oldSize - _size;
        }

        public int Uniquify()
        {
            if (_size < 0) return 0;
            int oldSize = _size;
            ListNode<T> p, q;
            for (p=First,q=p.Succ;q!=_tailer;p=q,q=p.Succ)
            {
                if (Eq(p.Data,q.Data))
                {
                    Remove(q);
                    q = p;
                }
            }
            return oldSize - _size;
        }

        public void Foreach(Action<T> action)
        {
            ListNode<T> p;
            for (p=First;p!=_tailer;p=p.Succ)
            {
                action(p.Data);
            }
        }

        public int CompareTo(IList<T> other)
        {
            return Size.CompareTo(other.Size);
        }


        public T Remove(int index)
        {
            if(index>=Size)
                throw new ArgumentException("The index is out of range");
            ListNode<T> p = _header;
            while (index>=0)
            {
                p = p.Succ;
                index--;
            }
            return Remove(p);
        }


        public bool Any(Func<T, bool> func)
        {
            ListNode<T> p = _header.Succ;
            while (p!=_tailer)
            {
                if (func(p.Data))
                    return true;
                p = p.Succ;
            }
            return false;
        }

        public T FirstOrDefault(Func<T, bool> func)
        {
            ListNode<T> p = _header.Succ;
            while (p!=_tailer)
            {
                if (func(p.Data))
                    return p.Data;
                p = p.Succ;
            }
            return default(T);
        }


        public T FirstItme(Func<T, bool> func)
        {
            ListNode<T> p = _header.Succ;
            while (p != _tailer)
            {
                if (func(p.Data))
                    return p.Data;
                p = p.Succ;
            }
            throw new InvalidOperationException("Could not find the first");
        }

        #region Debug工具

        internal class ListDebugView
        {
            private readonly IList<T> _list;

            public ListDebugView(List<T> list)
            {
                _list = list;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public T[] Element
            {
                get
                {
                    T[] element = new T[_list.Size];
                    if (_list.Size == 0) return element;
                    ListNode<T> node = _list.First;
                    int count = 0;
                    while (node!=_list.Last)
                    {
                        element[count] = node.Data;
                        count++;
                        node = node.Succ;
                    }
                    element[count] = _list.Last.Data;
                    return element;
                }
            }
        }
        #endregion
    }
}
