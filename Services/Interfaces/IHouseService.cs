using Domains.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IHouseService
    {
        Task<List<HouseDTO>> GetHousesByPriceRange(double lowest, double highest);
        Task<string> UploadHouseImage(string houseId);
    }
}
