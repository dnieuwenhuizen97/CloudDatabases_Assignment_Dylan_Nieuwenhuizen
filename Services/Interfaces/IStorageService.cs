using Domains.DTO;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStorageService
    {
        Task<CloudStorageAccount> GetStorageAccount();
        Task AddMortgageOfferToBlob(CustomerDTO customer, string customerId);
        Task<string> GetMortgageOfferUrl(string customerId);
        Task AddMessagesToQueue(string userId);
    }
}
