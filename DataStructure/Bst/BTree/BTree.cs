using System;


namespace Bst.BTree
{
    public class BTree<T> where T :IComparable
    {
        public int Size { get; private set; }
        private readonly int _order;
        public BtNode<T> Root { get; private set; }
        public BtNode<T> Hot { get; private  set; }

        public bool Empty
        {
            get
            {
                return Root == null;
            }
        }

        protected void SolveOverFlow(BtNode<T> x)
        {
            throw new NotImplementedException();
            
        }

        protected void SolveUnderFlow(BtNode<T> x)
        {
            throw new NotImplementedException();
        }

        public BTree(int order = 3)
        {
            _order = 3;
            Size = 0;
            Root=new BtNode<T>();
        }

        public BtNode<T> Search(T e)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T e)
        {
            throw new NotImplementedException();
        }

        public bool Insert(T e)
        {
            throw new NotImplementedException();
        }

    }
}
