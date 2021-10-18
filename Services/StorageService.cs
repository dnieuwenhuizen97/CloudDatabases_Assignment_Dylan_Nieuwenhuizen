using Microsoft.WindowsAzure.Storage;
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

            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference("customer-email-queue");
            await queue.CreateIfNotExistsAsync();

            await queue.AddMessageAsync(new CloudQueueMessage(userId));
        }

        public async Task<CloudStorageAccount> GetStorageAccount()
        {
            return CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
        }
    }
}
