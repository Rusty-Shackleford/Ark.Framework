﻿using System;

namespace Ark.Framework.Demo
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new PanelTest())
                game.Run();
        }
    }
#endif
}
