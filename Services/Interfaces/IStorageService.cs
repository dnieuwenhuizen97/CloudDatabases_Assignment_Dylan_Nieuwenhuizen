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
        Task AddMessagesToQueue(string userId);
    }
}
