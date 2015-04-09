using System;
using System.Collections.Generic;


namespace Tree
{
    /// <summary>
    /// 二叉树类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinTree<T> where T:IComparable<T>
    {
        #region 属性
        public int Size { get;  private set; }
        public BinNode<T> Root { get; private set; }

        public bool Empty { get { return Root == null; } } 
        #endregion

        /// <summary>
        /// 更新当前节点的高度
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual int UpdateHeight(BinNode<T> x)
        {
            return x.Height = 1 + Math.Max(Status(x.LChild), Status(x.RChild));
        }
        /// <summary>
        /// 更新当前节点和上面给结点所有的高度
        /// </summary>
        /// <param name="x"></param>
        protected void UpdateHeightAbove(BinNode<T> x)
        {
            while (x!=null)
            {
                UpdateHeight(x);
                x = x.Parent;
            }
        }
        #region 构造函数

        public BinTree()
        {
            Size = 0;
            Root = null;
        }
        #endregion

        private int Status(BinNode<T> p)
        {
            return p != null ? p.Height : -1;
        }
        public BinNode<T> InsertAsRoot(T e)
        {
            Size = 1;
            return Root = new BinNode<T>(e);
        }

        public BinNode<T> InsertAsLc(BinNode<T> x, T e)
        {
            Size++;
            x.InsertAsLc(e);
            UpdateHeightAbove(x);
            return x.LChild;
        }
        public BinNode<T> InsertAsRc(BinNode<T> x, T e)
        {
            Size++;
            x.InsertAsRc(e);
            UpdateHeightAbove(x);
            return x.RChild;
        }

        public BinNode<T> AttachAsLc(BinNode<T> x, BinTree<T> tr)
        {
            x.LChild = tr.Root;
            x.LChild.Parent = x;
            Size += tr.Size;
            UpdateHeightAbove(x);
            return x;
        }

        public BinNode<T> AttachAsRc(BinNode<T> x, BinTree<T> tr)
        {
            x.RChild = tr.Root;
            x.RChild.Parent = x;
            Size += tr.Size;
            UpdateHeightAbove(x);
            return x;
        }

        public int Remove(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeight(x.Parent);
            int n = RemoveAt(x);
            Size -= n;
            return n;
        }

        private void ClearFromParent(BinNode<T> x)
        {            
            if (x.IsRChild)
            {
                x.Parent.RChild = null;
            }
            else
            {
                x.Parent.LChild = null;
            }
        }
        private int RemoveAt(BinNode<T> x)
        {
            if (x==null)
            {
                return 0;
            }
            int n = 1 + RemoveAt(x.LChild) + RemoveAt(x.RChild);
            return n;
        }

        BinTree<T> Secede(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeightAbove(x.Parent);
            var s=new BinTree<T>();
            s.Root = x;
            x.Parent = null;
            s.Size = x.Size();
            Size -= s.Size;
            return s;
        }

        /// <summary>
        /// 层次遍历
        /// </summary>
        /// <param name="action"></param>
        public void TravLevel(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravLevel(action);
            }
        }

        #region 递归遍历

        /// <summary>
        /// 调用递归先序
        /// </summary>
        /// <param name="action"></param>
        public void TravPre(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravPre(action);
            }
        }

        /// <summary>
        /// 递归中序
        /// </summary>
        /// <param name="action"></param>
        public void TravIn(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravIn(action);
            }
        }

        /// <summary>
        /// 递归后序
        /// </summary>
        /// <param name="action"></param>
        public void TravePost(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravPost(action);
            }
        }

        #endregion


        

        #region 迭代遍历

        #region 先序遍历
        /// <summary>
        /// 先遍历所有左子树
        /// </summary>
        /// <param name="x">当前子树的根节点</param>
        /// <param name="action">操作</param>
        /// <param name="s">栈</param>
        private static void VisitAlongLeftBranch(BinNode<T> x, Action<T> action, Stack<BinNode<T>> s)
        {
            
            while (x != null)
            {
                action(x.Data);
                s.Push(x.RChild);
                x = x.LChild;
            }
        }
        /// <summary>
        /// 迭代先序遍历
        /// </summary>
        /// <param name="x">开始根节点</param>
        /// <param name="action">操作</param>
        public void TravPre_I2(BinNode<T> x, Action<T> action)
        {
            var s = new Stack<BinNode<T>>();
            while (true)
            {
                VisitAlongLeftBranch(x, action, s);
                if (s.Count == 0)
                {
                    break;
                }
                x = s.Pop();
            }
        }

        /// <summary>
        /// 迭代版先序遍历
        /// </summary>
        /// <param name="x">开始节点</param>
        /// <param name="action">遍历操作，委托</param>
        public void TravePre_I1(BinNode<T> x, Action<T> action)
        {
            var s=new Stack<BinNode<T>>();
            if (x!=null)
            {
                s.Push(x);
            }
            while (s.Count!=0)
            {
                x = s.Pop();
                action(x.Data);
                if (x.HasRChild)
                {
                    s.Push(x.RChild);
                }
                if (x.HasLChild)
                {
                    s.Push(x.LChild);
                }
            }
        }

        #endregion

        #region 中序遍历

        private static void GoAlongLeftBranch(BinNode<T> x, Stack<BinNode<T>> s)
        {
            while (x!=null)
            {
                s.Push(x);
                x = x.LChild;
            }
        }
        /// <summary>
        /// 中序遍历的迭代算法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="action"></param>
        public void TravIn_I1(BinNode<T> x,Action<T> action)
        {
            var s=new Stack<BinNode<T>>();
            while (true)
            {
                GoAlongLeftBranch(x,s);
                if (s.Count==0)
                {
                    break;
                }
                x = s.Pop();
                action(x.Data);
                x = x.RChild;
            }
        }

        public void TravIn_I2(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> s=new Stack<BinNode<T>>();
            while (true)
            {
                if(x != null)
                {
                    s.Push(x);
                    x = x.LChild;
                }
                else 
                {
                    if (s.Count != 0)
                    {
                        x = s.Pop();
                        action(x.Data);
                        x = x.RChild;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void TravIn_I3(BinNode<T> x, Action<T> aciton)
        {
            bool backTrack = false;
            while (true)
            {
                if (!backTrack && x.HasRChild)
                {
                    x = x.LChild;
                }
                else
                {
                    if (x != null)
                    {
                        aciton(x.Data);
                        if (x.HasRChild)
                        {
                            x = x.RChild;
                        }
                        else
                        {
                            if ((x=x.Succ())!=null)
                            {
                                break;
                            }
                            backTrack = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 后序遍历

        private static void GotoHLVFL(Stack<BinNode<T>> s)
        {
            BinNode<T> x;
            while ((x=s.Peek())!=null)
            {
                if (x.HasLChild)
                {
                    if (x.HasRChild)
                    {
                        s.Push(x.RChild);
                    }
                    s.Push(x.LChild);
                }
                else
                {
                    s.Push(x.RChild);
                }               
            }
            s.Pop();
            
        }

        public void TravPost_I(BinNode<T> x, Action<T> action)
        {
            Stack<BinNode<T>> s=new Stack<BinNode<T>>();
            if (x!=null)
            {
                s.Push(x);
            }
            while (s.Count!=0)
            {
                if (s.Peek()==x.Parent)
                {
                    GotoHLVFL(s);
                }
                x = s.Pop();
                action(x.Data);
            }
        }
        #endregion
        #endregion
    }
}
