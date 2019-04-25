using System;
using System.Linq;
using System.Threading.Tasks;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Tasks;
using EvenCart.Services.Emails;

namespace EvenCart.Infrastructure.Tasks
{
    public class EmailSchedulerTask : ITask
    {
        public void Dispose()
        {
            //do nothing else
        }

        public async Task Run()
        {
            //resolve email sender service
            var emailService = DependencyResolver.Resolve<IEmailService>();
            var emailMessages = emailService.Get(x => !x.IsSent && x.SendingDate <= DateTime.UtcNow).ToList();

            foreach (var message in emailMessages)
                await emailService.SendEmailAsync(message);
        }

        public string SystemName => "EvenCart.Infrastructure.Tasks.EmailSchedulerTask";
        public string Name => "Email Scheduler";
        public int DefaultCycleDurationInSeconds => 30; //every 1/2 minute
    }
}