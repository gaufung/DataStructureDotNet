using System;

namespace Sequence
{
    [Serializable]
    internal static class MathHelper
    {
        public static int NextPrime(int currentValue)
        {
            while (true)
            {
                if (IsPrime(currentValue))
                    return currentValue;
                currentValue++;
            }
        }


        public static int RandomLevel(int maxLevel)
        {
            Random rand = new Random();
            int lvl = (int)(Math.Log(1.0 - rand.NextDouble()) / Math.Log(1.0 - 1/Math.E));
            return Math.Min(lvl, maxLevel);
        }

        private static bool IsPrime(int n)
        {
            if (n == 2)
                return true;
            if (n == 3)
                return true;
            if (n%2 == 0)
                return false;
            if (n%3 == 0)
                return false;
            int i = 5;
            int w = 2;
            while (i*i<=n)
            {
                if (n%i == 0)
                    return false;
                i += w;
                w = 6 - w;
            }
            return true;
        }
    }
}
