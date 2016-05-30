using System;

namespace Sequence.SplayTree
{
    /// <summary>
    /// splay 树辅助帮助类
    /// </summary>
    [Serializable]
    internal static class SplayHelper
    {
        /// <summary>
        /// 将某棵子树作为结点的左孩子
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="p">结点</param>
        /// <param name="lc">要插入的子树</param>
        public static void AttachAsLChild<T>(this BinNode<T> p, BinNode<T> lc) where T : IComparable<T>
        {
            p.LChild = lc;
            if (lc != null)
            {
                lc.Parent = p;
            }
        }

        /// <summary>
        /// 将某棵子树作为结点的左孩子
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="p">结点</param>
        /// <param name="rc">要插入的子树</param>
        public static void AttachAsRChild<T>(this BinNode<T> p, BinNode<T> rc) where T : IComparable<T>
        {
            p.RChild = rc;
            if (rc != null)
            {
                rc.Parent = p;
            }
        }
    }
}
