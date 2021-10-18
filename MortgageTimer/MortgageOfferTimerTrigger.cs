using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace MortgageTimer
{
    public class MortgageOfferTimerTrigger
    {
        ICustomerService CustomerService { get; }

        public MortgageOfferTimerTrigger(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [Function(nameof(CreateMortgageOfferTimed))]
        public async Task CreateMortgageOfferTimed([TimerTrigger("0 59 23 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(CreateMortgageOfferTimed));
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            await CustomerService.CalculateCustomersMortgageOffers();
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
