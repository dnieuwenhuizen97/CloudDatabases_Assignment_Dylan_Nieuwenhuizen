using Domains;
using Domains.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<FinancialInformationDTO> UpdateCustomerFinancialInfo(string customerId, double salary);
        Task CalculateCustomersMortgageOffers();
    }
}
