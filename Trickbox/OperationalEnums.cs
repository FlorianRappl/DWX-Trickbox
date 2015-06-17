namespace Trickbox
{
    using System;
    using System.Globalization;

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

    static class OperationalEnums
    {
        public static bool IsStartOfWeek(this DayOfWeek weekDay)
        {
            return weekDay == CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        public static bool IsCombinationFulfilled(this MyRules rules)
        {
            return rules.HasFlag(MyRules.First | MyRules.Third);
        }
    }
}