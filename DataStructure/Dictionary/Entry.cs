using System;
using System.Diagnostics;

namespace Sequence
{
    /// <summary>
    /// 词条
    /// </summary>
    /// <typeparam name="TK">the key type</typeparam>
    /// <typeparam name="TV">the value type</typeparam>
    internal class Entry<TK, TV> where TK : IComparable<TK>
    {
        
        /// <summary>
        /// the key
        /// </summary>
        public TK Key { get; set; }
        /// <summary>
        /// the value
        /// </summary>
        public TV Value { get; set; }

        /// <summary>
        /// is the entry  valid or not
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// is the entry null or not
        /// </summary>
        public bool IsNotNull { get; set; }
         
        public Entry(TK key=default(TK),TV value=default(TV),bool isAlive=false,bool isNotNull=false)
        {
            Key = key;
            Value = value;
            IsAlive = isAlive;
            IsNotNull = isNotNull;
        }
        /// <summary>
        /// get hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
