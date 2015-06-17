namespace Trickbox
{
    partial class PartialClass
    {
        partial void Communicate(string message);

        public PartialClass()
        {
            Communicate("Constructor called.");
        }

        public override string ToString()
        {
            Communicate("ToString called");
            return base.ToString();
        }
    }
}
