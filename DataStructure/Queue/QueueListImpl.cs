using System;

namespace Sequence
{
    public class QueueListImpl<T>:Queue<T> where T:IComparable<T>
    {
        #region

        public static Queue<T> QueueFacotry()
        {
            return new QueueListImpl<T>();
        }
        #endregion


        private readonly IList<T> _list;

        private QueueListImpl()
        {
            _list = List<T>.ListFactory();
        }
        public override bool Empty
        {
            get { return _list.Empty; }
        }

        public override T Front
        {
            get { return _list.First.Data; }
        }

        public override T Dequeue()
        {
            return _list.Remove(_list.First);
        }

        public override void Enqueue(T e)
        {
            _list.InsertAsLast(e);
        }

        public override int Size
        {
            get { return _list.Size; }
        }

        public override void Foreach(Action<T> traverse)
        {
            _list.Foreach(traverse);
        }
    }
}
