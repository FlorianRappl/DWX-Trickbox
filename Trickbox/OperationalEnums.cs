namespace Trickbox
{
    using System;
    using System.Globalization;

    /// <summary>
    /// A simple enum; let's pretend we did not create it and we 
    /// simply cannot change it.
    /// </summary>
    [Flags]
    enum MyRules
    {
        None = 0,
        First = 0x1,
        Second = 0x2,
        Third = 0x4,
        Fourth = 0x8,
        Fifth = 0x10,
        // ...
    }

    /// <summary>
    /// We should not forget that enumerations are "types", too.
    /// </summary>
    static class OperationalEnums
    {
        /// <summary>
        /// Classic scenario illustrated with DayOfWeek. A handy method
        /// that just gives us an answer to: "Is the current day of the
        /// week the start of the week for the user's current locale?"
        /// </summary>
        public static bool IsStartOfWeek(this DayOfWeek weekDay)
        {
            return weekDay == CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        /// <summary>
        /// Now let's apply this to the enum defined above. Since we are
        /// not allowed to modify it, we always have to write the code
        /// shown in the method. But that is tedious, error-prone and
        /// will eventually lead to refactoring problems. Hence let's
        /// create a method - an extension method.
        /// </summary>
        public static bool IsCombinationFulfilled(this MyRules rules)
        {
            return rules.HasFlag(MyRules.First | MyRules.Third);
        }
    }
}