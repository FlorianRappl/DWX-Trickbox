namespace Trickbox
{
    using Trickbox.External;

    class ConstVsReadonly
    {
        public static string Watch()
        {
            // Will be resolved during compile-time
            var num1 = Settings.AnswerToEverything;
            // Will be resolved during run-time
            var num2 = Settings.MagicNumber;

            return $"The answer to everything is {num1}, the magic number {num2}.";
        }
    }
}
