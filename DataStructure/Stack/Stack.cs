using System;
using Vector;
namespace Stack
{
    public class Stack<T>:IStack<T> where T:IComparable
    {
        private readonly Vector<T> _vector;

        public Stack()
        {
            _vector=new Vector<T>();
        }
        public int Size
        {
            get { return _vector.Size; }
        }

        public bool Empty
        {
            get { return _vector.Empty; }
        }

        public void Push(T e)
        {
            _vector.Insert(e);
        }

        public T Pop()
        {
          
            return _vector.Remove(_vector.Size - 1);
        }

        public T Top()
        {
            return _vector[_vector.Size - 1];
        }
    }
}
