namespace Trickbox
{
    using Trickbox.External;

    class ConstVsReadonly
    {
        public static string Watch()
        {
            var num1 = Settings.AnswerToEverything;
            var num2 = Settings.MagicNumber;

            return $"The answer to everything is {num1}, the magic number {num2}.";
        }
    }
}
