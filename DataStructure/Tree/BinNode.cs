using System;

namespace Sequence
{
    /// <summary>
    /// 二叉树的节点类
    /// </summary>
    /// <typeparam name="T">The type of data</typeparam>
    public class BinNode<T> :IComparable<BinNode<T>> where T : IComparable<T>
    {
        #region 属性
        /// <summary>
        /// the binnode data
        /// </summary>
        public T Data { get;  set; }

        /// <summary>
        /// parent
        /// </summary>
        public BinNode<T> Parent { get;  set; }
        /// <summary>
        /// left child
        /// </summary>
        public BinNode<T> LChild { get;  set; }
        /// <summary>
        /// right child
        /// </summary>
        public BinNode<T> RChild { get;  set; }

        /// <summary>
        /// the node height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// null path length
        /// </summary>
        public int Npl { get; set; }

        /// <summary>
        /// the node color(red-black tree)
        /// </summary>
        public RbColor Color { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// the default constructor
        /// </summary>
        public BinNode():this(default(T))
        {           
        }
        /// <summary>
        /// the constructor
        /// </summary>
        /// <param name="e">data</param>
        /// <param name="parent">parent</param>
        /// <param name="lChild">left child</param>
        /// <param name="rChild">right child</param>
        /// <param name="height">height</param>
        /// <param name="npl">null path length</param>
        /// <param name="color">the color</param>
        public BinNode(T e, BinNode<T> parent = null, BinNode<T> lChild = null,
            BinNode<T> rChild = null, int height = 0, int npl = 1, RbColor color = RbColor.RbRed)
        {
            Data = e;
            Parent = parent;
            LChild = lChild;
            RChild = rChild;
            Height = height;
            Npl = npl;
            Color = color;
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 获取以当前节点为根节点的子树所有节点的个数
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return Size(this);
        }

        /// <summary>
        /// 递归调用求取Size
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int Size(BinNode<T> x)
        {
            if (x == null) return 0;
            return 1 + Size(x.LChild) + Size(x.RChild);
        }

        /// <summary>
        /// 插入左孩子
        /// </summary>
        /// <param name="e">节点的值</param>
        /// <returns>返回插入的节点</returns>
        public BinNode<T> InsertAsLc(T e)
        {
         
           return LChild=new BinNode<T>(e,this);
        }

        /// <summary>
        /// 插入右孩子
        /// </summary>
        /// <param name="e">节点的值</param>
        /// <returns>返回插入的节点</returns>
        public BinNode<T> InsertAsRc(T e)
        {
           
            return RChild = new BinNode<T>(e, this);
        }

        /// <summary>
        /// 获取中序遍历中的下一个节点
        /// </summary>
        /// <returns>the successor node </returns>
        public BinNode<T> Succ()
        {
            BinNode<T> s = this;
            if (HasRChild)
            {
                s = RChild;
                while (s.HasLChild)
                {
                    s = s.LChild;
                }
            }
            else
            {
                while (s.IsRChild)
                {
                    s = s.Parent;
                }
                s = s.Parent;
            }
            return s;
        }

        /// <summary>
        /// 层次遍历
        /// </summary>
        /// <param name="action">the action</param>
        public void TravLevel(Action<T> action)
        {
            Queue<BinNode<T>> q = QueueListImpl<BinNode<T>>.QueueFacotry();
            q.Enqueue(this);
            while (q.Size!=0)
            {
                var x = q.Dequeue();
                action(x.Data);
                if (x.HasLChild)
                {
                    q.Enqueue(x.LChild);
                }
                if (x.HasRChild)
                {
                    q.Enqueue(x.RChild);
                }
            }
        }

        /// <summary>
        /// 递归先序遍历
        /// </summary>
        /// <param name="action"></param>
        public void TravPre(Action<BinNode<T>> action)
        {
           TravPre(this,action);
        }
        /// <summary>
        /// 递归先序遍历  
        /// </summary>
        /// <param name="x">the node </param>
        /// <param name="trave">the action</param>
        private static void TravPre(BinNode<T> x, Action<BinNode<T>> trave)
        {
            trave(x);
            if (x.LChild != null) TravPre(x.LChild, trave);          
            if (x.RChild != null) TravPre(x.RChild, trave);
        }

        #region 中序递归遍历
        /// <summary>
        /// 中序递归遍历
        /// </summary>
        /// <param name="action"></param>
        public void TravIn(Action<BinNode<T>> action)
        {
            TravIn(this, action);
        }

        private static void TravIn(BinNode<T> x, Action<BinNode<T>> trave)
        {
            if (x.LChild != null) TravIn(x.LChild, trave);
            trave(x);
            if (x.RChild != null) TravIn(x.RChild, trave);
        }

        #endregion

        #region 后序遍历
        /// <summary>
        /// 后续递归遍历
        /// </summary>
        /// <param name="action">the action</param>
        public void TravPost(Action<BinNode<T>> action)
        {
            TravPost(this, action);
        }

        private static void TravPost(BinNode<T> x, Action<BinNode<T>> trave)
        {
            if (x.LChild != null)
            {
                TravPost(x.LChild, trave);
            }
            if (x.RChild != null)
            {
                TravPost(x.RChild, trave);
            }
            trave(x);
        }

        #endregion

        public static bool Lt(T a, T b)
        {
            return a.CompareTo(b) == -1;
        }

        public static bool Eq(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool Gt(T a, T b)
        {
            return a.CompareTo(b) == 1;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 是否为根节点
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return Parent == null;
            }
        }
        /// <summary>
        /// 是否为左孩子结点
        /// </summary>
        public bool IsLChild
        {
            get
            {
                return !IsRoot && this == Parent.LChild;
            }
        }
        /// <summary>
        /// 是否为右孩子结点
        /// </summary>
        public bool IsRChild
        {
            get
            {
                return !IsRoot && this == Parent.RChild;
            }
        }
        /// <summary>
        /// 是否有父节点
        /// </summary>
        public bool HasParent
        {
            get
            {
                return !IsRoot;
            }
        }
        /// <summary>
        /// 是否有左孩子
        /// </summary>
        public bool HasLChild
        {
            get
            {
                return LChild != null;
            }
        }
        /// <summary>
        /// 是否有右孩子
        /// </summary>
        public bool HasRChild
        {
            get
            {
                return RChild != null;
            }
        }
        /// <summary>
        /// 是否有孩子结点
        /// </summary>
        public bool HasChild
        {
            get
            {
                return HasLChild || HasRChild;
            }
        }
        /// <summary>
        /// 是否有两个孩子结点
        /// </summary>
        public bool HasBothChild
        {
            get
            {
                return HasLChild && HasRChild;
            }
        }
        /// <summary>
        /// 是否为叶子结点
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return !HasChild;
            }
        }
        /// <summary>
        /// 获取兄弟节点
        /// </summary>
        public BinNode<T> Sibling
        {
            get
            {
                return IsLChild ? Parent.RChild : Parent.LChild;
            }
        }
        /// <summary>
        /// 获取叔叔结点
        /// </summary>
        public BinNode<T> Uncle
        {
            get
            {
                return Parent.IsLChild ? 
                    Parent.Parent.RChild : Parent.Parent.LChild;
            }
        }
        /// <summary>
        /// 来自父节点的指针
        /// </summary>
        public BinNode<T> FromParentTo
        {
            get { return IsRoot ? this : (IsLChild ? Parent.LChild : Parent.RChild); }
        } 

        #endregion


        public int CompareTo(BinNode<T> other)
        {
            return Data.CompareTo(other.Data);
        }
    }
}
