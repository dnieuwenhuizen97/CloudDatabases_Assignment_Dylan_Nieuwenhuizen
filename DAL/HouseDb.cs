using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HouseDb
    {
        private readonly DatabaseContext _dbContext;

        public HouseDb(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<House>> FindHousesByPriceRange(double lowest, double highest)
        {
            List<House> houses = await _dbContext.Houses
                                            .AsQueryable()
                                            .Where(h => h.Price >= lowest && h.Price <= highest)
                                            .Include(h => h.Images)
                                            .ToListAsync();

            return houses;
        }

        public async Task<int> GetNumberOfHouseImagesById(string houseId)
        {
            House house = await _dbContext.Houses
                                            .Include(h => h.Images)
                                            .FirstOrDefaultAsync(h => h.HouseId == houseId);

            return house.Images.ToList().Count;
        }
    }
}
