using System;

namespace Vector
{
    interface IVector<T>
    {      
        int Size { get;}
        bool Empty { get; }

        T this[int index] { get; set; }
        int DisOrdered();
        int Find(T e);

        int Find(T e, int lo, int hi);
        int Search(T e);
        int Search(T e, int lo, int hi);
        T Remove(int r);
        int Remove(int lo, int hi);
        int Insert(int r, T e);
        int Insert(T e);
        int Deduplicate();
        int Uniquify();
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">操作委托</param>
        void Traverse(Action<T> action);


    }
}
