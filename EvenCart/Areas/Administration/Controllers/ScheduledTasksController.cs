using System.Linq;
using EvenCart.Areas.Administration.Models.ScheduledTasks;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Services.ScheduledTasks;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
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