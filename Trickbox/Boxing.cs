namespace Trickbox
{
    using System;
    using System.Globalization;

    static class Boxing
    {
        /// <summary>
        /// This is alright in most cases, but if we provide a struct than it is
        /// quite easy to actually do better.
        /// </summary>
        public static int GetLengthBoxed(this IFormattable formattable)
        {
            return formattable.ToString(null, CultureInfo.CurrentCulture)?.Length ?? 0;
        }

        /// <summary>
        /// Here we save the cost of "boxing" a struct. In this process a new object
        /// is created just to transport a reference to the original value.
        /// </summary>
        public static int GetLengthBoxed<T>(this T formattable)
            where T : IFormattable
        {
            return formattable.ToString(null, CultureInfo.CurrentCulture)?.Length ?? 0;
        }
    }
}
