namespace Trickbox
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    static class PLinq
    {
        /// <summary>
        /// Sequential does work; but we are CPU bound and we can do better.
        /// </summary>
        public static int CountPrimesSequential(this IEnumerable<int> input)
        {
            return input.Where(IsPrime).Count();
        }

        /// <summary>
        /// This works fine in parallel -- good speedup and great use-case.
        /// </summary>
        public static int CountPrimesParallel(this IEnumerable<int> input)
        {
            return input.AsParallel().Where(IsPrime).Count();
        }

        /// <summary>
        /// Most of the time will be spent in reading the files ...
        /// Sequential is doing a good job already.
        /// </summary>
        public static int CountWordsInFilesSequential(this IEnumerable<string> input)
        {
            return input.Select(CountWords).Sum();
        }

        /// <summary>
        /// The only thing that could be better is to generally improve the 
        /// algorithm. Here we are just wasting threads. And the main thread (which is
        /// also part of the parallel execution) will block the event loop. Bad!
        /// </summary>
        public static int CountWordsInFilesParallel(this IEnumerable<string> input)
        {
            return input.AsParallel().Select(CountWords).Sum();
        }

        /// <summary>
        /// Now a much better implementation. Instead of parallelizing, we do the
        /// IO stuff asynchronously (concurrently).
        /// </summary>
        public static async Task<int> CountWordsInFilesAsync(this IEnumerable<string> inputs)
        {
            var words = 0;

            foreach (var input in inputs)
            {
                using (var file = File.OpenRead(input))
                {
                    using (var reader = new StreamReader(file))
                    {
                        // Never blocks the event loop
                        var line = await reader.ReadLineAsync();
                        words += CountWordsInLine(line);
                    }
                }
            }

            return words;
        }

        static bool IsPrime(int n)
        {
            for (int i = 2; i * i < n; ++i)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        static int CountWords(string fileName)
        {
            var words = 0;

            using (var file = File.OpenRead(fileName))
            {
                using (var reader = new StreamReader(file))
                {
                    var line = reader.ReadLine();
                    words += CountWordsInLine(line);
                }
            }

            return words;
        }

        static int CountWordsInLine(string line)
        {
            var words = 0;
            var ascii = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsLetter(line[i]))
                {
                    if (!ascii)
                        words++;

                    ascii = true;
                }
                else
                    ascii = false;
            }

            return words;
        }
    }
}
