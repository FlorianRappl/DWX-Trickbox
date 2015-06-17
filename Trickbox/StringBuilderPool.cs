namespace Trickbox
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;

    /// <summary>
    /// Pools can be great to prevent wasting resources
    /// </summary>
    static class StringBuilderPool
    {
        // Use a concurrent stack to avoid race-conditions
        static readonly ConcurrentStack<StringBuilder> pool = 
            new ConcurrentStack<StringBuilder>();

        public static StringBuilder Acquire()
        {
            // We abuse the default() CF, again
            var builder = default(StringBuilder);
            pool.TryPop(out builder);
            // Either return the already created SB or a new one
            return builder ?? new StringBuilder();
        }

        public static String Release(this StringBuilder builder)
        {
            // Stringify it and return a (clean) one
            var str = builder.ToString();
            pool.Push(builder.Clear());
            // Return the generated string
            return str;
        }
    }
}
