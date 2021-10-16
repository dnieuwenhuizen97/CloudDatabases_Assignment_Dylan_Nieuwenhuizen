using Domains.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Helpers
{
    public static class FinancialInformationHelper
    {
        public static FinancialInformationDTO ToDTO(FinancialInformation financialInformation)
        {
            return new FinancialInformationDTO
            {
                CustomerId = financialInformation.CustomerId,
                AnnualSalary = financialInformation.AnnualSalary
            };
        }
    }
}
