using System;
using Tree;

namespace Bst.RB
{
    /// <summary>
    /// 红黑树定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedBlack<T>:Bst<T> where T:IComparable
    {
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
                    if (g.Parent == null)
                    {
                        Root = p;
                    }
                    return Connect34(x, p, g, x.LChild, x.RChild, p.RChild, g.RChild);
                }
                else
                {
                    x.Parent = g.Parent;
                    if (g.Parent == null)
                    {
                        Root = p;
                    }
                    return Connect34(p, x, g, p.LChild, x.LChild, x.RChild, g.RChild);
                }
            }
            else
            {
                if (x.IsRChild)
                {
                    p.Parent = g.Parent;
                    if (g.Parent == null)
                    {
                        Root = p;
                    }
                    return Connect34(g, p, x, g.LChild, p.LChild, x.LChild, x.RChild);
                }
                else
                {
                    x.Parent = g.Parent;
                    if (g.Parent == null)
                    {
                        Root = p;
                    }
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
        protected void SolveDoubleRed(BinNode<T> x)
        {
            if (x.IsRoot)
            {
                Root.Color=RbColor.RbBlack;
                Root.Height++;
                return;
            }
            BinNode<T> p = x.Parent;
            if (p.IsBlack())
            {
                return;
                
            }
            BinNode<T> g = p.Parent;
            BinNode<T> u = x.Uncle;
            if (u.IsBlack())
            {
                if (x.IsLChild == p.IsLChild)
                {
                    p.Color = RbColor.RbBlack;
                }
                else
                {
                    x.Color = RbColor.RbBlack;
                }
                g.Color = RbColor.RbRed;
                BinNode<T> gg = g.Parent;
                //fromparent 设置
                BinNode<T> r = RotateAt(x);
                r.Parent = gg;
            }
            else
            {
                p.Color = RbColor.RbBlack;
                p.Height++;
                u.Color=RbColor.RbBlack;
                u.Height++;
                if (!g.IsRChild)
                {
                    g.Color=RbColor.RbRed;
                }
                SolveDoubleRed(g);
            }
        }

        protected void SolveDoubleBalck(BinNode<T> r)
        {
            BinNode<T> p = r != null ? r.Parent : Hot;
            BinNode<T> s = (r == p.LChild) ? p.RChild : p.LChild;
            if (s.IsBlack())
            {
                BinNode<T> t = null;
                if (s.HasLChild && s.LChild.IsRed())
                {
                    t = s.LChild;
                }
                else if (s.HasRChild && s.RChild.IsRed())
                {
                    t = s.RChild;
                }
                if (t != null)
                {
                    RbColor oldColor = p.Color;
                    //设置FromParent函数
                    BinNode<T> b = RotateAt(t);
                    if (b.HasLChild)
                    {
                        b.LChild.Color = RbColor.RbBlack;
                    }
                    UpdateHeight(b.LChild);
                    if (b.HasRChild)
                    {
                        b.RChild.Color = RbColor.RbBlack;
                    }
                    UpdateHeight(b.RChild);
                    b.Color = oldColor;
                    UpdateHeight(b);
                }
                else
                {
                    s.Color = RbColor.RbRed;
                    s.Height--;
                    if (p.IsRed())
                    {
                        p.Color = RbColor.RbBlack;
                    }
                    else
                    {
                        p.Height--;
                        SolveDoubleBalck(p);
                    }
                }
            }
            else
            {
                s.Color=RbColor.RbBlack;
                p.Color=RbColor.RbRed;
                BinNode<T> t = s.IsLChild ? s.LChild : s.RChild;
                Hot = p;
                //设置FromParent
                SolveDoubleBalck(r);
            }
        }
        protected override int UpdateHeight(BinNode<T> x)
        {
            x.Height = Math.Max(x.LChild.Stature(), x.RChild.Stature());
            return x.IsBlack() ? x.Height++ : x.Height;
        }
        public override BinNode<T> Insert(T e)
        {
            BinNode<T> x = Search(e);
            if (x!=null)
            {
                return x;
            }
            //这一部分需要重新考虑一下
            x=new BinNode<T>(e,Hot,null,null,-1);
            Size++;
            SolveDoubleRed(x);
            return x;
        }
        public override bool Remove(T e)
        {
           //throw new NotImplementedException();
            BinNode<T> x = Search(e);
            if (x==null)
            {
                return false;
            }
            BinNode<T> r = RemoveAt(x);
            if (0>=--Size)
            {
                return true;
            }
            if (Hot==null)
            {
                Root.Color=RbColor.RbBlack;
                UpdateHeight(Root);
                return true;
            }
            if (Hot.BlackHeightUpdated())
            {
                return true;
            }
            if (r.IsRed())
            {
                r.Color = RbColor.RbBlack;
                r.Height++;
                return true;
            }
            SolveDoubleBalck(r);
            return true;
        }
    }
}
