using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Tasks;
using RoastedMarketplace.Data.Entity.ScheduledTasks;
using RoastedMarketplace.Services.Logger;

namespace RoastedMarketplace.Services.ScheduledTasks
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

        public void Start(Type[] availableTasksTypes)
        {
            /*
            //only start tasks if we have db installed
            if (!DatabaseManager.IsDatabaseInstalled())
                return;
            List<ScheduledTask> scheduledTasks;
            using (new OfflineDatabaseContextProvider())
            {
                //because dbcontext resolves per web request and the per request scope hasn't yet started here,
                //we'll have to fake open it, otherwise database context won't be resolved
                //first let's update 

                //get the scheduled tasks which are enabled
                scheduledTasks = _scheduledTaskService.Get().ToList();
                //first sync available tasks and database tasks
                UpdateTasksInDatabase(availableTasksTypes, scheduledTasks);
                if (!scheduledTasks.Any(x => x.Enabled))
                    return;
            }
            var registry = new Registry();
                //add each enabled task to the scheduler
                foreach (var sTask in scheduledTasks.Where(x => x.Enabled))
                {
                    var taskType = availableTasksTypes.FirstOrDefault(x => x.FullName == sTask.SystemName);
                    if (taskType != null)
                    {
                        if (sTask.Seconds < 30)
                            sTask.Seconds = 30;
                        //schedule the task
                        registry.Schedule(() =>
                        {
                            using (new OfflineDatabaseContextProvider())
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
                                    _logger.LogError<ScheduledTask>(ex, null, "Failed to complete the task '{0}'", task.SystemName);
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
                JobManager.Initialize(registry);*/
            
        }

        private void UpdateTasksInDatabase(Type[] allTaskTypes, IList<ScheduledTask> availableScheduledTasks)
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