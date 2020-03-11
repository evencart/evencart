#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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