namespace Trickbox
{
    using System;
    // Used to "import" the static method Invariant
    using static System.FormattableString;

    /// <summary>
    /// String interpolation is among the most useful new features.
    /// </summary>
    class Interpolate
    {
        /// <summary>
        /// String interpolation works with any expression.
        /// </summary>
        public static string WithComputation()
        {
            return $"We can also compute stuff {5 * 8 + 2}!";
        }

        /// <summary>
        /// Formatters can also be supplied - just separated the expression
        /// from the formatter with a colon.
        /// </summary>
        public static string WithFormat()
        {
            var number = 31;
            return $"Using formatters is possible, like {number:X2}!";
        }

        /// <summary>
        /// Even the culture can be changed. This is, however, only possible in
        /// conjunction with a framework feature: FormattableString
        /// </summary>
        public static string WithInvariantCulture()
        {
            var number = 3.1415;
            return Invariant($"Printing PI culture-independent yields {number}.");
        }

        /// <summary>
        /// Finally we can use this to build our own FormattableString targets.
        /// </summary>
        public static string WithCustomFormatter()
        {
            var protocol = "http";
            var host = "www.google.com";
            var path = "search";
            var query = "q=test";
            // Nice and readable: Also sanatizes the string!
            return Url($"{protocol}://{host}/{path}?{query}");
        }

        static string Url(FormattableString str)
        {
            return str.ToString(UrlFormatProvider.Instance);
        }

        class UrlFormatProvider : IFormatProvider
        {
            private readonly UrlFormatter _formatter = new UrlFormatter();

            private UrlFormatProvider() { }

            public static readonly UrlFormatProvider Instance = new UrlFormatProvider();

            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter))
                    return _formatter;
                return null;
            }

            class UrlFormatter : ICustomFormatter
            {
                public string Format(string format, object arg, IFormatProvider formatProvider)
                {
                    if (arg == null)
                        return string.Empty;
                    if (format == "r")
                        return arg.ToString();
                    return Uri.EscapeDataString(arg.ToString());
                }
            }
        }
    }
}
