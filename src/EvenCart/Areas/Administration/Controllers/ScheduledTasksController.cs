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
using EvenCart.Areas.Administration.Models.ScheduledTasks;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Tasks;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Data.Extensions;
using EvenCart.Services.ScheduledTasks;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Helpers;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class ScheduledTasksController : FoundationAdminController
    {
        private readonly IScheduledTaskService _scheduledTaskService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly ILogger _logger;
        public ScheduledTasksController(IScheduledTaskService scheduledTaskService, IModelMapper modelMapper, IDataSerializer dataSerializer, ILogger logger)
        {
            _scheduledTaskService = scheduledTaskService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _logger = logger;
        }

        [DualGet("", Name = AdminRouteNames.ScheduledTasksList)]
        public IActionResult ScheduledTasksList(ScheduledTaskSearchModel searchModel)
        {
            var tasks = _scheduledTaskService.GetScheduledTasks(out int totalMatches, searchModel.SearchPhrase,
                searchModel.EnableStatus, searchModel.Current, searchModel.RowCount);
            var models = tasks.Select(x => _modelMapper.Map<ScheduledTaskModel>(x)).ToList();
            return R.Success.With("tasks", models)
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .WithParams(searchModel)
                .Result;
        }

        [DualGet("{scheduledTaskId}", Name = AdminRouteNames.GetScheduledTask)]
        public IActionResult ScheduledTaskEditor(int scheduledTaskId)
        {
            ScheduledTask task = null;
            if (scheduledTaskId < 1 || (task = _scheduledTaskService.Get(scheduledTaskId)) == null)
                return NotFound();

            var model = _modelMapper.Map<ScheduledTaskModel>(task);
            return R.Success.With("task", model).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveScheduledTask, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(ScheduledTaskModel))]
        public IActionResult SaveScheduledTask(ScheduledTaskModel taskModel)
        {
            // find the task
            var task = _scheduledTaskService.Get(taskModel.Id);
            if (task == null)
                return NotFound();
            task.Enabled = taskModel.Enabled;
            task.StopOnError = taskModel.StopOnError;
            task.Seconds = taskModel.Seconds;
            _scheduledTaskService.Update(task);
            return R.Success.Result;
        }

        [DualPost("run", Name = AdminRouteNames.RunScheduledTask, OnlyApi = true)]
        public IActionResult RunNow(string scheduledTaskSystemName)
        {
            if (scheduledTaskSystemName.IsNullEmptyOrWhiteSpace())
                return BadRequest();
            var scheduledTask = _scheduledTaskService.FirstOrDefault(x => x.SystemName == scheduledTaskSystemName);
            var iTask = DependencyResolver.Resolve<ITask>(scheduledTaskSystemName);
            try
            {
                ScheduledTaskHelper.RunScheduledTask(scheduledTask, iTask, _scheduledTaskService, _logger, false);
            }
            catch (Exception)
            {
                return R.Fail.With("error",
                    T("An error occurred while running the task. Please check the log for details")).Result;
            }
            return R.Success.Result;
        }
    }
}