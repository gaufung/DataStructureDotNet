namespace Stack
{
    interface IStack<T>
    {
        int Size { get; }
        bool Empty { get; }
        void Push(T e);
        T Pop();
        T Top();
    }
}
