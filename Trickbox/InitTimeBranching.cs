namespace Trickbox
{
    using System;

    /// <summary>
    /// Some concepts from FP: 
    /// * init-time branching and 
    /// * self-defining functions.
    /// </summary>
    class InitTimeBranching
    {
        readonly Action<String> log;

        public InitTimeBranching(bool outputToConsole)
        {
            // Boolean evaluation cached to the consequence directly
            if (outputToConsole)
                log = Console.WriteLine;
            else 
                log = m => { };
        }

        public void Foo()
        {
            log("Started");
            // ...
            log("Finished");
        }

        public void SelfDefining()
        {
            var infinity = 1;// or much (!) larger
            var action = default(Action);

            // Reference to method, which ...
            action = () =>
            {
                log("Initial call; setting up stuff ...");
                // redefines itself after the first call.
                action = () => { };
            };

            for (int i = 0; i < infinity; i++)
            {
                action();
                // something else
            }
        }
    }
}
