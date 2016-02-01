using System;
using Tree;

namespace Bst.SplayTree
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class SpalyHelper
    {
        public static void AttachAsLChild<T>(this BinNode<T> p, BinNode<T> lc) where T : IComparable
        {
            p.LChild = lc;
            if (lc!=null)
            {
                lc.Parent = p;
            }
        }

        public static void AttachAsRChild<T>(this BinNode<T> p, BinNode<T> rc) where T : IComparable
        {
            p.RChild = rc;
            if (rc!=null)
            {
                rc.Parent = p;
            }
        }
    }
}
