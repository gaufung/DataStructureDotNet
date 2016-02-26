using System;
using System.Globalization;

namespace Sequence
{
    /// <summary>
    /// 排序算法
    /// </summary>
    public static  class Sort<T> where T:IComparable<T>
    {
        #region 辅助

        /// <summary>
        /// a 大于 b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static Boolean Gt(T a, T b)
        {
            return (a as IComparable<T>).CompareTo(b) > 0;
        }
        /// <summary>
        /// a 小于 b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static Boolean Lt(T a, T b)
        {
            return (a as IComparable<T>).CompareTo(b) < 0;
        }
        /// <summary>
        /// a 等于 b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static Boolean Eq(T a, T b)
        {
            return (a as IComparable<T>).CompareTo(b) == 0;
        }

        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="items"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        static void Swap(T[] items, int i, int j)
        {
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
        #endregion

        #region 快速排序

        /// <summary>
        /// 快排
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void QuickSort(T[] nums)
        {
            
        }

        #endregion

        #region 冒泡排序
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void BubbleSort(T[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n-1; i++)
            {
                int maxKey = 0;
                bool sorted = true;
                for (int j = 1; j < n - i; j++)
                {
                    if (Gt(nums[maxKey], nums[j]))
                    {
                        Swap(nums,maxKey,j);
                        sorted = false;                        
                    }
                    maxKey = j;
                }
                if (sorted)
                {
                    break;
                }
            }

        }
        #endregion

        #region 选择排序
        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void SelectionSort(T[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i < n-1; i++)
            {
                int minKey = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Lt(nums[j], nums[minKey]))
                        minKey = j;
                }
                Swap(nums,minKey,i);
            }
        }
        #endregion

        #region 插入排序
        /// <summary>
        /// 快排
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void InsertSort(T[] nums)
        {
            int n = nums.Length;
            for (int i = 1; i < n; i++)
            {
                T backup = nums[i];
                int j = i - 1;
                for (; j >=0; j--)
                {
                    if (Gt(nums[j], backup))
                        nums[j + 1] = nums[j];
                    else
                    {
                       break;
                    }
                }
                nums[j + 1] = backup;
            }
        }
        #endregion

        #region 归并排序
        /// <summary>
        /// 归并排序
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void MergeSort(T[] nums)
        {
            
        }
        #endregion

        #region 堆排序
        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void HeapSort(T[] nums)
        {
            
        }
        #endregion
    }
}
