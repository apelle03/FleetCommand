using System;

namespace Fleet_Command {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FC game = new FC())
            {
                game.Run();
            }
        }
    }
#endif
}

