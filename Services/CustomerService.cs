using DAL;
using Domains;
using Domains.DTO;
using Domains.Helpers;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private CustomerDb CustomerDb { get; }

        public CustomerService(CustomerDb customerDb)
        {
            CustomerDb = customerDb;
        }

        public async Task<FinancialInformationDTO> UpdateCustomerFinancialInfo(string customerId, double salary)
        {
            FinancialInformation financialInformation = await CustomerDb.UpdateFinancialInformation(customerId, salary);

            return FinancialInformationHelper.ToDTO(financialInformation);
        }
    }
}
