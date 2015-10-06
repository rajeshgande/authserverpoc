using System;
using Microsoft.Owin.Hosting;
using OcsAuthServer.Infrastructure;

namespace OcsAuthServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting web Server...");
            // Specify the URI to use for the local host:
            string baseUri = "http://localhost:8080";

            var startOptions = new StartOptions();
            startOptions.Urls.Add(baseUri);
            WebApp.Start(startOptions, (appBuilder) => { new WebApiHostBootstrapper().Configuration(appBuilder); });
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
    }
}
