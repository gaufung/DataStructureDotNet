using System;
using Tree;
namespace Bst.AVL
{
    public static class AvlHelper
    {
        /// <summary>
        /// 计算高度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int Stature<T>(this BinNode<T> p) where T :IComparable
        {
            return p != null ? p.Height : -1;
        }
        /// <summary>
        /// 判断是否达到平衡状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Balanced<T>(this BinNode<T> p) where T : IComparable
        {
            return p.RChild.Stature() == p.LChild.Stature();
        }

        /// <summary>
        /// 计算平衡因子
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int BalFac<T>(this BinNode<T> p) where T : IComparable
        {
            return p.LChild.Stature() - p.RChild.Stature();
        }

        /// <summary>
        /// 是否达到平衡
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool AvlBalanced<T>(this BinNode<T> p) where T : IComparable
        {
            return p.BalFac() > -2 && p.BalFac() < 2;
        }

        public static void FromParentTo<T>(this BinNode<T> x, BinNode<T> p) where T : IComparable
        {
            if (!x.IsRoot)
            {
                if (x.IsLChild)
                {
                    x.Parent.LChild = p;
                }
                else
                {
                    x.Parent.RChild = p;
                }

            }
        }
        
    }
}
