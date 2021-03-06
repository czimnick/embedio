﻿namespace Unosquare.Labs.EmbedIO.Command
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unosquare.Labs.EmbedIO Web Server");
            Console.WriteLine("  Command-Line Utility: Press any key to stop the server.");

            using (var server = new WebServer(Properties.Settings.Default.ServerAddress, new Logger()))
            {
                if (Properties.Settings.Default.UseLocalSessionModule)
                    server.RegisterModule(new Modules.LocalSessionModule());

                var staticFilesModule = new Modules.StaticFilesModule(Properties.Settings.Default.HtmlRootPath)
                {
                    DefaultDocument = Properties.Settings.Default.HtmlDefaultDocument,
                    DefaultExtension = Properties.Settings.Default.HtmlDefaultExtension,
                    UseRamCache = Properties.Settings.Default.UseRamCache
                };

                server.RegisterModule(staticFilesModule);

                // start the server
                server.RunAsync();
                Console.ReadKey(true);
            }
        }
    }
}
