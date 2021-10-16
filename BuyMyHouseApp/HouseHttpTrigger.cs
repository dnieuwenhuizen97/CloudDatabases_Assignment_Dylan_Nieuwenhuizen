using Domains;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace BuyMyHouseApp
{
    public class HouseHttpTrigger
    {
        [Function(nameof(GetHouseByPriceRange))]
        [OpenApiOperation(operationId: "getHousesByPriceRange", tags: new[] { "house" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "lowestPrice", In = ParameterLocation.Query, Required = true, Type = typeof(double), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "highestPrice", In = ParameterLocation.Query, Required = true, Type = typeof(double), Visibility = OpenApiVisibilityType.Important)]
        public async Task<HttpResponseData> GetHouseByPriceRange([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "house")] HttpRequestData req, FunctionContext executionContext, double lowestPrice, double highestPrice)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync($"The values are: {lowestPrice.ToString("0.00")} and {highestPrice.ToString("0.00")}");

            return response;
        }
    }
}
