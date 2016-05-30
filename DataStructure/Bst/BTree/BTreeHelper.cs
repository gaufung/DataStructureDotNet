using System;
using System.Collections.Generic;

namespace Sequence.BTree
{
    [Serializable]
    internal static class BTreeHelper
    {
        /// <summary>
        /// 查找不小于value值的最大index,在有序向量中使用二分查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Search<T>(this List<T> list,T value) where T:IComparable<T>
        {
            int j = list.Count - 1;
            for (; j >= 0; j--)
            {
                if (!list[j].Gt(value))
                    break;
            }
            return j;
        }

        /// <summary>
        /// 从每个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T RemoveFrom<T>(this List<T> list, int index)
        {
            T backup = list[index];
            list.RemoveAt(index);
            return backup;
        }
        /// <summary>
        /// 大于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Gt<T>(this T a, T b) where T : IComparable<T>
        {
            return a .CompareTo(b) > 0;
        }
        /// <summary>
        /// 等于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Eq<T>(this T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) ==0;
        }
        /// <summary>
        /// 小于
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Lt<T>(this T a, T b) where T : IComparable<T>
        {
            return !a.Gt(b);
        }
    }
}
