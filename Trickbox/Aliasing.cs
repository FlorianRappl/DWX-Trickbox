namespace Trickbox
{
    // Aliasing whole namespaces
    using Sys = System;
    // typedef for types, e.g., specialized generic classes
    using Rational = System.Tuple<System.Int32, System.Int32>;
    // Rename to prevent namespace clashes
    using Terminal = System.Console;
    // This is not aliasing, but another possibility
    using static System.Math;

    class Aliasing
    {
        public static void UseRational()
        {
            // The intend is stated much better than with new Tuple...
            var pi = new Rational(22, 7);
            var diff = Abs(PI * pi.Item2 - pi.Item1) / pi.Item2;

            Terminal.ForegroundColor = Sys.ConsoleColor.Green;
            Terminal.WriteLine($"The difference to the real PI is {diff}.");
            Terminal.ResetColor();
        }
    }
}
