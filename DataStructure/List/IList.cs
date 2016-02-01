using System;
namespace List
{
    interface IList<T>
    {
        int Size { get; }
        bool Empty { get; }

        T this[int index] { get; set; }

        ListNode<T> First { get; }
        ListNode<T> Last { get; }
        ListNode<T> InsertAsFirst(T e);
        ListNode<T> InsertAsLast(T e);

        ListNode<T> InsertAsBefore(ListNode<T> p, T e);

        ListNode<T> InsertAsAfter(ListNode<T> p, T e);

        T Remove(ListNode<T> p);

        int DisOrder();
        ListNode<T> Find(T e);

        ListNode<T> Find(T e,int n,ListNode<T> p);

        ListNode<T> Search(T e);
        ListNode<T> Search(T e,int n,ListNode<T> p);

        int Deduplicate();
        int Uniquify();

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">操作委托</param>
        void Traverse(Action<T> action);
    }
}
