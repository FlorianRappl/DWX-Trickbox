namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class DictionaryVsSwitch
    {
        #region Dictionary

        static readonly Dictionary<string, Action<string[]>> actions = new Dictionary<string, Action<string[]>>
        {
            ["mv"] = args => File.Move(args[0], args[1]),
            ["rm"] = args => File.Delete(args[0]),
            ["cp"] = args => File.Copy(args[0], args[1]),
            ["touch"] = args => File.AppendAllText(args[0], String.Empty)
        };

        public static void ExecuteWithDictionary(string command, params string[] arguments)
        {
            var action = default(Action<string[]>);
            
            if (actions.TryGetValue(command, out action))
                action(arguments);
        }

        #endregion

        #region Switch

        public static void ExecuteWithSwitch(string command, params string[] arguments)
        {
            switch (command)
            {
                case "mv":
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
