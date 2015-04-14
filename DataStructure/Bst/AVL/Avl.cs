using System;
using Tree;


namespace Bst.AVL
{
    /// <summary>
    /// AVL 树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Avl<T>:Bst<T> where T: IComparable
    {
        public override BinNode<T> Insert(T e)
        {
            BinNode<T> x = Search(e);
            if (x != null) return x;
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Size++;
                UpdateHeightAbove(Root);
            }
            else
            {
                if (Lt(e, Hot.Data))                              
                    x = Hot.InsertAsLc(e);                
                else                
                    x = Hot.InsertAsRc(e);                
                Size++;
                UpdateHeightAbove(x);
            }
            //一直到根节点
            for (BinNode<T> g=Hot;g!=null;g=g.Parent)
            {
                if (!g.AvlBalanced())
                {
                    var p = RotateAt(TallerChild(TallerChild(g)));
                    if (g.IsRoot)
                    {
                        Root = p;
                    }
                    else
                    {
                        g.FromParentTo(p);
                    }
                    break;
                }
                else
                {
                    UpdateHeight(g);
                }
            }
            return x;
        }

        public override bool Remove(T e)
        {
            BinNode<T> x = Search(e);
            if (x == null) return false;
            RemoveAt(x);
            Size--;
            for (BinNode<T> g=Hot;g!=null;g=g.Parent)
            {
                if(!g.AvlBalanced())
                {
                    var p = RotateAt(TallerChild(TallerChild(g)));
                    if (g.IsRoot)
                    {
                       g= Root = p;
                    }
                    else
                    {
                       g.FromParentTo(p);
                        g = p;
                    }
                    
                }
                UpdateHeight(g);
            }
            return true;
        }

        /// <summary>
        /// 旋转调整
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private BinNode<T> RotateAt(BinNode<T> x)
        {
            var p = x.Parent;
            var g = p.Parent;
            if (p.IsLChild)
            {
                if (x.IsLChild)
                {
                    p.Parent = g.Parent;
                    return Connect34(x, p, g, x.LChild, x.RChild, p.RChild, g.RChild);
                }
                else
                {
                    x.Parent = g.Parent;
                    return Connect34(p, x, g, p.LChild, x.LChild, x.RChild, g.RChild);
                }
            }
            else
            {
                if (x.IsRChild)
                {
                    p.Parent = g.Parent;
                    return Connect34(g, p, x, g.LChild, p.LChild, x.LChild, x.RChild);
                }
                else
                {
                    x.Parent = g.Parent;
                    return Connect34(g, x, p, g.LChild, x.LChild, x.RChild, p.LChild);
                }
            }

        }

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
        private BinNode<T> Connect34(BinNode<T> a, BinNode<T> b, BinNode<T> c, 
            BinNode<T> t0, BinNode<T> t1, BinNode<T> t2,BinNode<T> t3)
        {
            a.LChild = t0;
            if (t0!=null)
            {
                t0.Parent=a;
            }
            a.RChild = t1;
            if (t1!=null)
            {
                t1.Parent = a;
                UpdateHeight(a);
            }
            c.LChild = t2;
            if (t2!=null)
            {
                t2.Parent = c;
            }
            c.RChild = t3;
            if (t3!=null)
            {
                t3.Parent = c;
                UpdateHeight(c);
            }
            b.LChild = a;
            a.Parent=b;
            b.RChild = c;
            c.Parent = b;
            UpdateHeight(b);
            return b;
        }
        #region 辅助静态方法

        //找到高度更高的子树
        private static BinNode<T> TallerChild(BinNode<T> x)
        {
            return x.LChild.Stature() > x.RChild.Stature()
                ? x.LChild
                : (x.RChild.Stature() > x.LChild.Stature() ?
                x.RChild : x.IsLChild ? x.LChild : x.RChild);
        }

        #region 比较操作
        private static bool Eq(T a, T b)
        {
            return BinNode<T>.Eq(a, b);
        }

        private static bool Lt(T a, T b)
        {
            return BinNode<T>.Lt(a, b);
        }
        #endregion

        #endregion
    }
}
