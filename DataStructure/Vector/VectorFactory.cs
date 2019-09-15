//-----------------------------------------------------------------------
// <copyright file="VectorFactory.cs" company="gaufung.com">
//     All right reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Sequence
{
    using System;

    /// <summary>
    /// Vector factory.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    public class VectorFactory<T> where T : IComparable<T>
    {
        /// <summary>
        /// Create an empty vector.
        /// </summary>
        /// <returns>The vector interface.</returns>
        public static IVector<T> Create()
        {
            return new Vector<T>();
        }
        
        /// <summary>
        /// Create a vector from exist array from lo to hi.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="lo">The low index(inclusive)</param>
        /// <param name="hi">The high index(exclusive)</param>
        /// <returns>The vector interface.</returns>
        public static IVector<T> Create(T[] source, int lo, int hi)
        {
            return new Vector<T>(source, lo, hi);
        }

        /// <summary>
        /// Create a vector from exist source by length n.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="n">The length of to copy.</param>
        /// <returns>The vector interface.</returns>
        public static IVector<T> Create(T[] source, int n)
        {
            return new Vector<T>(source, n);
        }
    }
}
