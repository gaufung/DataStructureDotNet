using System;

namespace Sequence.RB
{
    public class RBTree<T>:Bst<T> where T:IComparable<T>
    {

        #region 辅助函数

        private bool IsBlack(BinNode<T> node)
        {
            return node == null || node.Color == RbColor.RbBlack;
        }

        private bool IsRed(BinNode<T> node)
        {
            return !IsBlack(node);
        }

        protected override int UpdateHeight(BinNode<T> node)
        {
            node.Height = Math.Max(Height(node.LChild), Height(node.RChild));
            return IsBlack(node) ? ++node.Height : node.Height;
        }

        private int Height(BinNode<T> node)
        {
            if (node == null)
                return 1;
            else
            {
                return node.Height;
            }
        }
        #endregion

        public override BinNode<T> Insert(T e)
        {
            BinNode<T> x = Search(e);
            if (x != null) return x;
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Size++;
                UpdateHeight(Root);
                Root.Color = RbColor.RbBlack;
                return x;
            }
            x = Lt(e, Hot.Data) ? Hot.InsertAsLc(e) : Hot.InsertAsRc(e);
            Size++;
            UpdateHeightAbove(x);
            x.Color = RbColor.RbRed;
            SolveDoubleRed(x);
            return x;
        }

        public override bool Remove(T e)
        {
            BinNode<T> x = Search(e);
            if (x == null) return false;
            RemoveAt(x);
            Size--;
            throw new NotImplementedException("delete is more complicated,see you later");
        }


        #region 红黑树修复

        private void SolveDoubleRed(BinNode<T> x)
        {
            if (IsBlack(x)) return ;
            //x is root
            if (x.Parent == null)
            {
                x.Color=RbColor.RbBlack;
                x.Height++;
                return;
            }
            BinNode<T> p = x.Parent;
            if (IsBlack(p) || p.Parent == null) return;
            //p Color is red 
            BinNode<T> g = p.Parent;
            BinNode<T> u = p.IsLChild ? g.RChild : g.LChild;
            if (IsBlack(u))
            {
                //如果p和x同为左孩子或者同为右孩子
                if (p.IsLChild && x.IsLChild)
                {
                    p.Color=RbColor.RbBlack;
                    g.Color=RbColor.RbRed;
                    // if g is root
                    if (g.Parent == null)
                    {
                        Connect34(x, p, g, x.LChild, x.RChild, p.RChild, g.RChild);
                        Root = p;
                        Root.Parent = null;
                    }
                    else
                    {
                        BinNode<T> gg = g.Parent;
                        bool isgLeft = g.IsLChild;
                        Connect34(x, p, g, x.LChild, x.RChild, p.RChild, g.RChild);
                        p.Parent = gg;
                        if (isgLeft)
                        {
                            gg.LChild = p;
                        }
                        else
                        {
                            gg.RChild = p;
                        }
                    }
                }
                else if (p.IsRChild && x.IsRChild)
                {
                    p.Color = RbColor.RbBlack;
                    g.Color = RbColor.RbRed;
                    // if g is root
                    if (g.Parent == null)
                    {
                        Connect34(g,p,x,g.LChild,p.LChild,x.LChild,x.RChild);
                        Root = p;
                        Root.Parent = null;
                    }
                    else
                    {
                        BinNode<T> gg = g.Parent;
                        bool isgLeft = g.IsLChild;
                        Connect34(g, p, x, g.LChild, p.LChild, x.LChild, x.RChild);
                        p.Parent = gg;
                        if (isgLeft)
                        {
                            gg.LChild = p;
                        }
                        else
                        {
                            gg.RChild = p;
                        }
                    }
                }
                else if (x.IsRChild && p.IsLChild)
                {
                    x.Color = RbColor.RbBlack;
                    p.Color = RbColor.RbRed;
                    g.Color = RbColor.RbRed;
                    if (g.Parent == null)
                    {
                        Connect34(p, x, g, p.LChild, x.LChild, x.RChild, g.RChild);
                        x.Parent = null;
                        Root = x;
                    }
                    else
                    {
                        BinNode<T> gg = g.Parent;
                        bool isLeftChild = g.IsLChild;
                        Connect34(p, x, g, p.LChild, x.LChild, x.RChild, g.RChild);
                        x.Parent = gg;
                        if (isLeftChild)
                        {
                            gg.LChild = x;
                        }
                        else
                        {
                            gg.RChild = x;
                        }
                    }
                }
                else
                {
                    x.Color = RbColor.RbBlack;
                    p.Color = RbColor.RbRed;
                    g.Color = RbColor.RbRed;
                    if (g.Parent == null)
                    {
                        Connect34(g, x, p, g.LChild, x.LChild, x.RChild, g.RChild);
                        x.Parent = null;
                        Root = x;
                    }
                    else
                    {
                        BinNode<T> gg = p.Parent;
                        bool isLefChild = gg.IsLChild;
                        Connect34(g, x, p, g.LChild, x.LChild, x.RChild, g.RChild);
                        x.Parent = gg;
                        if (isLefChild)
                        {
                            gg.LChild = x;
                        }
                        else
                        {
                            gg.RChild = x;
                        }
                    }
                }
            }
            else
            {
                p.Color=RbColor.RbBlack;
                p.Height++;
                u.Color=RbColor.RbBlack;
                u.Height++;
                // g is root
                if (g.Parent == null)
                {
                    g.Height++;
                }
                else
                {
                    g.Color=RbColor.RbRed;
                    SolveDoubleRed(g);
                }
            }
        }
        #endregion
    }
}
