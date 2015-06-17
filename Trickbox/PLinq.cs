namespace Trickbox
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class PLinq
    {
        public static int CountPrimesParallel(this IEnumerable<int> input)
        {
            return input.AsParallel().Where(IsPrime).Count();
        }

        public static int CountPrimesSequential(this IEnumerable<int> input)
        {
            return input.Where(IsPrime).Count();
        }

        public static int CountWordsInFilesParallel(this IEnumerable<string> input)
        {
            return input.AsParallel().Select(CountWords).Count();
        }

        public static int CountWordsInFilesSequential(this IEnumerable<string> input)
        {
            return input.Select(CountWords).Count();
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
                    var ascii = false;
                    var line = reader.ReadLine();

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
                }
            }

            return words;
        }
    }
}
