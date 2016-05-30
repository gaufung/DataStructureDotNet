using System;
using System.Diagnostics;

namespace Sequence
{
    /// <summary>
    /// 字典类抽象类
    /// </summary>
    /// <typeparam name="TK">Key</typeparam>
    /// <typeparam name="TV">Value</typeparam>
    [Serializable]
    [DebuggerDisplay("Size={Size}")]
    public abstract class Dictionary<TK,TV> where TK:IComparable<TK>
    {
        /// <summary>
        /// the Size of the dictionary
        /// </summary>
        public abstract int Size
        {
            get; 
            protected set;
        }

        public abstract TK[] Keys { get; }
        public abstract TV[] Values { get; }
        public bool Empty
        {
            get { return Size==0;}
        }


        /// <summary>
        /// the object indexer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  TV this[TK key]
        {
            get { return Get(key); }
            set { Put(key, value); }
        }

        /// <summary>
        /// put the pair {key,value}
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        protected abstract bool Put(TK key, TV value);

        /// <summary>
        /// get the value of the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected abstract TV Get(TK key);

        /// <summary>
        /// 判断是包含该Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract bool Contains(TK key);

        /// <summary>
        /// 删除某个键
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>the value</returns>
        public abstract TV Remove(TK key);

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action"></param>
        public abstract void Foreach(Action<TK, TV> action);
    }
}
