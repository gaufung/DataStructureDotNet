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
            if (x.IsRoot)//如果x为根节点
            {
                Root.Color=RbColor.RbBlack;//蚺蛇
                Root.Height++;//根节点的高度+1；
                return;
            }
            //如果父亲节点为黑色，返回，停止更新
            BinNode<T> p = x.Parent;
            if (p.IsBlack()) return;  
            //获取祖父节点和叔叔节点
            BinNode<T> g = p.Parent;
            BinNode<T> u = x.Uncle;
            //为黑
            Boolean isGrandLChild = g.IsLChild;
            if (u.IsBlack())
            {
                if (x.IsLChild == p.IsLChild)//zig-zig or zag-zag
                {
                    p.Color = RbColor.RbBlack;//coloring
                }
                else//zig-zag or zag-zig
                {
                    x.Color = RbColor.RbBlack;
                }
                g.Color = RbColor.RbRed;//colring
                BinNode<T> gg = g.Parent;//grand-grandparent
                //fromparent 设置               
                BinNode<T> r = RotateAt(x);
                if (gg!=null)
                {
                    if (isGrandLChild)
                    {
                        gg.LChild = r;
                    }
                    else
                    {
                        gg.RChild = r;
                    }   
                }              
                r.Parent = gg;
            }
            else
            {
                p.Color = RbColor.RbBlack;
                p.Height++;
                u.Color=RbColor.RbBlack;
                u.Height++;
                if (!g.IsRoot)
                {
                    g.Color=RbColor.RbRed;
                }
                SolveDoubleRed(g);
            }
        }

        protected void SolveDoubleBalck(BinNode<T> r)
        {
            BinNode<T> p = r != null ? r.Parent : Hot;
            if (p == null) return;
            BinNode<T> s = (r==p.LChild)?p.RChild:p.LChild;//兄弟节点必然存在
            Boolean isParentLChild = p.IsLChild;
            //bb-1
            if (s.IsBlack())//if sibling node's color is black
            {
                BinNode<T> t = null;
                if (s.HasLChild && s.LChild.IsRed()) t = s.LChild;
                else if (s.HasRChild && s.RChild.IsRed()) t = s.RChild;
                if (t!= null)//如果有红孩子
                {
                    RbColor oldColor = p.Color;
                    //设置FromParent函数
                    BinNode<T> b = RotateAt(t);
                    if (p.Parent!=null)
                    {
                        if (isParentLChild)
                        {
                            p.LChild = b;
                        }
                        else
                        {
                            p.RChild = b;
                        }
                    }
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
                //
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
                BinNode<T> b = RotateAt(r);
                if(p.Parent!=null)
                {
                    if (isParentLChild)
                    {
                        p.Parent.LChild = b;
                    }
                    else
                    {
                        p.Parent.RChild = b;
                    }
                }
                SolveDoubleBalck(r);
            }
        }
        protected override int UpdateHeight(BinNode<T> x)
        {
            x.Height = Math.Max(x.LChild.Stature(), x.RChild.Stature());
            return x.IsBlack() ? x.Height++ : x.Height;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override BinNode<T> Insert(T e)
        {
            var x = Search(e);
            if (x!=null)
            {
                return x;
            }
            //如果插入的是根节点
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Root.Height = 0;
                Root.Color = RbColor.RbBlack;
                return Root;
            }
            //如果该位置的父亲节点大于要插入的节点，插入为父亲节点的左孩子，否则为右孩子
            x = Hot.Data.Gt(e) ? Hot.InsertAsLc(e) : Hot.InsertAsRc(e);       
            x.Height = -1;
            Size++;
            //解决双红问题问题
            SolveDoubleRed(x);
            return x;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool Remove(T e)
        {
           //throw new NotImplementedException();
            BinNode<T> x = Search(e);           
            if (x == null) return false;//the node does not exist
            Boolean isDeleteRed = x.IsRed();
            BinNode<T> r = RemoveAt(x);
            if (0 >= --Size) return true;//if the tree is empty
            if (Hot==null)//if delete the Root
            {
                Root.Color=RbColor.RbBlack;//coloring 
                UpdateHeight(Root);//update the black height
                return true;
            }
            if (isDeleteRed) return true;//if deleted node's color is red : return             
            //if (Hot.BlackHeightUpdated())//to confirm the height of the node updatded
            //{
            //    return true;
            //}
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
