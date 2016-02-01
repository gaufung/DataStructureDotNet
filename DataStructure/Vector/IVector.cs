using System;

namespace Sequence
{
    public interface IVector<T> where T:IComparable<T>
    {
        /// <summary>
        /// The size of Vector
        /// </summary>
        int Size { get; }

        /// <summary>
        /// if the vector is Empty or not
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// this object indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int index] { get; set; }

        /// <summary>
        /// the count of inversion
        /// </summary>
        /// <returns></returns>
        int DisOrdered();

        /// <summary>
        /// find the value of e in the unsorted vector
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        int Find(T e);

        /// <summary>
        /// find the value of e in unsorted vector from lo to hi [lo,hi)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        int Find(T e, int lo, int hi);

        /// <summary>
        /// search the value of e in sorted vector
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        int Search(T e);

        /// <summary>
        /// seach the value of e in sorted vector from lo to hi [lo,hi)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        int Search(T e, int lo, int hi);

        /// <summary>
        /// remove the rank of r in vector
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        T Remove(int r);

        /// <summary>
        /// remove the vector's values from lo to hi [lo,hi)
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        int Remove(int lo, int hi);

        /// <summary>
        /// insert the value of e at the rank of r
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        int Insert(int r, T e);

        /// <summary>
        /// insert the value of e as the last position of vector
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        int Insert(T e);
        /// <summary>
        /// delete the same element in unsorted vecotr
        /// </summary>
        /// <returns></returns>
        int Deduplicate();

        /// <summary>
        /// delete the same element in sorted vector
        /// </summary>
        /// <returns></returns>
        int Uniquify();

        /// <summary>
        /// traverse the vector
        /// </summary>
        /// <param name="action">the delegate</param>
        void Traverse(Action<T> action);
    }
}
