using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Tree;

namespace Bst.BTree
{
    public static class BTreeHelper
    {
        /// <summary>
        /// 有序列表查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int Search<T>(this List<T> list, T e) where T : IComparable
        {
            int lo = 0;
            int hi = list.Count;
            while (lo<hi)
            {
                int mi = (lo + hi) >> 1;
                if (e.Lt(list[mi]))
                {
                    hi = mi;
                }
                else if (e.Gt(list[mi]))
                {
                    lo = mi + 1;
                }
                else
                {
                    return mi;
                }
            }
            return lo-1;
        }

        public static int Size<T>(this List<T> list) 
        {
            return list.Count;
        }

        public static T RemoveFrom<T>(this List<T> list, int index)
        {
            T a = list[index];
            list.RemoveAt(index);
            return a;
        }

        public static bool Empty<T>(this List<T> list) where T : IComparable
        {
            return list.Size() == 0;
        }
        public static bool Gt<T>(this T a, T b) where T : IComparable
        {
            return (a as IComparable).CompareTo(b) == 1;
        }
        public static bool Lt<T>(this T a, T b) where T : IComparable
        {
            return (a as IComparable).CompareTo(b) == -1;
        }
        public static bool Eq<T>(this T a, T b) where T : IComparable
        {
           return   (a as IComparable).CompareTo(b) == 0;
        }
    }
}
