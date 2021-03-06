using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace MortgageTimer
{
    public class CustomerEmailTimerTrigger
    {
        ICustomerService CustomerService { get; }

        public CustomerEmailTimerTrigger(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [Function(nameof(SendMortgageOfferToQueue))]
        public async Task SendMortgageOfferToQueue([TimerTrigger("0 0 08 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(SendMortgageOfferToQueue));
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            await CustomerService.AddCustomersToQueue();
        }
    }
}
