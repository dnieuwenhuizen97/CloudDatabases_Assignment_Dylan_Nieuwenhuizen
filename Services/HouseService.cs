using DAL;
using Domains;
using Domains.DTO;
using Domains.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HouseService : IHouseService
    {
        private HouseDb HouseDb { get; }

        public HouseService(HouseDb houseDb)
        {
            HouseDb = houseDb;
        }

        public async Task<List<HouseDTO>> GetHousesByPriceRange(double lowest, double highest)
        {
            List<House> houses = await HouseDb.FindHousesByPriceRange(lowest, highest);

            return HouseHelper.ListToDTO(houses);
        }
    }
}
