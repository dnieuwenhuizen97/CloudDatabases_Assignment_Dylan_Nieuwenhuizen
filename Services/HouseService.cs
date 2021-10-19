using DAL;
using Domains;
using Domains.DTO;
using Domains.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HouseService : IHouseService
    {
        private IStorageService StorageService { get; }
        private HouseDb HouseDb { get; }

        public HouseService(IStorageService storageService, HouseDb houseDb)
        {
            StorageService = storageService;
            HouseDb = houseDb;
        }

        public async Task<List<HouseDTO>> GetHousesByPriceRange(double lowest, double highest)
        {
            List<House> houses = await HouseDb.FindHousesByPriceRange(lowest, highest);

            return HouseHelper.ListToDTO(houses);
        }

        public async Task<string> UploadHouseImage(string houseId)
        {
            int numberOfImages = await HouseDb.GetNumberOfHouseImagesById(houseId);

            Stream image = await GetRandomImage();

            string imageUrl = await StorageService.AddHouseImageToBlob(image, houseId, (numberOfImages + 1).ToString());

            await HouseDb.AddHouseImageUrlToTable(imageUrl, houseId);

            return imageUrl;
        }

        public Task<Stream> GetRandomImage()
        {
            WebClient webClient = new WebClient();
            byte[] randomImageBytes = webClient.DownloadData(Environment.GetEnvironmentVariable("RandomImages"));
            Stream randomImageStream = new MemoryStream(randomImageBytes);

            return Task.FromResult(randomImageStream);
        }
    }
}
