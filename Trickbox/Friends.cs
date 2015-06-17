namespace Trickbox
{
    /// <summary>
    /// Distinguish between very close relationships by using nested classes.
    /// If we inherit outside of MotherClass, e.g.,
    /// 
    ///     SonClass : MotherClass,
    /// 
    /// then we do not want to share the closest secrets. However, inheriting
    /// inside (nested) gives us access to the private methods of the parent
    /// class. The mother is sharing her secrets with the daughter; this is a
    /// closer relationship.
    /// </summary>
    class MotherClass
    {
        // e.g. consider a private field
        private int _secret = 42;

        public class DaughterClass : MotherClass
        {
            public int GetSecretOf(MotherClass mother)
            {
                // accessible since we are inside the MotherClass
                return mother._secret;
            }
        }
    }
}
