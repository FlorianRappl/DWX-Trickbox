namespace Trickbox
{
    /// <summary>
    /// Our "main" partial class. We want to call a special,
    /// probably "platform-dependent" method "Communicate" here.
    /// We could use classic OOP techniques for the job; but there
    /// is also another, simpler, solution.
    /// </summary>
    partial class PartialClass
    {
        /// <summary>
        /// Partial methods cannot have a return-type, since
        /// the partial class must also work without another
        /// counter-part. These classes are "partial", not
        /// "incomplete".
        /// </summary>
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
