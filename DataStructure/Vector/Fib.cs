
namespace Vector
{
    /// <summary>
    /// Fibonacci类
    /// </summary>
    internal class Fib
    {
        private int _f;
        private int _g;

        public Fib(int n)
        {
            _f = 1;
            _g = 0;
            while (_g<n)
            {
                Next();
            }
        }
        public int Get()
        {
            return _g;
        }

        public int Next()
        {
            _g += _f;
            _f = _g - _f;
            return _g;
        }

        public int Prev()
        {
            _f = _g - _f;
            _g -= _f;
            return _g;
        }
    }
}
