using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Tree
{

    public class BinNode<T> where T : IComparable<T>
    {
        #region 属性

        public T Data { get;  set; }
        public BinNode<T> Parent { get;  set; }
        public BinNode<T> LChild { get;  set; }
        public BinNode<T> RChild { get;  set; }
        public int Height { get; set; }
        public int Npl { get; set; }
        public RbColor Color { get; set; }

        #endregion

        #region 构造函数

        public BinNode()
        {
            Data = default(T);
            Parent = null;
            LChild = null;
            RChild = null;
            Height = 0;
            Npl = 1;
            Color = RbColor.RbRed;
        }

        public BinNode(T e, BinNode<T> parent = null, BinNode<T> lChild = null,
            BinNode<T> rChild = null, int height = 0, int npl = 1, RbColor color = RbColor.RbBlack)
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
        public int Size()
        {
            return Size(this);
        }

        private int Size(BinNode<T> x)
        {
            if (x==null)
            {
                return 0;
            }
            return 1 + Size(x.LChild) + Size(x.RChild);
        }

        public BinNode<T> InsertAsLc(T e)
        {
           return LChild=new BinNode<T>(e,this);
        }

        public BinNode<T> InsertAsRc(T e)
        {
            return RChild = new BinNode<T>(e, this);
        }

        /// <summary>
        /// 获取中序遍历中的下一个节点
        /// </summary>
        /// <returns></returns>
        public BinNode<T> Succ()
        {
            BinNode<T> s = this;
            if (HasRChild)
            {
                s = this.RChild;
                while (s.HasRChild)
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
        /// <param name="action"></param>
        public void TravLevel(Action<T> action)
        {
            Queue<BinNode<T>> q=new Queue<BinNode<T>>();
            q.Enqueue(this);
            while (q.Count!=0)
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

        public void TravPre(Action<T> action)
        {
            throw new NotImplementedException();
            //action(Data);
            //if (LChild == null) return;
            //LChild.TravPre(action);
            //if (RChild == null) return;
            //RChild.TravPre(action);
            
        }

        public void TravIn(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void TravPost(Action<T> action)
        {
            throw new NotImplementedException();
            //if (LChild == null) return;
            //{
                
            //}
        }

        public static bool Lt(T a, T b)
        {
            return (a as IComparable<T>).CompareTo(b) == -1;
        }

        public static bool Eq(T a, T b)
        {
            return (a as IComparable<T>).CompareTo(b) == 0;
        }

       
        #endregion

        #region 辅助方法
        public bool IsRoot
        {
            get
            {
                return this.Parent == null;
            }
        }

        public bool IsLChild
        {
            get
            {
                return !IsRoot && this == this.Parent.LChild;
            }
        }

        public bool IsRChild
        {
            get
            {
                return !IsRoot && this == this.Parent.RChild;
            }
        }

        public bool HasParent
        {
            get
            {
                return !IsRoot;
            }
        }

        public bool HasLChild
        {
            get
            {
                return LChild != null;
            }
        }

        public bool HasRChild
        {
            get
            {
                return RChild != null;
            }
        }

        public bool HasChild
        {
            get
            {
                return HasLChild || HasRChild;
            }
        }

        public bool HasBothChild
        {
            get
            {
                return HasLChild && HasRChild;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return !HasChild;
            }
        }
        /// <summary>
        /// 兄弟节点
        /// </summary>
        public BinNode<T> Sibling
        {
            get
            {
                return IsLChild ? this.Parent.RChild : this.Parent.LChild;
            }
        }

        public BinNode<T> Uncle
        {
            get
            {
                return this.Parent.IsLChild ? 
                    this.Parent.Parent.RChild : this.Parent.Parent.LChild;
            }
        }

        public BinNode<T> FromParentTo
        {
            get { return IsRoot ? this : (IsLChild ? this.Parent.LChild : this.Parent.RChild); }
            //set { (IsRoot ? this : (IsLChild ? this.Parent.LChild : this.Parent.RChild)) = value; }
        } 
        #endregion

    }
}
