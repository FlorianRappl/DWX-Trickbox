namespace Trickbox
{
    using System;

    class WeakRef
    {
        // "Classic" WR without type information
        WeakReference wrObj;
        // New .NET 4 kind of generic WR
        WeakReference<String> wrStr;

        public Object Obj
        {
            // Handy to access the target with the conditional member op.
            get { return wrObj?.Target; }
            set { wrObj = new WeakReference(value); }
        }

        public String Str
        {
            get
            {
                // We need the TryGetTarget method
                // and we abuse the default CF again
                var target = default(String);
                wrStr?.TryGetTarget(out target);
                return target;
            }
            set
            {
                wrStr = new WeakReference<String>(value);
            }
        }
    }
}
