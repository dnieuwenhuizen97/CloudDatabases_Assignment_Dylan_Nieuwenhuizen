using DAL;
using Domains;
using Domains.DTO;
using Domains.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private IStorageService StorageService { get; }
        private CustomerDb CustomerDb { get; }

        public CustomerService(IStorageService storageService, CustomerDb customerDb)
        {
            StorageService = storageService;
            CustomerDb = customerDb;
        }

        public async Task<FinancialInformationDTO> UpdateCustomerFinancialInfo(string customerId, double salary)
        {
            FinancialInformation financialInformation = await CustomerDb.UpdateFinancialInformation(customerId, salary);

            return FinancialInformationHelper.ToDTO(financialInformation);
        }

        public async Task CalculateCustomersMortgageOffers()
        {
            List<Customer> customers = await CustomerDb.FindAllCustomers();

            foreach (Customer customer in customers)
            {
                double salary = customer.FinancialInformation.AnnualSalary;

                customer.MortgageOffers = new MortgageOffer(salary / 2 * 30);
            }

            await CustomerDb.UpdateCustomersMortgageOffers(customers);
        }

        public async Task AddCustomersToQueue()
        {
            List<Customer> customers = await CustomerDb.FindCustomersWithFinancialInformation();

            foreach (Customer c in customers)
                await StorageService.AddMessagesToQueue(c.CustomerId);
        }
    }
}
