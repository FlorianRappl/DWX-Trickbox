namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using static System.Math;

    class AllNewFeatures
    {
        public AllNewFeatures(string myProperty)
        {
            if (myProperty == null)
                throw new ArgumentNullException(nameof(myProperty));

            MyProperty = myProperty;
        }

        public string MyProperty
        {
            get;
        }

        public string OtherProperty
        {
            get;
            set;
        }

        public static double Compute(double x, double y) => 
            Abs(Cos(x) * Sin(y) * Pow(x, y));

        public static Dictionary<string, string> Create()
        {
            return new Dictionary<string, string>
            {
                ["First"] = "Something",
                ["Second"] = "Something else"
            };
        }

        public static string ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            } catch(PathTooLongException) 
                when (fileName.Length < 255)
            {
                return null;
            }
        }

        public override string ToString()
        {
            var length = OtherProperty?.Length ?? 0;
            return $"The value of MyProperty is {MyProperty}, with another string of {length} characters.";
        }
    }
}
