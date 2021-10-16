using Domains.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Helpers
{
    public static class CustomerHelper
    {
        public static CustomerDTO ToDTO(Customer customer)
        {
            return new CustomerDTO
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AnnnualSalary = customer.FinancialInformation.AnnualSalary,
                MortgageOffer = customer.MortgageOffers.MaxAmountToBorrow
            };
        }
    }
}
