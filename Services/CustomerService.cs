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
        public IEmailService EmailService { get; }
        private CustomerDb CustomerDb { get; }

        public CustomerService(IStorageService storageService, IEmailService emailService, CustomerDb customerDb)
        {
            StorageService = storageService;
            this.EmailService = emailService;
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

                Console.WriteLine("Time available: " + customer.MortgageOffers.TimeAvailable.ToString());
            }

            await CustomerDb.UpdateCustomersMortgageOffers(customers);
        }

        public async Task AddCustomersToQueue()
        {
            List<Customer> customers = await CustomerDb.FindCustomersWithFinancialInformation();

            foreach (Customer c in customers)
                await StorageService.AddMessagesToQueue(c.CustomerId);
        }

        public async Task SendMortgageEmailToCustomer(string customerId)
        {
            Customer customer = await CustomerDb.FindCustomerMortgageOffers(customerId);

            CustomerDTO customerDTO = CustomerHelper.ToDTO(customer);

            await StorageService.AddMortgageOfferToBlob(customerDTO, customerId);

            string blobUrl = await StorageService.GetMortgageOfferUrl(customerId);

            await EmailService.SendMortgageEmail(customer.EmailAddress, blobUrl);
        }
    }
}
