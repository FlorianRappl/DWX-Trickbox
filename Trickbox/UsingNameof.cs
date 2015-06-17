namespace Trickbox
{
    using System;

    class UsingNameof
    {
        public UsingNameof(Object argument)
        {
            if (argument == null)
                throw new ArgumentNullException(nameof(argument));
            
            // ...
        }

        public int Foo() => 42;

        public override string ToString()
        {
            return $"Name {nameof(UsingNameof)} in namespace {nameof(Trickbox)} with method {nameof(UsingNameof.Foo)}.";
        }
    }
}
