namespace Trickbox.External
{
    public class Settings
    {
        // Constant, resolved during compile-time
        public const int AnswerToEverything = 42;

        // Here the value is resolved during run-time
        public static readonly int MagicNumber = 97;
    }
}
