using System;
using System.Globalization;
using System.Linq;

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
        public static Boolean Gt(T a, T b)
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
            QuickSort(nums,0,nums.Length);
        }

        private static void QuickSort(T[] nums, int lo, int hi)
        {
            if(hi-lo<2)return;
            int mi = Partition(nums,lo,hi - 1);
            QuickSort(nums,lo,mi);
            QuickSort(nums,mi+1,hi);
        }
        /// <summary>
        /// 获取轴点[lo,hi]
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private static int Partition(T[] nums, int lo, int hi)
        {
            Swap(nums,lo,new Random().Next()%(hi-lo));
            T pivot = nums[lo];
            while (lo<hi)
            {
                while (lo < hi && !Gt(pivot, nums[hi])) 
                    hi--;
                nums[lo] = nums[hi];
                while (lo < hi && !Gt(nums[lo], pivot)) 
                    lo++;
                nums[hi] = nums[lo];
            }
            nums[lo] = pivot;
            return lo;
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
            MergetSort(nums,0,nums.Length);
        }

        private static void MergetSort(T[] nums, int lo, int hi)
        {
            if (hi - lo < 2) return;
            int mi = (lo + hi) >> 1;
            MergetSort(nums,lo,mi);
            MergetSort(nums,mi,hi);
            Merge(nums,lo,mi,hi);
        }
        private static void Merge(T[] nums, int lo, int mi, int hi)
        {
            int m = mi - lo;
            int n = hi - mi;
            T[] temp=new T[m];
            int i;
            for (i = lo; i < mi; i++)
            {
                temp[i - lo] = nums[i];
            }
            i = 0;
            int j = 0;
            int k = lo;
            while (i<m&&j<n)
            {
                if (Lt(nums[mi + j], temp[i]))
                {
                    nums[k++] = nums[mi + j];
                    j++;
                }
                else
                {
                    nums[k++] = temp[i];
                    i++;
                }
            }
            if (i<m)
            {
                for (; i<m; i++)
                {
                    nums[k++] = temp[i];
                }
            }
            if (j < n)
            {
                for (; j < n; j++)
                    nums[k++] = nums[mi + j];
            }
        }
        #endregion

        #region 堆排序
        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="nums">待排序的数组</param>
        public static void HeapSort(T[] nums)
        {
            Heapfy(nums);
            int counter = 1;
            while (counter<nums.Length)
            {
                Swap(nums, 0, nums.Length - counter);
                PercolateDown(nums, nums.Length - counter, 0);
                counter++;
            }
        }

        /// <summary>
        /// 下滤操作
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static  int PercolateDown(T[] nums,int n,int i)
        {
            int j;
            while (i != (j = ProperParent(nums,n, i)))
            {
                Swap(nums,i, j);
                i = j;
            }
            return i;
        }

        /// <summary>
        /// 上滤操作
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int PercolateUp(T[] nums,int i)
        {
            while (ParentValid(i))
            {
                int j = Parent(i);
                if (Lt(nums[i], nums[j])) break;
                Swap(nums,i, j);
                i=j;
            }
            return i;
        }

        private static void Heapfy(T[] nums)
        {
            int count = nums.Length;
            for (int i = LastInternal(count); InHeap(count, i); i--)
            {
                PercolateDown(nums,count, i);
            }
        }

        /// <summary>
        /// 父节点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>父节点的索引</returns>
        private static int Parent(int i)
        {
            return (i - 1) >> 1;
        }
        /// <summary>
        /// 左孩子节点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>左孩子节点的索引</returns>
        private static int LChild(int i)
        {
            return 1 + (i << 1);
        }
        /// <summary>
        /// 右孩子结点的索引
        /// </summary>
        /// <param name="i">当前节点的索引</param>
        /// <returns>右孩子结点的索引</returns>
        private static int RChild(int i)
        {
            return (1 + i) << 1;
        }
        /// <summary>
        /// 判断某个索引是否在堆中
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool InHeap(int n, int i)
        {
            return i > -1 && i < n;
        }
        /// <summary>
        /// 二叉堆中非叶子结点的最大索引
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int LastInternal(int n)
        {
            return Parent(n - 1);
        }
        /// <summary>
        /// 父节点是否有效
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool ParentValid(int i)
        {
            return 0 < i;
        }
        /// <summary>
        /// 左孩子是否有效
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool LChildValid(int n, int i)
        {
            return InHeap(n, LChild(i));
        }
        /// <summary>
        /// 右孩子是否有效
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static bool RChildValid(int n, int i)
        {
            return InHeap(n, RChild(i));
        }

        /// <summary>
        /// 两者中最大者的序号
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int Bigger(T[] nums,int i, int j)
        {
            return (nums[i] as IComparable<T>).CompareTo(nums[j]) >= 0
                ? i
                : j;
        }

        /// <summary>
        /// 父子（最多三个）结点中最大的值的序号（相等时父节点优先）
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int ProperParent(T[] nums,int n, int i)
        {
            if (RChildValid(n, i))
            {
                return Bigger(nums,Bigger(nums, i, LChild(i)), RChild(i));
            }
            if (LChildValid(n, i))
            {
                return Bigger(nums,i, LChild(i));
            }
            return i;
        }
        #endregion

        #region Shell排序

        public static void ShellSort(T[] nums)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 众数选取

        public static bool Majority(T[] nums, ref T maj)
        {
            maj = MajorityCandidate(nums);
            return MajorityCheck(nums,maj);
        }

        private static T MajorityCandidate(T[] nums)
        {
            T maj = nums[0];
            for (int c = 0, i = 0; i < nums.Length; i++)
            {
                if (0 == c)
                {
                    maj = nums[i];
                    c = 1;
                }
                else
                {
                    if (Eq(maj, nums[i]))
                        c++;
                    else
                    {
                        c--;
                    }
                }
            }
            return maj;
        }

        private static bool MajorityCheck(T[] nums, T maj)
        {
            int occurrence = nums.Count(t => Eq(maj, t));
            return 2*occurrence > nums.Length;
        }
        #endregion

        #region 中位数选取

        /// <summary>
        /// 子向量s1[lo1,lo1+n1)和s2[lo2,lo2+n2)分别有序，通过归并的方法获取
        /// 计算出中位数
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="lo1"></param>
        /// <param name="n1"></param>
        /// <param name="s2"></param>
        /// <param name="lo2"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        private static T TrivialMedian(T[] s1, int lo1, int n1, T[] s2, int lo2, int n2)
        {
            int h1 = lo1 + n1;
            int h2 = lo1 + n2;
            T[] s = new T[n1 + n2];
            int k = 0;
            while (lo1<h1&&lo2<h2)
            {
                if (Lt(s1[lo1],s2[lo2]))
                {
                    s[k++] = s1[lo1++];
                }
                else
                {
                    s[k++] = s2[lo2++];
                }
            }
            while (lo1 < h1) s[k++] = s1[lo1++];
            while (lo2 < h2) s[k++] = s2[lo2++];
            return s[(n1 + n2)/2];
        }

        public static T Median(T[] s1, int lo1, int n1, T[] s2, int lo2, int n2)
        {
            if(n1>n2) return Median(s2,lo2,n2,s1,lo1,n1);
            if(n2<6)
                return TrivialMedian(s1,lo1,n1,s2,lo2,n2);
            if (2*n1 < n2)           
                return Median(s1,lo1,n1,s2,lo2+(n2-n1-1)/2,n1+2-(n2-n1)%2);
            int mi1 = lo1 + n1/2;
            int mi2a = lo2 + (n1 - 1)/2;
            int mi2b = lo2 + n2 - 1 - n1/2;
            if (Gt(s1[mi1], s2[mi2b]))
                return Median(s1, lo1, n1/2 + 1, s2, mi2a, n2 - (n1 - 1)/2);
            else if (Lt(s1[mi1], s2[mi2a]))
                return Median(s1, mi1, (n1 + 1)/2, s2, lo2, n2 - n1/2);
            else
                return Median(s1, lo1, n1, s2, mi2a, n2 - (n1 - 1)/2*2);
        }
        #endregion
    }
}
