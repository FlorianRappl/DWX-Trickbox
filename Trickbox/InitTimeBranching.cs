namespace Trickbox
{
    using System;

    class InitTimeBranching
    {
        readonly Action<String> log;

        public InitTimeBranching(bool outputToConsole)
        {
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

            action = () =>
            {
                log("Initial call; setting up stuff ...");
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
