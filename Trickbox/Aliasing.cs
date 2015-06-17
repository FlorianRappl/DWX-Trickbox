namespace Trickbox
{
    using Sys = System;
    using Rational = System.Tuple<System.Int32, System.Int32>;
    using Terminal = System.Console;
    using static System.Math;

    class Aliasing
    {
        public static void UseRational()
        {
            var pi = new Rational(22, 7);
            var diff = Abs(PI * pi.Item2 - pi.Item1) / pi.Item2;

            Terminal.ForegroundColor = Sys.ConsoleColor.Green;
            Terminal.WriteLine($"The difference to the real PI is {diff}.");
            Terminal.ResetColor();
        }
    }
}
