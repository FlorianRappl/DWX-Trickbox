namespace Trickbox
{
    class MotherClass
    {
        private int _secret = 42;

        public class DaughterClass : MotherClass
        {
            public int GetSecretOf(MotherClass mother)
            {
                return mother._secret;
            }
        }
    }
}
