using DAL;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
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
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(Configure)
                .Build();

            host.Run();
        }

        private static void Configure(HostBuilderContext buider, IServiceCollection Services)
        {
            Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
            Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();
            //Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("AzureWebJobsStorage"));
            Services.AddSingleton<ICustomerService, CustomerService>();
            Services.AddSingleton<IHouseService, HouseService>();
            Services.AddSingleton<DatabaseContext>();
            Services.AddSingleton<CustomerDb>();
            Services.AddSingleton<HouseDb>();
        }
    }
}