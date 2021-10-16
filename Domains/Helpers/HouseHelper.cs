using Domains.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Helpers
{
    public static class HouseHelper
    {
        public static HouseDTO ToDTO(House house)
        {
            return new HouseDTO
            {
                Price = house.Price,
                Images = house.Images.ToList()
            };
        }

        public static List<HouseDTO> ListToDTO(List<House> houses)
        {
            List<HouseDTO> houseDTOs = new List<HouseDTO>();

            foreach (House house in houses)
            {
                houseDTOs.Add(ToDTO(house));
            }

            return houseDTOs;
        }
    }
}
