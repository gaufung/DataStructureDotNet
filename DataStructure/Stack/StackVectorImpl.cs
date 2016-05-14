using System;

namespace Sequence
{
    internal class StackVectorImpl<T>:Stack<T> where T:IComparable<T>
    {
        private readonly IVector<T> _vector;

        public StackVectorImpl()
        {
            _vector = Vector<T>.VectorFactory();
        }
        public override bool Empty
        {
            get { return _vector.Empty; }
        }

        public override int Size
        {
            get { return _vector.Size; }
        }

        public override void Push(T e)
        {
            _vector.Insert(e);
        }

        public override T Pop()
        {
            return _vector.Remove(_vector.Size - 1);
        }

        public override T Top
        {
            get { return _vector[_vector.Size-1]; }
        }

        public override int Find(T other)
        {
            return _vector.Find(other);
        }
    
        public override void Travese(Action<T> traves)
        {
            for (int i = 0; i < _vector.Size; i++)
            {
                traves(_vector[i]);
            }
        }
    }
}
