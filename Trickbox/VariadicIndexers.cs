namespace Trickbox
{
    class VariadicIndexers
    {
        /// <summary>
        /// Don't forget: Indexers are methods, too.
        /// </summary>
        public int this[params string[] arguments]
        {
            // Easy to supply params parameter ...
            get { return arguments.Length; }
        }
    }
}
