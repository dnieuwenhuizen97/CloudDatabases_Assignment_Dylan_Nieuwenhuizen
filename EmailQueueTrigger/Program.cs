using DAL;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;

namespace EmailQueueTrigger
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(Configure)
                .Build();

            host.Run();
        }

        private static void Configure(HostBuilderContext buider, IServiceCollection Services)
        {
            Services.AddSingleton<ICustomerService, CustomerService>();
            Services.AddSingleton<IStorageService, StorageService>();
            Services.AddSingleton<IEmailService, EmailService>();
            Services.AddSingleton<DatabaseContext>();
            Services.AddSingleton<CustomerDb>();
        }
    }
}