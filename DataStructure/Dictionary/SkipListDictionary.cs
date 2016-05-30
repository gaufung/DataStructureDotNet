using System;

namespace Sequence
{
    [Serializable]
    public class SkipListDictionary<TK,TV>:Dictionary<TK,TV> where TK:IComparable<TK>
    {
        private const int MaxLevel = 6;

        private int _level;

        private int _size;

        private SkipNode<TK,TV> _header;

        public SkipListDictionary()
        {
            _header=new SkipNode<TK, TV>(MaxLevel);
            _level = 0;
            _size = 0;
        }

        public override int Size
        {
            get
            {
                return _size;
            }
            protected set { _size = value; }
        }

        public override TK[] Keys
        {
            get
            {
                TK[] keys=new TK[Size];
                SkipNode<TK, TV> x = _header;
                int counter = 0;
                while (x.Forward[0]!=null)
                {
                    keys[counter++] = x.Forward[0].Key;
                    x = x.Forward[0];
                }
                return keys;
            }
        }

        public override TV[] Values
        {
            get
            {
                TV[] values = new TV[Size];
                SkipNode<TK, TV> x = _header;
                int counter = 0;
                while (x.Forward[0] != null)
                {
                    values[counter++] = x.Forward[0].Value;
                    x = x.Forward[0];
                }
                return values;
            }
        }

        protected override bool Put(TK key, TV value)
        {
            SkipNode<TK, TV> x = _header;
            SkipNode<TK, TV>[] update = new SkipNode<TK, TV>[MaxLevel + 1];
            for (int i = _level; i >= 0; i--)
            {
                while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
                {
                    x = x.Forward[i];
                }
                update[i] = x;
            }
            x = x.Forward[0];
            if (x == null || x.Key.CompareTo(key) != 0)
            {
                int lvl = MathHelper.RandomLevel(MaxLevel);
                if (lvl > _level)
                {
                    for (int i = _level + 1; i <= lvl; i++)
                    {
                        update[i] = _header;
                    }
                    _level = lvl;
                }
                x = new SkipNode<TK,TV>(lvl, key,value);
                _size++;
                for (int i = 0; i <= lvl; i++)
                {
                    x.Forward[i] = update[i].Forward[i];
                    update[i].Forward[i] = x;
                }
            }
            if (x.Key.CompareTo(key) == 0)
            {
                x.Value = value;
            }
            return true;
        }

        protected override TV Get(TK key)
        {
            SkipNode<TK, TV> x = _header;          
            for (int i = _level; i >= 0; i--)
            {
                while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
                {
                    x = x.Forward[i];
                }
            }
            if (x.Forward[0].Key.CompareTo(key) == 0)
                return x.Forward[0].Value;
            else 
                throw new ArgumentException("The Key was not found");
        }

        public override bool Contains(TK key)
        {
            SkipNode<TK,TV> x = _header;
            for (int i = MaxLevel; i >= 0; i--)
            {
                while (x.Forward[i]!=null&&x.Forward[i].Key.CompareTo(key)<0)
                {
                    x = x.Forward[i];
                }
            }
            x = x.Forward[0];
            return x != null && x.Key.CompareTo(key) == 0;
        }

        public override TV Remove(TK key)
        {
            SkipNode<TK, TV> x = _header;
            SkipNode<TK, TV>[] updateNode = new SkipNode<TK, TV>[MaxLevel + 1];
            for (int i = _level; i >= 0; i--)
            {
                while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
                {
                    x = x.Forward[i];
                }
                updateNode[i] = x;
            }
            x = x.Forward[0];
            if (x.Key.CompareTo(key) == 0)
            {
                for (int i = 0; i <= _level; i++)
                {
                    if (updateNode[i].Forward[i] != x)
                    {
                        break;
                    }
                    updateNode[i].Forward[i] = x.Forward[i];
                }
                while (_level > 0 && _header.Forward[_level] == null)
                {
                    _level--;
                }
                _size--;
            }
            else
            {
                throw new ArgumentException("The Key was not found");
            }
            return x.Value;
        }

        public override void Foreach(Action<TK, TV> action)
        {
            SkipNode<TK, TV> x = _header;
            while (x.Forward[0] != null)
            {
                action(x.Key, x.Value);
            }
        }
    }
}
