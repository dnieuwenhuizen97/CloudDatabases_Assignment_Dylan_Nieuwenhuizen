using Domains;
using Domains.DTO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BuyMyHouseApp
{
    public class CustomerHttpTrigger
    {
        private ICustomerService CustomerService { get; }

        public CustomerHttpTrigger(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [Function(nameof(UpdateFinancialInformation))]
        [OpenApiOperation(operationId: "updateFinancialInformation", tags: new[] { "user" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "customerId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "annualSalary", In = ParameterLocation.Query, Required = true, Type = typeof(double), Visibility = OpenApiVisibilityType.Important)]
        public async Task<HttpResponseData> UpdateFinancialInformation([HttpTrigger(AuthorizationLevel.Anonymous, "PUT", Route = "user/financial")] HttpRequestData req, FunctionContext executionContext, string customerId, double annualSalary)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            FinancialInformationDTO financialInformation = await CustomerService.UpdateCustomerFinancialInfo(customerId, annualSalary);

            await response.WriteAsJsonAsync(financialInformation);

            return response;
        }
    }
}
