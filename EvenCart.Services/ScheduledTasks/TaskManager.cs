using System;
using System.Collections.Generic;
using System.Linq;
using FluentScheduler;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Tasks;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Services.Extensions;
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
            var availableTasksTypes = TypeFinder.ClassesOfType<ITask>();
            var scheduledTasks = _scheduledTaskService.Get(x => true).ToList();
            //first sync available tasks and database tasks
            UpdateTasksInDatabase(availableTasksTypes, scheduledTasks);
            if (!scheduledTasks.Any(x => x.Enabled))
                return;
            var registry = new Registry();
                //add each enabled task to the scheduler
                foreach (var sTask in scheduledTasks.Where(x => x.Enabled))
                {
                    var taskType = availableTasksTypes.FirstOrDefault(x => x.FullName == sTask.SystemName);
                    if (taskType != null)
                    {
                        if (sTask.Seconds < 10)
                            sTask.Seconds = 10;
                        //schedule the task
                        registry.Schedule(() =>
                        {
                            using (var task = (ITask)Activator.CreateInstance(taskType))
                            {
                                try
                                {
                                    sTask.LastStartDateTime = DateTime.UtcNow;
                                    sTask.IsRunning = true;
                                    _scheduledTaskService.Update(sTask);
                                    task.Run();
                                    sTask.LastSuccessDateTime = DateTime.UtcNow;
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError<ScheduledTask>(ex, "Failed to complete the task '{0}'", null, task.SystemName);
                                    //check if task should be stopped
                                    if (sTask.StopOnError)
                                    {
                                        sTask.Enabled = false;
                                        sTask.LastEndDateTime = DateTime.UtcNow;
                                    }
                                }
                                finally
                                {
                                    sTask.IsRunning = false;
                                    //update the task
                                    _scheduledTaskService.Update(sTask);
                                }
                            }

                        }).ToRunEvery(sTask.Seconds).Seconds();

                    }
                }
                //initialize the jobmanager
                JobManager.Initialize(registry);
            
        }

        private void UpdateTasksInDatabase(IList<Type> allTaskTypes, IList<ScheduledTask> availableScheduledTasks)
        {
            var taskNames = allTaskTypes.Select(x => x.FullName).ToArray();
            foreach (var taskName in taskNames)
            {
                var iTask = (ITask)Activator.CreateInstance(allTaskTypes.First(x => x.FullName == taskName));
                if (availableScheduledTasks.Any(x => x.SystemName == taskName))
                {
                    var t = availableScheduledTasks.First(x => x.SystemName == taskName);
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
                    SystemName = taskName,
                    StopOnError = false,
                    Seconds = iTask.DefaultCycleDurationInSeconds
                };
                _scheduledTaskService.Insert(st);
                availableScheduledTasks.Add(st);
            }
        }
    }
}