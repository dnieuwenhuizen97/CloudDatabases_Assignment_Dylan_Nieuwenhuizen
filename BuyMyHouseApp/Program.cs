using DAL;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BuyMyHouseApp
{
    public class Program
    {

        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
                .ConfigureServices(Configure)
                .Build();

            host.Run();
        }

        private static void Configure(HostBuilderContext buider, IServiceCollection Services)
        {
            Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
            Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();
            Services.AddSingleton<ICustomerService, CustomerService>();
            Services.AddSingleton<IStorageService, StorageService>();
            Services.AddSingleton<IHouseService, HouseService>();
            Services.AddSingleton<DatabaseContext>();
            Services.AddSingleton<CustomerDb>();
            Services.AddSingleton<HouseDb>();
        }
    }
}