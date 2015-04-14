using System;
using Tree;

namespace Bst
{
    /// <summary>
    /// 搜素二叉树接口
    /// </summary>
    interface IBBst<T> where T :IComparable
    {
        /// <summary>
        /// 按照3+4规则旋转
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t0"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <returns></returns>
        BinNode<T> Connect34(BinNode<T> a,BinNode<T> b,BinNode<T> c,BinNode<T> t0,BinNode<T> t1 ,BinNode<T> t2,BinNode<T>t3);
        
        /// <summary>
        /// 旋转调整
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        BinNode<T> RotateAt(BinNode<T> x);
    }
}
