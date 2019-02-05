using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.ScheduledTasks;
using RoastedMarketplace.Data.Entity.ScheduledTasks;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.ScheduledTasks;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class ScheduledTasksController : FoundationAdminController
    {
        private readonly IScheduledTaskService _scheduledTaskService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public ScheduledTasksController(IScheduledTaskService scheduledTaskService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _scheduledTaskService = scheduledTaskService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        [DualGet("", Name = AdminRouteNames.ScheduledTasksList)]
        public IActionResult ScheduledTasksList(ScheduledTaskSearchModel searchModel)
        {
            var tasks = _scheduledTaskService.GeScheduledTasks(out int totalMatches, searchModel.SearchPhrase,
                searchModel.EnableStatus, searchModel.Current, searchModel.RowCount);
            var models = tasks.Select(x => _modelMapper.Map<ScheduledTaskModel>(x)).ToList();
            return R.Success.With("tasks", () => models, () => _dataSerializer.Serialize(models))
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
    }
}