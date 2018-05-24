using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool runSeed = false;

            if (args.Contains("seed"))
            {
                runSeed = true;
                args = args.Where(d => d != "seed").ToArray();
            }

            var host = BuildWebHost(args);

            //Executa com o Seed
            if (runSeed) RunSeed(host).Wait();

            //Executa normalmente
            else host.Run();
        }

        private static async Task RunSeed(IWebHost host)
        {
            Console.WriteLine("Running seed...");
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<Db>();
                try
                {
                    //Inicializa o Banco de Dados
                    await DbInitializer.Initialize(dbContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
                finally
                {
                    Console.WriteLine("Seed ended");
                    Console.Read();
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
