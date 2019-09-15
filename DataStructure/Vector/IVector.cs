//-----------------------------------------------------------------------
// <copyright file="IVector.cs" company="gaufung.com">
//     All right reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Sequence
{
    using System;

    /// <summary>
    /// The interface of Vector.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    public interface IVector<T> : IComparable<IVector<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets a value indicating whether the vector is empty.
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// Gets the value from index.
        /// </summary>
        /// <param name="index">The vector index.</param>
        /// <returns>Returns the value.</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// The count of inversion.
        /// </summary>
        /// <returns>Return the count.</returns>
        int DisOrdered();

        /// <summary>
        /// Find the value of e in the unsorted vector.
        /// </summary>
        /// <param name="e">The expect value</param>
        /// <returns>The index.</returns>
        int Find(T e);

        /// <summary>
        /// Find the value of e in unsorted vector from lo to hi [lo,hi).
        /// </summary>
        /// <param name="e">The expect value</param>
        /// <param name="lo">The low index(inclusive)</param>
        /// <param name="hi">The high index(exclusive)</param>
        /// <returns>The index.</returns>
        int Find(T e, int lo, int hi);

        /// <summary>
        /// Search the value of e in sorted vector
        /// </summary>
        /// <param name="e">The expect value.</param>
        /// <returns>The index.</returns>
        int Search(T e);

        /// <summary>
        /// Search the value of e in sorted vector from lo to hi [lo,hi)
        /// </summary>
        /// <param name="e">The expect value</param>
        /// <param name="lo">The low index(inclusive)</param>
        /// <param name="hi">The high index(exclusive)</param>
        /// <returns>The index.</returns>
        int Search(T e, int lo, int hi);

        /// <summary>
        /// Remove the rank of r in vector
        /// </summary>
        /// <param name="r">The to remove index.</param>
        /// <returns>The value</returns>
        T Remove(int r);

        /// <summary>
        /// Remove the vector's values from lo to hi [lo,hi)
        /// </summary>
        /// <param name="lo">The low index(inclusive)</param>
        /// <param name="hi">The high index(exclusive)</param>
        /// <returns>The index.</returns>
        int Remove(int lo, int hi);

        /// <summary>
        /// insert the value of e at the rank of r
        /// </summary>
        /// <param name="r">The to insert index.</param>
        /// <param name="e">The to insert value.</param>
        /// <returns>The index.</returns>
        int Insert(int r, T e);

        /// <summary>
        /// insert the value of e as the last position of vector
        /// </summary>
        /// <param name="e">The to insert value.</param>
        /// <returns>The index.</returns>
        int Insert(T e);

        /// <summary>
        /// Delete the same element in unsorted vector.
        /// </summary>
        /// <returns>The count of duplicated</returns>
        int Deduplicate();

        /// <summary>
        /// Delete the same element in sorted vector
        /// </summary>
        /// <returns>The count of same value.</returns>
        int Uniquify();

        /// <summary>
        /// traverse the vector
        /// </summary>
        /// <param name="action">the delegate</param>
        void Foreach(Action<T> action);

        /// <summary>
        /// Any method if once  element is fitted return true; 
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>Return whether it meets the requirement.</returns>
        bool Any(Func<T, bool> func);

        /// <summary>
        /// Take the first element when condition is required, if not found, returns the default value of T.
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>The value that meets the requirements.</returns>
        T FirstOrDefault(Func<T, bool> func);

        /// <summary>
        /// Take the first element when condition is required
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>The value that meets the requirements.</returns>
        T First(Func<T, bool> func);
    }
}