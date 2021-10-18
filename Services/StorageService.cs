using Domains.DTO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StorageService : IStorageService
    {
        public async Task AddMessagesToQueue(string userId)
        {
            CloudStorageAccount storageAccount = await GetStorageAccount();

            CloudQueueClient client = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = client.GetQueueReference("customer-email-queue");
            await queue.CreateIfNotExistsAsync();

            await queue.AddMessageAsync(new CloudQueueMessage(userId));
        }

        public async Task AddMortgageOfferToBlob(CustomerDTO customer, string customerId)
        {
            CloudStorageAccount storageAccount = await GetStorageAccount();

            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("mortgage-offer-blob");
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(customerId);

            blockBlob.Properties.ContentType = "application/json";
            await blockBlob.UploadTextAsync($"Hello {customer.FirstName} {customer.LastName}, Based on your annual salary of {customer.AnnnualSalary.ToString("€ 0.00")}, you are able to borrow a maximum amount of {customer.MortgageOffer.ToString("€ 0.00")}.");
        }

        public async Task<string> GetMortgageOfferUrl(string customerId)
        {
            CloudStorageAccount storageAccount = await GetStorageAccount();

            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("mortgage-offer-blob");
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(customerId);

            return blockBlob.Uri.ToString();
        }

        public async Task<CloudStorageAccount> GetStorageAccount()
        {
            return CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
        }
    }
}
