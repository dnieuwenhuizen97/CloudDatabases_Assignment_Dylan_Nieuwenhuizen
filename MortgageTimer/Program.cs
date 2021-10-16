using Azure.Storage.Blobs;
using DAL;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;

namespace MortgageTimer
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
            Services.AddScoped(_ => { return new BlobServiceClient(buider.Configuration.GetConnectionString("AzureBlobStorage")); });
            Services.AddSingleton<ICustomerService, CustomerService>();
            Services.AddSingleton<DatabaseContext>();
            Services.AddSingleton<CustomerDb>();
        }
    }
}