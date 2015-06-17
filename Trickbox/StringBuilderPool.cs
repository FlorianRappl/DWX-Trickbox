namespace Trickbox
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;

    static class StringBuilderPool
    {
        static readonly ConcurrentStack<StringBuilder> pool = 
            new ConcurrentStack<StringBuilder>();

        public static StringBuilder Acquire()
        {
            var builder = default(StringBuilder);
            pool.TryPop(out builder);
            return builder ?? new StringBuilder();
        }

        public static String Release(this StringBuilder builder)
        {
            var str = builder.ToString();
            pool.Push(builder.Clear());
            return str;
        }
    }
}
