namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Prefer dictionaries (unless good (maybe performance) reasons not to)
    /// over a more traditional switch-case
    /// </summary>
    class DictionaryVsSwitch
    {
        #region Dictionary

        static readonly Dictionary<string, Action<string[]>> actions = new Dictionary<string, Action<string[]>>
        {
            // We do not take care if the provided number of arguments is correct
            ["mv"] = args => File.Move(args[0], args[1]),
            // We use the new index initializers
            ["rm"] = args => File.Delete(args[0]),
            ["cp"] = args => File.Copy(args[0], args[1]),
            ["touch"] = args => File.AppendAllText(args[0], String.Empty)
        };

        public static void ExecuteWithDictionary(string command, params string[] arguments)
        {
            // We could also use Action<string[]> action; - but let's abuse the default() CF
            var action = default(Action<string[]>);
            
            // Try-Get to combine search (is there something?) and retrieval in one call
            if (actions.TryGetValue(command, out action))
                action(arguments);
        }

        #endregion

        #region Switch

        public static void ExecuteWithSwitch(string command, params string[] arguments)
        {
            // This is not only tedious, it is also less flexible and violates SOLID
            switch (command)
            {
                case "mv":
                    // Again we do not care about the number of arguments
                    File.Move(arguments[0], arguments[1]);
                    break;
                case "rm":
                    File.Delete(arguments[0]);
                    break;
                case "cp":
                    File.Copy(arguments[0], arguments[1]);
                    break;
                case "touch":
                    File.AppendAllText(arguments[0], String.Empty);
                    break;
            }
        }

        #endregion
    }
}
