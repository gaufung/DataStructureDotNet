using System;
using System.Diagnostics;
using System.Text;

namespace Sequence
{
    /// <summary>
    /// 串的实现
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Size={Length}")]
    [DebuggerDisplay("Content={_vector}")]
    public class Strings:IString
    {
        private readonly IVector<char> _vector;

        public Strings(char[] chars)
        {
            _vector = Vector<Char>.VectorFactory(chars,chars.Length);
        }
        public int Length
        {
            get { return _vector.Size; }
        } 

        public char CharAt(int i)
        {
            return _vector[i];
        }

        public IString SubStr(int i, int k)
        {
            if (i+k>Length)
            {
                throw new ArgumentOutOfRangeException("The index is out of range");
            }
            char[] chars=new char[k];
            int counter = 0;
            while (counter<k)
            {
                chars[counter] = CharAt(i + counter);
                counter++;
            }
            return new Strings(chars);
        }

        public IString Prefix(int k)
        {
            return SubStr(0,k);
        }

        public IString Suffix(int k)
        {
            int start = Length - k;
            return SubStr(start,k);
        }

        public bool Equals(IString other)
        {
            if (other.Length != Length) return false;
            for (int i = 0; i < Length; i++)
            {
                if (other.CharAt(i) != CharAt(i)) return false;
            }
            return true;
        }

        public void Concat(IString other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                _vector.Insert(other.CharAt(i));
            }
        }

        public int IndexOf(IString substr)
        {
            int res=Match(substr);
            return res >= Length ? -1 : res;
        }

        #region KMP算法

        private int Match(IString pattern)
        {
            int[] next = BuildNext(pattern);
            int i = 0;
            int j = 0;
            int m = pattern.Length;
            int n = Length;
            while (j<m&&i<n)
            {
                if (0 > j || CharAt(i) == pattern.CharAt(j))
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }
            return i - j;

        }

        private int[] BuildNext(IString pattern)
        {
            int j = 0;
            int m = pattern.Length;
            int[] res=new int[m];
            int t = res[0] = -1;
            while (j<m-1)
            {
                if(0>t||pattern.CharAt(j)==pattern.CharAt(t))
                {
                    j++;
                    t++;
                    res[j] = t;
                }
                else
                {
                    t = res[t];
                }
            }
            return res;
        }
        #endregion


        public void Foreach(Action<char> action)
        {
            _vector.Foreach(action);
        }

        public bool Any(Func<char, bool> func)
        {
            return _vector.Any(func);
        }

        public int First(Func<char, bool> func)
        {
            int index = -1;
            for (int i = 0; i < _vector.Size; i++)
            {
                if (func(_vector[i]))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < _vector.Size; i++)
            {
                sb.Append(sb[i]);
            }
            return sb.ToString();
        }


    }
}
