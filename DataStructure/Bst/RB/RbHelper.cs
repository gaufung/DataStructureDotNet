using System;
using Tree;

namespace Bst.RB
{
    public static class RbHelper
    {
        public static bool IsBlack<T>(this BinNode<T> p) where T : IComparable
        {
            return p == null || p.Color == RbColor.RbBlack;
        }

        public static bool IsRed<T>(this BinNode<T> p) where T : IComparable
        {
            return !p.IsBlack();
        }
        /// <summary>
        /// redblack高度更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool BlackHeightUpdated<T>(this BinNode<T> p) where T : IComparable
        {
            return p.LChild.Stature() == p.RChild.Stature() &&
            p.Height == (p.IsRed() ? p.LChild.Stature() : p.LChild.Stature() + 1);
        }
        /// <summary>
        /// 计算高度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int Stature<T>(this BinNode<T> p) where T : IComparable
        {
            return p != null ? p.Height : -1;
        }
    }
}
