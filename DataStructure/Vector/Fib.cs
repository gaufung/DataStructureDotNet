//-----------------------------------------------------------------------
// <copyright file="Fib.cs" company="gaufung.com">
//     All right reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Sequence
{
    /// <summary>
    /// Fibonacci class for fib search.
    /// </summary>
    internal class Fib
    {
        /// <summary>
        /// The f value.
        /// </summary>
        private int f;
        
        /// <summary>
        /// The g value.
        /// </summary>
        private int g;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fib"/> class.
        /// </summary>
        /// <param name="n">The size of fib.</param>
        public Fib(int n)
        {
            this.f = 1;
            this.g = 0;
            while (this.g < n)
            {
                this.Next();
            }
        }

        /// <summary>
        /// The fib's value.
        /// </summary>
        public int Value => this.g;

        /// <summary>
        /// Previous Value.
        /// </summary>
        /// <returns>The value.</returns>
        public int Prev()
        {
            this.f = this.g - this.f;
            this.g -= this.f;
            return this.g;
        }

        /// <summary>
        /// Next iteration.
        /// </summary>
        /// <returns>The value.</returns>
        private int Next()
        {
            this.g += this.f;
            this.f = this.g - this.f;
            return this.g;
        }
    }
}
