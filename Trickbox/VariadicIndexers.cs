namespace Trickbox
{
    class VariadicIndexers
    {
        public int this[params string[] arguments]
        {
            get { return arguments.Length; }
        }
    }
}
