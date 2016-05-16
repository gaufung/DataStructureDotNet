using System;

namespace Sequence
{
    /// <summary>
    /// 二叉树类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinTree<T> where T:IComparable<T>
    {
        #region 属性

        /// <summary>
        /// 树的大小
        /// </summary>
        public int Size { get;  protected set; }
        /// <summary>
        /// the root of the tree
        /// </summary>
        public BinNode<T> Root { get;  set; }
        /// <summary>
        /// if the tree is empty or not
        /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        public BinTree()
        {
            Size = 0;
            Root = null;
        }
        #endregion

        /// <summary>
        /// the status the tree node
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int Status(BinNode<T> p)
        {
            return p != null ? p.Height : -1;
        }
        /// <summary>
        /// 新的结点作为根节点插入
        /// </summary>
        /// <param name="e">结点的值</param>
        /// <returns>新的根节点</returns>
        public BinNode<T> InsertAsRoot(T e)
        {
            Size = 1;
            return Root = new BinNode<T>(e);
        }
        /// <summary>
        /// 作为某个结点的左孩子结点插入
        /// </summary>
        /// <param name="x">要插入的父节点</param>
        /// <param name="e">结点的值</param>
        /// <returns>插入的结点的值</returns>
        public BinNode<T> InsertAsLc(BinNode<T> x, T e)
        {
            Size++;
            var leftChild=x.InsertAsLc(e);
            UpdateHeightAbove(x);
            return leftChild;
        }
        /// <summary>
        /// 作为右孩子结点插入
        /// </summary>
        /// <param name="x">结点</param>
        /// <param name="e">要插入的结点的值</param>
        /// <returns>插入的结点</returns>
        public BinNode<T> InsertAsRc(BinNode<T> x, T e)
        {
            Size++;
            var rightChild=x.InsertAsRc(e);
            UpdateHeightAbove(x);
            return rightChild;
        }
        /// <summary>
        /// 将一棵树作为结点x的Left Child插入
        /// </summary>
        /// <param name="x">要插入的结点</param>
        /// <param name="tr">要插入的树</param>
        /// <returns>返回结点</returns>
        public BinNode<T> AttachAsLc(BinNode<T> x, BinTree<T> tr)
        {
            int xSize=RemoveAt(x.LChild);
            x.LChild = tr.Root; 
            x.LChild.Parent = x;
            Size =Size-xSize+tr.Size;
            var backup = x;
            UpdateHeightAbove(x);
            return backup;
        }
        /// <summary>
        /// 将一棵树作为结点x的Right Child插入
        /// </summary>
        /// <param name="x">要插入的结点</param>
        /// <param name="tr">要插入的树</param>
        /// <returns>返回结点</returns>
        public BinNode<T> AttachAsRc(BinNode<T> x, BinTree<T> tr)
        {
            int xSize = RemoveAt(x.RChild);
            x.RChild = tr.Root;
            x.RChild.Parent = x;
            Size =Size-xSize+tr.Size;
            var backup = x;
            UpdateHeightAbove(x);
            return backup;
        }
        /// <summary>
        /// 将结点X从树中摘除
        /// </summary>
        /// <param name="x">结点X</param>
        /// <returns>摘除结点为根节点的子树的结点数目</returns>
        public int Remove(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeight(x.Parent);
            int n = RemoveAt(x);
            Size -= n;
            return n;
        }
        /// <summary>
        /// 从父节点的关系链中摘除
        /// </summary>
        /// <param name="x"></param>
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
        /// <summary>
        /// 从结点X为子树的所有结点的数目
        /// </summary>
        /// <param name="x">作为根节点的子树</param>
        /// <returns>结点的数目</returns>
        private int RemoveAt(BinNode<T> x)
        {
            if (x==null)
            {
                return 0;
            }
            int n = 1 + RemoveAt(x.LChild) + RemoveAt(x.RChild);
            return n;
        }

        /// <summary>
        /// 从某个结点摘除出，并且以此为根节点创建一个树
        /// </summary>
        /// <param name="x">摘除的结点</param>
        /// <returns>摘除出来的树</returns>
        public BinTree<T> Secede(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeightAbove(x.Parent);
            var s = new BinTree<T> {Root = x};
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
        private static void VisitAlongLeftBranch(BinNode<T> x, 
            Action<T> action, Stack<BinNode<T>> s)
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
            Stack<BinNode<T>> s = StackVectorImpl<BinNode<T>>.StackFactory();
            while (true)
            {
                VisitAlongLeftBranch(x, action, s);
                if (s.Size == 0) break;
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
            var s = StackVectorImpl<BinNode<T>>.StackFactory();
            if (x!=null)
            {
                s.Push(x);
            }
            while (s.Size!=0)
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
            var s = StackVectorImpl<BinNode<T>>.StackFactory();
            while (true)
            {
                GoAlongLeftBranch(x,s);
                if (s.Size==0)
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
            Stack<BinNode<T>> s = StackVectorImpl<BinNode<T>>.StackFactory();
            while (true)
            {
                if(x != null)
                {
                    s.Push(x);
                    x = x.LChild;
                }
                else 
                {
                    if (s.Size != 0)
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
            while ((x=s.Top)!=null)
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
            Stack<BinNode<T>> s = StackVectorImpl<BinNode<T>>.StackFactory();
            if (x!=null)
            {
                s.Push(x);
            }
            while (s.Size!=0)
            {
                if (s.Top==x.Parent)
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
