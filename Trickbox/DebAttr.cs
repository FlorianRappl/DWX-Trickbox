namespace Trickbox
{
    using System.Diagnostics;

    /// <summary>
    /// We can determine what should be displayed in the debugger column
    /// </summary>
    [DebuggerDisplay(value: "ExampleClass")]
    class DebAttr
    {
        // Or what should be hidden for the compiler
        [DebuggerHidden]
        public void Foo()
        {
            int a = 3;
            int b = 9;
            Debug.WriteLine(a + b);
        }

        // Or what shouldn't be "stepped-through"
        [DebuggerStepThrough]
        public void Bar()
        {
            // Long (and well tested!) logic here
        }
    }
}
