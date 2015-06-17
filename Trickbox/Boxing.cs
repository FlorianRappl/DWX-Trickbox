namespace Trickbox
{
    using System;
    using System.Globalization;

    static class Boxing
    {
        public static int GetLengthBoxed(this IFormattable formattable)
        {
            return formattable.ToString(null, CultureInfo.CurrentCulture)?.Length ?? 0;
        }

        public static int GetLengthBoxed<T>(this T formattable)
            where T : IFormattable
        {
            return formattable.ToString(null, CultureInfo.CurrentCulture)?.Length ?? 0;
        }
    }
}
