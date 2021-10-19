using Domains;
using Domains.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BuyMyHouseApp
{
    public class HouseHttpTrigger
    {
        private IHouseService HouseService { get; }

        public HouseHttpTrigger(IHouseService houseService)
        {
            HouseService = houseService;
        }

        [Function(nameof(GetHouseByPriceRange))]
        [OpenApiOperation(operationId: "getHousesByPriceRange", tags: new[] { "house" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "lowestPrice", In = ParameterLocation.Query, Required = true, Type = typeof(double), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "highestPrice", In = ParameterLocation.Query, Required = true, Type = typeof(double), Visibility = OpenApiVisibilityType.Important)]
        public async Task<HttpResponseData> GetHouseByPriceRange([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "house")] HttpRequestData req, double lowestPrice, double highestPrice)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            List<HouseDTO> houseDTOs = await HouseService.GetHousesByPriceRange(lowestPrice, highestPrice);

            await response.WriteAsJsonAsync(houseDTOs);

            return response;
        }

        [Function(nameof(UploadHouseImage))]
        [OpenApiOperation(operationId: "uploadHouseImage", tags: new[] { "house" }, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "houseId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        public async Task<HttpResponseData> UploadHouseImage([HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "house/images")] HttpRequestData req, string houseId)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            string houseImageUrl = await HouseService.UploadHouseImage(houseId);

            await response.WriteAsJsonAsync(houseImageUrl);

            return response;
        }
    }
}
