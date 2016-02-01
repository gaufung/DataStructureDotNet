using System;
using List;
namespace Queue
{
    public class Queue<T>:IQueue<T> where T:IComparable
    {
        private readonly List<T> _list;
        public Queue()
        {
            _list=new List<T>();
        }
        public void Enqueue(T e)
        {
            _list.InsertAsLast(e);
        }
        public T Dequeue()
        {
            return _list.Remove(_list.First);
        }
        public T Front
        {
            get
            {
                return _list.First.Data;
            }
            set
            {
                _list.First.Data = value;
            }
        }
        public bool Empty
        {
            get { return _list.Empty; }
        }
    }
}
