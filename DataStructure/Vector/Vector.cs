using System;

namespace Vector
{
    public class Vector<T> :IVector<T> 
    {
        private T[] _elem;
        private const int DefaultCapactiry = 4;
        private int _size;
        private int _capacity;

        #region 构造函数
        public Vector()
        {
            _capacity = DefaultCapactiry;
            _elem = new T[_capacity];
            _size = 0;
        }
        private void CopyFrom(T[] source, int lo, int hi)
        {
            _elem=new T[3*(hi-lo)];
            _size = 0;
            while (lo<hi)
            {
                _elem[_size++] = source[lo++];
            }
        }
       

        public Vector(T[] source, int lo, int hi)
        {
            CopyFrom(source,lo,hi);
        }

        public Vector(T[] source,int n)
        {
            CopyFrom(source,0,n);
        }

        public Vector(Vector<T> v,int lo,int hi)
        {
            CopyFrom(v._elem,lo,hi);
        }

        public Vector(Vector<T> v)
        {
            CopyFrom(v._elem,0,v._size);
        }
        #endregion

        #region 扩容和缩小
        /// <summary>
        /// Expand the Vector's Capacity
        /// </summary>
        private void Expand()
        {
            if (_size < _capacity) return;
            if (_capacity < DefaultCapactiry) _capacity = DefaultCapactiry;
            var newElem = new T[_capacity <<= 1];
            for (var i = 0; i < _size; i++)
            {
                newElem[i] = _elem[i];
            }
            _elem = newElem;
        }

        /// <summary>
        /// Shrink the Vector's Capacity
        /// </summary>
        private void Shrink()
        {
            if (_capacity < DefaultCapactiry << 1) return;
            if (_size << 2 > _capacity) return;
            var newElem=new T[_capacity>>=1];
            for (var i = 0; i < _size; i++)
            {
                newElem[i] = _elem[i];
            }
            _elem = newElem;
        } 
        #endregion

        

        #region 属性
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>返回值</returns>
        public T this[int index]
        {

            get
            {
                return _elem[index];
            }
            set
            {
                _elem[index] = value;
            }
        }

        public int Size
        {
            get { return _size; }
        }

        public bool Empty
        {
            get { return _size == 0; }
        }

        #endregion

        #region 比较操作符
        /// <summary>
        /// Greater Than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Gt(T a, T b)
        {
            if (a is IComparable)
            {
                return (a as IComparable).CompareTo(b) == 1;
            }
            throw new InvalidCastException("T is not IComparable");
        }
        /// <summary>
        /// Equal to
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Eq(T a, T b)
        {
            if (a is IComparable)
            {
                return (a as IComparable).CompareTo(b) == 0;
            }
            throw new InvalidCastException("T is not IComparable");
        }
        /// <summary>
        /// Litter than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Lt(T a, T b)
        {
            if (a is IComparable)
            {
                return (a as IComparable).CompareTo(b) == -1;
            }
            throw new InvalidCastException("T is not IComparable");
        }
        #endregion
        

        public int DisOrdered()
        {
            throw new System.NotImplementedException();
        }

        #region 无序查找
        /// <summary>
        /// Find  a　elemt  int the whole vector. If Failed return -1. the vector is not sorted.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int Find(T e)
        {
            return Find(e, 0, _size);
        }

        /// <summary>
        /// Find  a　elemt  int part vector. If Failed return lo-1. the vector is not sorted.
        /// </summary>
        /// <param name="e">to find elem</param>
        /// <param name="lo">lo</param>
        /// <param name="hi">hi</param>
        /// <returns>found index</returns>
        public int Find(T e, int lo, int hi)
        {
            while (lo < hi-- && !Eq(_elem[hi], e))
            {
            }
            return hi;
        }
        #endregion

        

        public int Search(T e)
        {
            throw new System.NotImplementedException();
        }

        public int Serach(T e, int lo, int hi)
        {
            throw new System.NotImplementedException();
        }

        #region 删除元素

        /// <summary>
        /// remove a elem in r index;
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public T Remove(int r)
        {
            T e = _elem[r];
            Remove(r, r + 1);
            return e;
        }

        /// <summary>
        /// remove elemes index range [lo,hi)
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns>the length to remove</returns>
        public int Remove(int lo, int hi)
        {
            while (hi < _size)
            {
                _elem[lo] = _elem[hi];
                lo++;
                hi++;
            }
            _size -= hi - lo;
            Shrink();
            return hi - lo;
        }

        #endregion

        #region 插入元素

        /// <summary>
        /// Insert a elem in the Vector
        /// </summary>
        /// <param name="r">the index to insert</param>
        /// <param name="e">item's value</param>
        /// <returns>the index</returns>
        public int Insert(int r, T e)
        {
            Expand();
            for (int i = _size; i > r; i--)
            {
                _elem[i] = _elem[i - 1];
            }
            _elem[r] = e;
            _size++;
            return r;
        }

        /// <summary>
        /// Insert a elem in the vector tailer
        /// </summary>
        /// <param name="e">elem value</param>
        /// <returns>insert index</returns>
        public int Insert(T e)
        {
            return Insert(_size, e);
        }

        #endregion


        public int Deduplicate()
        {
            throw new System.NotImplementedException();
        }

        public int Uniquify()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Traverse the element in the vector
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(System.Action<T> action)
        {
            for (int i = 0; i < _size; i++)
            {
                action(_elem[i]);
            }
        }



      
    }
}
