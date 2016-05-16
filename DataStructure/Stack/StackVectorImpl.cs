using System;

namespace Sequence
{
    public class StackVectorImpl<T>:Stack<T> where T:IComparable<T>
    {

        #region 方法工厂

        public static Stack<T> StackFactory()
        {
            return new StackVectorImpl<T>();
        }
        #endregion

        private readonly IVector<T> _vector;

        private  StackVectorImpl()
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
            get
            {
                if (Empty) return default(T);
                return _vector[_vector.Size - 1];
            }
        }

        public override int Find(T other)
        {
            return _vector.Find(other);
        }
    
        public override void Foreach(Action<T> traverse)
        {
            _vector.Foreach(traverse);
        }
    }
}
