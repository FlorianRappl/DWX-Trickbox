namespace Trickbox
{
    using System;
    using System.Diagnostics;

    class VarArgs
    {
        /// <summary>
        /// Hey children - don't do that at home
        /// </summary>
        public static void Flexible(__arglist)
        {
            // Actually pretty raw access here
            var ai = new ArgIterator(__arglist);

            while (ai.GetRemainingCount() > 0)
            {
                var typeRef = ai.GetNextArg();
                // Need to get the object reference from the
                // combined (typeref, objectref) element
                var obj = TypedReference.ToObject(typeRef);

                Debug.WriteLine(obj);
            }
        }

        public static void CallFlexible()
        {
            // Compare this to params object[] ... call
            // much less overhead and really direct
            Flexible(__arglist(2, 3, 4, "Hi there"));
        }
    }
}
