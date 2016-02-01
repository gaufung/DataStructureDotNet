using System;

namespace Sequence
{
    /// <summary>
    /// the abstract class the queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Queue<T> where T:IComparable<T>
    {
        /// <summary>
        /// whether the queue is empty
        /// </summary>
        public abstract bool Empty { get; }

        /// <summary>
        /// the front value of the queue
        /// </summary>
        public abstract T Front { get; }

        /// <summary>
        /// de queue
        /// </summary>
        /// <returns></returns>
        public abstract T Dequeue();
        /// <summary>
        /// en queue
        /// </summary>
        /// <param name="e"></param>
        public abstract void Enqueue(T e);

        /// <summary>
        /// the size of the queue
        /// </summary>
        public abstract int Size { get; }
    }
}
