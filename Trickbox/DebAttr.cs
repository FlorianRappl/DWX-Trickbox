namespace Trickbox
{
    using System.Diagnostics;

    [DebuggerDisplay(value: "ExampleClass")]
    class DebAttr
    {
        [DebuggerHidden]
        public void Foo()
        {
            int a = 3;
            int b = 9;
            Debug.WriteLine(a + b);
        }
    }
}
