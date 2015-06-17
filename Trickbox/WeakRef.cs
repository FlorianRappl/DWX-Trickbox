namespace Trickbox
{
    using System;

    class WeakRef
    {
        WeakReference wrObj;
        WeakReference<String> wrStr;

        public Object Obj
        {
            get { return wrObj?.Target; }
            set { wrObj = new WeakReference(value); }
        }

        public String Str
        {
            get
            {
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
