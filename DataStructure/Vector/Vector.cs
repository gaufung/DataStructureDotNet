//-----------------------------------------------------------------------
// <copyright file="Vector.cs" company="gaufung.com">
//     All right reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Sequence
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Vector array.
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    [Serializable]
    [DebuggerDisplay("Size={Size}")]
    [DebuggerDisplay("Empty={Empty}")]
    [DebuggerTypeProxy(typeof(Vector<>.VectorDebugView))]
    public class Vector<T> : IVector<T> where T : IComparable<T>
    {
        /// <summary>
        /// Default capacity.
        /// </summary>
        private const int DefaultCapacity = 4;

        /// <summary>
        /// Array to store all elements.
        /// </summary>
        private T[] elements;

        /// <summary>
        /// Current array size.
        /// </summary>
        private int size;

        /// <summary>
        /// Array's capacity.
        /// </summary>
        private int capacity;

        /// <summary>
        /// Initializes a new instance of the  <see cref="Vector{T}"/> class.
        /// </summary>
        internal Vector()
        {
            this.capacity = DefaultCapacity;
            this.elements = new T[this.capacity];
            this.size = 0;
        }

        /// <summary>
        /// Initializes a new instance of the  <see cref="Vector{T}"/> class.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="lo">The low index.</param>
        /// <param name="hi">The high index.</param>
        internal Vector(T[] source, int lo, int hi)
        {
            this.CopyFrom(source, lo, hi);
        }

        /// <summary>
        /// Initializes a new instance of the  <see cref="Vector{T}"/> class.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="n">The length.</param>
        internal Vector(T[] source, int n)
        {
            this.CopyFrom(source, 0, n);
        }

        /// <summary>
        /// Gets the size of vector.
        /// </summary>
        public int Size => this.size;

        /// <summary>
        /// Gets a value indicating whether the vector is empty.
        /// </summary>
        public bool Empty => this.size == 0;

        /// <summary>
        /// Gets or sets the element at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The elements. </returns>
        public T this[int index]
        {
            get
            {
                if (index >= this.size)
                {
                    throw new IndexOutOfRangeException("index");
                }

                return this.elements[index];
            }

            set
            {
                if (index >= this.size)
                {
                    throw new IndexOutOfRangeException("index");
                }

                this.elements[index] = value;
            }
        }

        /// <summary>
        /// Find an element int the whole vector. If Failed return -1. the vector is not sorted.
        /// </summary>
        /// <param name="e">the to find element.</param>
        /// <returns>The index.</returns>
        public int Find(T e)
        {
            return this.Find(e, 0, this.size);
        }

        /// <summary>
        /// Find an element in the vector.
        /// </summary>
        /// <param name="e">to find element.</param>
        /// <param name="lo">The low index.</param>
        /// <param name="hi">The high index.</param>
        /// <returns>Found index</returns>
        public int Find(T e, int lo, int hi)
        {
            while (true)
            {
                if (lo < hi-- && !Eq(this.elements[hi], e))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            return hi;
        }

        /// <summary>
        /// Search the value e in the vector.
        /// </summary>
        /// <param name="e">The expected value.</param>
        /// <returns>Return the first index.</returns>
        public int Search(T e)
        {
            return this.Search(e, 0, this.size);
        }

        /// <summary>
        /// Search the value e in the vector.
        /// </summary>
        /// <param name="e">The expected value.</param>
        /// <param name="lo">The lower index.</param>
        /// <param name="hi">The higher index.</param>
        /// <returns>Returns the first index.</returns>
        public int Search(T e, int lo, int hi)
        {
            var random = new Random();
            return (random.Next() % 2 == 0) ? BinSearch(this.elements, lo, hi, e) : FibSearch(this.elements, lo, hi, e);
        }

        /// <summary>
        /// remove a elem in r index;
        /// </summary>
        /// <param name="r">the index to remove</param>
        /// <returns>The to remove element.</returns>
        public T Remove(int r)
        {
            T e = this.elements[r];
            this.Remove(r, r + 1);
            return e;
        }

        /// <summary>
        /// remove elements index range [lo,hi)
        /// </summary>
        /// <param name="lo">The high index.</param>
        /// <param name="hi">The low index.</param>
        /// <returns>the length to remove</returns>
        public int Remove(int lo, int hi)
        {
            while (hi < this.size)
            {
                this.elements[lo] = this.elements[hi];
                lo++;
                hi++;
            }

            this.size -= hi - lo;
            this.Shrink();
            return hi - lo;
        }

        /// <summary>
        /// Insert a elem in the Vector
        /// </summary>
        /// <param name="r">the index to insert</param>
        /// <param name="e">item's value</param>
        /// <returns>the index</returns>
        public int Insert(int r, T e)
        {
            this.Expand();
            for (int i = this.size; i > r; i--)
            {
                this.elements[i] = this.elements[i - 1];
            }

            this.elements[r] = e;
            this.size++;
            return r;
        }

        /// <summary>
        /// Insert an element in the vector tail.
        /// </summary>
        /// <param name="e">element value</param>
        /// <returns>insert index</returns>
        public int Insert(T e)
        {
            return this.Insert(this.size, e);
        }

        /// <summary>
        /// Calculate the reverse count
        /// </summary>
        /// <returns>The count of disorder elements.</returns>
        public int DisOrdered()
        {
            int counter = 0;
            for (int i = 1; i < this.size; i++)
            {
                int n = 0;
                while (n < i)
                {
                    if (Lt(this[i], this[n]))
                    {
                        counter++;
                    }

                    n++;
                }
            }

            return counter;
        }

        /// <summary>
        /// Unique the vector.
        /// </summary>
        /// <returns>The duplicated element count.</returns>
        public int Deduplicate()
        {
            int oldSize = this.size;
            int i = 1;
            while (i < this.size)
            {
                if (this.Find(this.elements[i], 0, i) < 0)
                {
                    i++;
                }
                else
                {
                    this.Remove(i);
                }
            }

            this.Shrink();
            return oldSize - this.size;
        }

        /// <summary>
        /// Uniquify the elements in ordered vector.
        /// </summary>
        /// <returns>The unique elements count.</returns>
        public int Uniquify()
        {
            int i = 0, j = 0;
            while (++j < this.size)
            {
                if (!Eq(this.elements[i], this.elements[j]))
                {
                    i++;
                    this.elements[i] = this.elements[j];
                }
            }

            this.size = ++i;
            this.Shrink();
            return j - i;
        }

        /// <summary>
        /// Foreach the element in the vector
        /// </summary>
        /// <param name="action">The action for each elements.</param>
        public void Foreach(Action<T> action)
        {
            for (int i = 0; i < this.size; i++)
            {
                action(this.elements[i]);
            }
        }

        /// <summary>
        /// Compare to Vector.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>Return ture if this vector bigger than other.</returns>
        public int CompareTo(IVector<T> other)
        {
            return this.Size.CompareTo(other.Size);
        }

        /// <summary>
        /// Any element meet the require.
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>The return type.</returns>
        public bool Any(Func<T, bool> func)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (func(this.elements[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// First or default element which meet the requirement.
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>The value</returns>
        public T FirstOrDefault(Func<T, bool> func)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (func(this.elements[i]))
                {
                    return this.elements[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// First element which meets the requirement
        /// </summary>
        /// <param name="func">The filter.</param>
        /// <returns>The value.</returns>
        public T First(Func<T, bool> func)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (func(this.elements[i]))
                {
                    return this.elements[i];
                }
            }

            throw new InvalidOperationException("Could not found");
        }

        /// <summary>
        /// Binary Search
        /// </summary>
        /// <param name="elem">the array</param>
        /// <param name="lo">The lower index.</param>
        /// <param name="hi">The higher index.</param>
        /// <param name="e">The expected value.</param>
        /// <returns>The index.</returns>
        private static int BinSearch(T[] elem, int lo, int hi, T e)
        {
            while (lo < hi)
            {
                int mi = (lo + hi) >> 1;
                if (Lt(e, elem[mi]))
                {
                    hi = mi;
                }
                else
                {
                    if (Lt(elem[mi], e))
                    {
                        lo = mi + 1;
                    }
                    else
                    {
                        return mi;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Fibonacci Search
        /// </summary>
        /// <param name="elem">the array</param>
        /// <param name="lo">The lower index.</param>
        /// <param name="hi">The higher index.</param>
        /// <param name="e">The expected value.</param>
        /// <returns>The index.</returns>
        private static int FibSearch(T[] elem, int lo, int hi, T e)
        {
            Fib fib = new Fib(hi - lo);
            while (lo < hi)
            {
                while (hi - lo < fib.Value)
                {
                    fib.Prev();
                }

                int mi = lo + fib.Value - 1;
                if (Lt(e, elem[mi]))
                {
                    hi = mi;
                }
                else
                {
                    if (Lt(elem[mi], e))
                    {
                        lo = mi + 1;
                    }
                    else
                    {
                        return mi;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Greater Than
        /// </summary>
        /// <param name="a">left value</param>
        /// <param name="b">right value</param>
        /// <returns>True if left value greater than right value.</returns>
        private static bool Gt(T a, T b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Equal to
        /// </summary>
        /// <param name="a">left value</param>
        /// <param name="b">right value</param>
        /// <returns>True if left value equal to right value</returns>
        private static bool Eq(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        /// <summary>
        /// Litter than
        /// </summary>
        /// <param name="a">left value</param>
        /// <param name="b">right value</param>
        /// <returns>True if left value less than right value.</returns>
        private static bool Lt(T a, T b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Copy source array to this vector.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="lo">The low index.</param>
        /// <param name="hi">The high index.</param>
        private void CopyFrom(T[] source, int lo, int hi)
        {
            this.elements = new T[3 * (hi - lo)];
            this.size = 0;
            while (lo < hi)
            {
                this.elements[this.size] = source[lo];
                this.size++;
                lo++;
            }
        }

        /// <summary>
        /// Expand the Vector's Capacity
        /// </summary>
        private void Expand()
        {
            if (this.size < this.capacity)
            {
                return;
            }

            if (this.capacity < DefaultCapacity)
            {
                this.capacity = DefaultCapacity;
            }

            var newElem = new T[this.capacity <<= 1];
            for (var i = 0; i < this.size; i++)
            {
                newElem[i] = this.elements[i];
            }

            this.elements = newElem;
        }

        /// <summary>
        /// Shrink the Vector's Capacity
        /// </summary>
        private void Shrink()
        {
            if (this.capacity < DefaultCapacity << 1)
            {
                return;
            }

            if (this.size << 2 > this.capacity)
            {
                return;
            }

            var newElem = new T[this.capacity >>= 1];
            for (var i = 0; i < this.size; i++)
            {
                newElem[i] = this.elements[i];
            }

            this.elements = newElem;
        }

        /// <summary>
        /// The debug view.
        /// </summary>
        internal class VectorDebugView
        {
            /// <summary>
            /// The vector.
            /// </summary>
            private readonly Vector<T> vector;

            /// <summary>
            /// Initializes a new instance of the  <see cref="VectorDebugView"/> class.
            /// </summary>
            /// <param name="vertor">The vector.</param>
            public VectorDebugView(Vector<T> vertor)
            {
                this.vector = vertor;
            }

            /// <summary>
            /// Gets the vector elements.
            /// </summary>
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public T[] Elements
            {
                get
                {
                    T[] elements = new T[this.vector.Size];
                    for (int i = 0; i < this.vector.Size; i++)
                    {
                        elements[i] = this.vector[i];
                    }

                    return elements;
                }
            }
        }
    }
}
