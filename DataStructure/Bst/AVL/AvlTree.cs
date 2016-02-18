using System;

namespace Sequence.AVL
{
    public class AvlTree<T>:Bst<T> where T :IComparable<T>
    {
        public override BinNode<T> Insert(T e)
        {
            BinNode<T> x = Search(e);
            if (x != null) return x;
            //空树，第一次插入节点
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Size++;
                UpdateHeight(Root);
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
            for (BinNode<T> g = Hot; g != null; g = g.Parent)
            {
                if (!g.AvlBalanced())
                {
                    bool isLchild = g.IsLChild;
                    bool isRoot = g == Root;
                    var y = RotateAt(TallerChild(TallerChild(g)));
                    if (!isRoot)
                    {
                        if (isLchild)
                        {
                            y.Parent.LChild = y;
                        }
                        else
                        {
                            y.Parent.RChild = y;
                        }
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
            for (BinNode<T> g = Hot; g != null; g = g.Parent)
            {
               
                if (!g.AvlBalanced())
                {
                    bool isLeft = g.IsLChild;
                    bool isRoot = g.IsRoot;
                    var parent = g.Parent;
                    var y = RotateAt(TallerChild(TallerChild(g)));
                    if (isRoot)
                    {
                        Root = y;
                    }
                    else
                    {
                        if (isLeft)
                        {
                            g.Parent.LChild = y;
                        }
                        else
                        {
                            g.Parent.RChild = y;
                        }
                    }
                    g.Parent = y;
                    y.Parent = parent;
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
                //zig-zig
                if (x.IsLChild)
                {
                    p.Parent = g.Parent;//交换父节点
                    if (g.Parent == null)
                    {
                        Root = p;
                    }
                    return Connect34(x, p, g, x.LChild, x.RChild, p.RChild, g.RChild);
                }
                //zig-zag
                else
                {
                    x.Parent = g.Parent;//交换父节点
                    if (g.Parent == null)
                    {
                        Root = x;
                    }
                    return Connect34(p, x, g, p.LChild, x.LChild, x.RChild, g.RChild);
                }
            }
            else
            {
                //zag-zag
                if (x.IsRChild)
                {
                    p.Parent = g.Parent;//交换父节点
                    if (g.Parent == null)
                    {
                        Root =p;
                    }
                    return Connect34(g, p, x, g.LChild, p.LChild, x.LChild, x.RChild);
                }
                //zag-zig
                else
                {
                    x.Parent = g.Parent;//交换父节点
                    if (g.Parent == null)
                    {
                        Root = x;
                    }
                    return Connect34(g, x, p, g.LChild, x.LChild, x.RChild, p.LChild);
                }
            }

        }

        /// <summary>
        /// 按照3+4规则旋转 按照中序遍历的规则使之能够达到
        /// t0->a->t1->b->t2->c->t3
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="c">c</param>
        /// <param name="t0">t0</param>
        /// <param name="t1">t1</param>
        /// <param name="t2">t2</param>
        /// <param name="t3">t3</param>
        /// <returns>返回局部子树的根节点</returns>
        private BinNode<T> Connect34(BinNode<T> a, BinNode<T> b, BinNode<T> c,
            BinNode<T> t0, BinNode<T> t1, BinNode<T> t2, BinNode<T> t3)
        {
            a.LChild = t0;
            if (t0 != null)
            {
                t0.Parent = a;
            }
            a.RChild = t1;
            if (t1 != null)
            {
                t1.Parent = a;

            }
            UpdateHeight(a);
            c.LChild = t2;
            if (t2 != null)
            {
                t2.Parent = c;
            }
            c.RChild = t3;
            if (t3 != null)
            {
                t3.Parent = c;
            }
            UpdateHeight(c);
            b.LChild = a;
            a.Parent = b;
            b.RChild = c;
            c.Parent = b;
            UpdateHeight(b);
            return b;
        }

        //找到高度更高的子树
        private static BinNode<T> TallerChild(BinNode<T> x)
        {
            return x.LChild.Stature() > x.RChild.Stature()
                ? x.LChild
                : (x.RChild.Stature() > x.LChild.Stature() ?
                x.RChild : x.IsLChild ? x.LChild : x.RChild);
        }
    }
}
