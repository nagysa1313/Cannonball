﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Cannonball
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
            using (var game = new WorldTestGame())
            //using (var game = new SphereTestGame())
            //using (var game = new ParticleTestGame())
            //using (var game = new PlasmaTestGame())
            //using (var game = new LSystemTestGame())
            //using (var game = new LightningTestGame())
            //using (var game = new PrimitiveTestGame())
                game.Run();
        }
    }
#endif
}