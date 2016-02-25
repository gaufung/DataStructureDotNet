using System;
using System.Collections.Generic;
using System.Globalization;

namespace Sequence
{
    /// <summary>
    /// 串的实现
    /// </summary>
    public class CharSequence:ICharSequence
    {
        private readonly IList<char> _list;

      

        public CharSequence(IEnumerable<char> chars)
        {
            _list=new List<char>(chars);
        }
        public int Length
        {
            get { return _list.Count; }
        }

        public char CharAt(int i)
        {
            return _list[i];
        }

        public ICharSequence SubStr(int i, int k)
        {
            if (i+k>Length)
            {
                throw new ArgumentOutOfRangeException("字符串数目超出要求");
            }
            char[] chars=new char[k];
            int counter = 0;
            while (counter<k)
            {
                chars[counter] = CharAt(i + counter);
                counter++;
            }
            return new CharSequence(chars);
        }

        public ICharSequence Prefix(int k)
        {
            return SubStr(0,k);
        }

        public ICharSequence Suffix(int k)
        {
            int start = Length - k;
            return SubStr(start,k);
        }

        public bool Equals(ICharSequence other)
        {
            if (other.Length != Length) return false;
            for (int i = 0; i < Length; i++)
            {
                if (other.CharAt(i) != CharAt(i)) return false;
            }
            return true;
        }

        public void Concat(ICharSequence other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                _list.Add(other.CharAt(i));
            }
        }

       

        public int IndexOf(ICharSequence substr)
        {
            int res=Match(substr);
            return res >= Length ? -1 : res;
        }

        #region KMP算法

        private int Match(ICharSequence pattern)
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

        private int[] BuildNext(ICharSequence pattern)
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
    }
}
