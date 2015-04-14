using System;
namespace Bst
{
    public class Entry<TK,TV>:IComparable
    {
        private readonly  TK _key;
        private readonly TV _value;

        public Entry(TK key,TV value)
        {
            _key = key;
            _value = value;
        }
        public Entry(Entry<TK, TV> entry)
        {
            _key = entry._key;
            _value = entry._value;
        } 
        public int CompareTo(object obj)
        {
            return (_key as IComparable).CompareTo(obj as IComparable);
        }
    }
}
