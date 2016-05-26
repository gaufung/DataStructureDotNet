using System;
using System.Diagnostics;

namespace Sequence
{
    /// <summary>
    /// Hash字典实现
    /// </summary>
    /// <typeparam name="TK">key</typeparam>
    /// <typeparam name="TV">value</typeparam>
    //[DebuggerTypeProxy(typeof(Sequence.HashDictionary<>.HashDictionaryDebugView))]
    public class HashDictionary<TK, TV> : Dictionary<TK, TV> where TK : IComparable<TK>
    {
        private static readonly int DEFAULT_TABLE_SIZE = 101;

        private Entry<TK, TV>[] _element;

        private int _size;

        public HashDictionary()
        {
            _element=new Entry<TK, TV>[DEFAULT_TABLE_SIZE];
            Reset();
        }

        private void Reset()
        {
            for (int i = 0; i < _element.Length; i++)
            {
                _element[i]=new Entry<TK, TV>();
            }
        }

        private void Rehash()
        {
            if (2*_size < _element.Length)
                return;
            int nextCount = MathHelper.NextPrime(4*_size);
            Entry<TK,TV>[] backup=new Entry<TK, TV>[_size];
            int counter = 0;
            for (int i = 0; i < _element.Length; i++)
            {
                if (_element[i].IsAlive)
                    backup[counter++] = _element[i];
            }
            _element=new Entry<TK, TV>[nextCount];
            Reset();
            foreach (var entry in backup)
            {
                Put(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// 寻找要插入词条的位置
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        private int FindPos(TK key)
        {
            int collisionNum = 0;
            int pos = Math.Abs(key.GetHashCode() % _element.Length);
            while (_element[pos].IsNotNull)
            {
                if(_element[pos].Key.CompareTo(key)==0)
                    break;
                pos += 2 * (++collisionNum) - 1;
                if (pos >= _element.Length)
                    pos -= _element.Length;
            }
            return pos;
        }

        #region to override function
        public override int Size
        {
            get
            {
                return _size;
            }
            protected set
            {
                _size=value;
            }
        }


        protected override bool Put(TK key, TV value)
        {
            int pos = FindPos(key);
            if(_element[pos].IsNotNull)
                _element[pos].Value = value;
            else
            {
                _element[pos]=new Entry<TK, TV>(key,value);
            }
            _element[pos].IsNotNull = true;
            _element[pos].IsAlive = true;
            _size++;
            Rehash();
            return true;
        }

        protected override TV Get(TK key)
        {
            if (!Contains(key))
                throw new ArgumentException("The key was not found");
            int pos = FindPos(key);
            return _element[pos].Value;
        }

        public override bool Contains(TK key)
        {
            int pos = FindPos(key);
            if (!_element[pos].IsNotNull)
                return false;
            return _element[pos].IsAlive;
        }

        public override TV Remove(TK key)
        {
            if(!Contains(key))
                throw new ArgumentException("The key was not found");
            int pos = FindPos(key);
            _element[pos].IsAlive = false;
            _size--;
            return _element[pos].Value;
        }

        public override void Foreach(Action<TK, TV> action)
        {
            foreach (Entry<TK, TV> t in _element)
            {
                if (t.IsAlive)
                    action(t.Key, t.Value);
            }
        }

        public override TK[] Keys
        {
            get
            {
                TK[] keys=new TK[_size];
                int counter = 0;
                foreach (Entry<TK, TV> t in _element)
                {
                    if (t.IsAlive)
                        keys[counter++] = t.Key;
                }
                return keys;
            }
        }

        public override TV[] Values
        {
            get
            {
                TV[] values=new TV[_size];
                int counter = 0;
                foreach (Entry<TK, TV> t in _element)
                {
                    if (t.IsAlive)
                        values[counter++] = t.Value;
                }
                return values;
            }
        }
        #endregion


        #region for debug

        internal class HashDictionaryDebugView
        {
            private HashDictionary<TK, TV> _dic;

            public HashDictionaryDebugView(HashDictionary<TK, TV> dic)
            {
                _dic = dic;
            }
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]  
            public string[] KeyValue
            {
                get
                {
                    string[] strings=new string[_dic.Size];
                    TK[] keys = _dic.Keys;
                    TV[] values = _dic.Values;
                    for (int i = 0; i < _dic.Size; i++)
                    {
                        strings[i] = "Key:" + keys[i] + ",Value:" + values[i];
                    }
                    return strings;
                }
            }
        }
        #endregion
    }
}
