using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Http;
using BIPS.PRESENTACION;

namespace BIPS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuracion de IhttpClientFactory
            var ServiceCollection = new ServiceCollection();
            Configure(ServiceCollection);
            var servicios = ServiceCollection.BuildServiceProvider();
            var MyHttpClientFactory = servicios.GetRequiredService<IHttpClientFactory>();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormEmpresa());
        }

        private static void Configure(ServiceCollection service)
        {
            service.AddHttpClient();
            
        }
    }
}