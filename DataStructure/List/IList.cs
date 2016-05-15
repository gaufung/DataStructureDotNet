using System;

namespace Sequence
{
    /// <summary>
    /// the interface of List
    /// </summary>
    /// <typeparam name="T">type argument</typeparam>
    public interface IList<T>
    {
        /// <summary>
        /// the size of list
        /// </summary>
        int Size { get; }

        /// <summary>
        /// whether the list is empty
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// the object index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int index] { get; set; }

        /// <summary>
        /// the first element
        /// </summary>
        ListNode<T> First { get; }
        /// <summary>
        /// the last element
        /// </summary>
        ListNode<T> Last { get; }

        /// <summary>
        /// insert the value of e as the first element
        /// </summary>
        /// <param name="e">the value</param>
        /// <returns></returns>
        ListNode<T> InsertAsFirst(T e);

        /// <summary>
        /// insert the value of e as the last element
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        ListNode<T> InsertAsLast(T e);

        /// <summary>
        /// insert the value of e before the position of p
        /// </summary>
        /// <param name="p">the position</param>
        /// <param name="e">the element of e</param>
        /// <returns>the insert value</returns>
        ListNode<T> InsertAsBefore(ListNode<T> p, T e);

        /// <summary>
        /// insert the value of e after the position of p
        /// </summary>
        /// <param name="p">the position</param>
        /// <param name="e">the element</param>
        /// <returns>the insert value</returns>
        ListNode<T> InsertAsAfter(ListNode<T> p, T e);

        /// <summary>
        /// remove position of p
        /// </summary>
        /// <param name="p">the position</param>
        /// <returns></returns>
        T Remove(ListNode<T> p);

        /// <summary>
        /// count the inversion
        /// </summary>
        /// <returns></returns>
        int DisOrder();
        /// <summary>
        /// find the element in unsorted list
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        ListNode<T> Find(T e);

        /// <summary>
        /// find the element from position forward n position in 
        /// </summary>
        /// <param name="e">the element value</param>
        /// <param name="n">the forward elements count</param>
        /// <param name="p">the position</param>
        /// <returns>the position</returns>
        ListNode<T> Find(T e,int n,ListNode<T> p);

        /// <summary>
        /// search the element of e in sorted list
        /// </summary>
        /// <param name="e">the element</param>
        /// <returns></returns>
        ListNode<T> Search(T e);

        /// <summary>
        /// search the element of e from position of p 
        /// forward n elements in sorted list
        /// </summary>
        /// <param name="e">the element of e</param>
        /// <param name="n">n elements</param>
        /// <param name="p">the position</param>
        /// <returns></returns>
        ListNode<T> Search(T e,int n,ListNode<T> p);

        /// <summary>
        /// delete the same element in unsorted list
        /// </summary>
        /// <returns></returns>
        int Deduplicate();

        /// <summary>
        /// delete the same element in sorted list
        /// </summary>
        /// <returns></returns>
        int Uniquify();

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">操作委托</param>
        void Foreach(Action<T> action);

        void Clear();
    }
}
