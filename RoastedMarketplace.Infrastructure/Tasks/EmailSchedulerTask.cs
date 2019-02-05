using System;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Tasks;
using RoastedMarketplace.Services.Emails;

namespace RoastedMarketplace.Infrastructure.Tasks
{
    public class EmailSchedulerTask : ITask
    {
        public void Dispose()
        {
            //do nothing else
        }

        public void Run()
        {
            //resolve email sender service
            var emailService = DependencyResolver.Resolve<IEmailService>();
            var emailMessages = emailService.Get(x => !x.IsSent && x.SendingDate <= DateTime.UtcNow).ToList();

            foreach (var message in emailMessages)
                emailService.SendEmail(message);
        }

        public string SystemName => "RoastedMarketplace.Infrastructure.Tasks.EmailSchedulerTask";
        public string Name => "Email Scheduler";
        public int DefaultCycleDurationInSeconds => 30; //every 1/2 minute
    }
}