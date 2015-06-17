namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    // using "using static" for "including" all static members
    using static System.Math;

    class AllNewFeatures
    {
        public AllNewFeatures(string myProperty)
        {
            // Such null-checks are much better with nameof()
            if (myProperty == null)
                throw new ArgumentNullException(nameof(myProperty));

            // Assignment of readonly auto-property
            MyProperty = myProperty;
        }

        public string MyProperty
        {
            // Only a getter? This is a readonly auto-property
            get;
        }

        // "Normal" auto-property
        public string OtherProperty
        {
            get;
            set;
        }

        // Here we are using two things:
        public static double Compute(double x, double y) => // 1. A method body expression
            Abs(Cos(x) * Sin(y) * Pow(x, y)); // 2. The static methods from System.Math

        public static Dictionary<string, string> Create()
        {
            return new Dictionary<string, string>
            {
                // Index initializers used to setup a dictionary
                ["First"] = "Something",
                ["Second"] = "Something else"
            };
        }

        public static string ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
                // Let's do some catching for all PathTooLongException types
            } catch(PathTooLongException)
                when (fileName.Length < 255) // ... but restrict it to the decribed case
            {
                return null;
            }
        }

        public override string ToString()
        {
            // We are not sure if OtherProperty has been set ...
            var length = OtherProperty?.Length ?? 0;

            // Why the "??" operator? The code above without "?? 0" is equivalent to:

            /*
                int? length = null;

                if (OtherProperty != null)
                    length = OtherProperty.Length;
            */

            // However, now it transforms to look as follows:

            /*
                int? _length = null;

                if (OtherProperty != null)
                    _length = OtherProperty.Length;

                int length = _length ?? 0;
            */

            // Without optimization this actually becomes:

            /*
                int length;

                if (OtherProperty != null)
                    length = OtherProperty.Length;
                else
                    length = 0;
            */

            // Or even shorter:

            // var length = OtherProperty != null ? OtherProperty.Length : 0;

            return $"The value of MyProperty is {MyProperty}, with another string of {length} characters.";
        }
    }
}
