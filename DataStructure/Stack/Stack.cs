using System;

namespace Sequence
{
    /// <summary>
    /// the abstract defining of statck
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Stack<T> where T:IComparable<T>
    {
        /// <summary>
        /// whether the stack is empty
        /// </summary>
        public abstract Boolean Empty { get; }

        /// <summary>
        /// get the size of stack
        /// </summary>
        public abstract int Size { get; }

        /// <summary>
        /// push the element
        /// </summary>
        /// <param name="e"></param>
        public abstract void Push(T e);

        /// <summary>
        /// pop the element
        /// </summary>
        /// <returns></returns>
        public abstract T Pop();

        /// <summary>
        /// get the top of the stack
        /// </summary>
        public abstract T Top { get; }
    }
}
