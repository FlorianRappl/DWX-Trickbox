namespace Trickbox
{
    using System;
    using System.Diagnostics;

    class VarArgs
    {
        public static void Flexible(__arglist)
        {
            var ai = new ArgIterator(__arglist);

            while (ai.GetRemainingCount() > 0)
            {
                var typeRef = ai.GetNextArg();
                var obj = TypedReference.ToObject(typeRef);

                Debug.WriteLine(obj);
            }
        }

        public static void CallFlexible()
        {
            Flexible(__arglist(2, 3, 4, "Hi there"));
        }
    }
}
