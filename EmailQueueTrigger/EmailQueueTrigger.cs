using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace EmailQueueTrigger
{
    public class EmailQueueTrigger
    {
        private ICustomerService CustomerService { get; }

        public EmailQueueTrigger(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [Function(nameof(SendMortgageOfferToCustomers))]
        public async Task SendMortgageOfferToCustomers([QueueTrigger("customer-email-queue", Connection = "AzureWebJobsStorage")] string QueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# Queue trigger function processed: {QueueItem}");

            CustomerService.SendMortgageEmailToCustomer(QueueItem);
        }
    }
}
