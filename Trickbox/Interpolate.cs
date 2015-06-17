namespace Trickbox
{
    using System;
    using static System.FormattableString;

    class Interpolate
    {
        public static string WithComputation()
        {
            return $"We can also compute stuff {5 * 8 + 2}!";
        }

        public static string WithFormat()
        {
            var number = 31;
            return $"Using formatters is possible, like {number:X2}!";
        }

        public static string WithInvariantCulture()
        {
            var number = 3.1415;
            return Invariant($"Printing PI culture-independent yields {number}.");
        }

        public static string WithCustomFormatter()
        {
            var protocol = "http";
            var host = "www.google.com";
            var path = "search";
            var query = "q=test";
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
