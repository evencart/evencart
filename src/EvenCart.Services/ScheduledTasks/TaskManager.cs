using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using FluentScheduler;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Tasks;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Services.Extensions;
using EvenCart.Services.Helpers;
using EvenCart.Services.Logger;

namespace EvenCart.Services.ScheduledTasks
{
    public class TaskManager : ITaskManager
    {
        private readonly IScheduledTaskService _scheduledTaskService;
        private readonly ILogger _logger;
        public TaskManager(IScheduledTaskService scheduledTaskService, ILogger logger)
        {
            _scheduledTaskService = scheduledTaskService;
            _logger = logger;
        }

        public void Start()
        {
            //only start tasks if we have db installed
            if (!DatabaseManager.IsDatabaseInstalled())
                return;
            var availableTasks = DependencyResolver.ResolveMany<ITask>().ToList();
            var scheduledTasks = _scheduledTaskService.Get(x => true).ToList();
            //first sync available tasks and database tasks
            UpdateTasksInDatabase(availableTasks, scheduledTasks);
            if (!scheduledTasks.Any(x => x.Enabled))
                return;
            var registry = new Registry();
            //add each enabled task to the scheduler
            foreach (var sTask in scheduledTasks.Where(x => x.Enabled))
            {
                var task = availableTasks.FirstOrDefault(x => x.SystemName == sTask.SystemName);
                if (task == null)
                    continue;

                if (sTask.Seconds < 10)
                    sTask.Seconds = 10;
                //schedule the task
                registry.Schedule(() =>
                {
                    
                    try
                    {
                        ScheduledTaskHelper.RunScheduledTask(sTask, task, _scheduledTaskService, _logger);
                    }
                    catch (Exception ex)
                    {
                        //do nothing here
                    }

                }).ToRunEvery(sTask.Seconds).Seconds();
            }
            //initialize the jobmanager
            JobManager.Initialize(registry);

        }

        private void UpdateTasksInDatabase(IList<ITask> allTaskTypes, IList<ScheduledTask> availableScheduledTasks)
        {
            foreach (var iTask in allTaskTypes)
            {
                if (availableScheduledTasks.Any(x => x.SystemName == iTask.SystemName))
                {
                    var t = availableScheduledTasks.First(x => x.SystemName == iTask.SystemName);
                    if (t.Name != iTask.Name)
                    {
                        t.Name = iTask.Name;
                        _scheduledTaskService.Update(t);
                    }
                    continue; //it's there so no need to do anything except update name
                }

                //if we are here, we'll need to add the task
                var st = new ScheduledTask()
                {
                    Enabled = false,
                    IsRunning = false,
                    Name = iTask.Name,
                    SystemName = iTask.SystemName,
                    StopOnError = false,
                    Seconds = iTask.DefaultCycleDurationInSeconds
                };
                _scheduledTaskService.Insert(st);
                availableScheduledTasks.Add(st);
            }
        }
    }
}