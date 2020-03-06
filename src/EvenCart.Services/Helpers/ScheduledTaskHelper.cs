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
using EvenCart.Core.Tasks;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using EvenCart.Services.ScheduledTasks;

namespace EvenCart.Services.Helpers
{
    public static class ScheduledTaskHelper
    {
        public static void RunScheduledTask(ScheduledTask sTask, ITask associatedTask, IScheduledTaskService scheduledTaskService, ILogger logger, bool autoRun = true)
        {
            try
            {
                sTask.LastStartDateTime = DateTime.UtcNow;
                sTask.IsRunning = true;
                scheduledTaskService.Update(sTask);
                associatedTask.Run();
                sTask.LastSuccessDateTime = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                sTask.LastEndDateTime = DateTime.UtcNow;
                logger.LogError<ScheduledTask>(ex, "Failed to complete the task '{0}'", null, associatedTask.SystemName);
                if (sTask.StopOnError && autoRun)
                {
                    sTask.Enabled = false;
                }
            }
            finally
            {
                sTask.IsRunning = false;
                //update the task
                scheduledTaskService.Update(sTask);
                //dispose the task
                associatedTask.Dispose();
            }
            
        }
    }
}