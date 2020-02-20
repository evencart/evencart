using System.Linq;
using EvenCart.Areas.Administration.Models.Logger;
using EvenCart.Data.Constants;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Allows to view and delete activity logs
    /// </summary>
    public class LogsController : FoundationAdminController
    {
        private readonly ILoggerEntityService _loggerEntityService;
        private readonly IModelMapper _modelMapper;
        public LogsController(ILoggerEntityService loggerEntityService, IModelMapper modelMapper)
        {
            _loggerEntityService = loggerEntityService;
            _modelMapper = modelMapper;
        }

        /// <summary>
        /// Gets the logs recorded in the system
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="LogModel">log</see> objects as 'logs'</response>
        [DualGet("", Name = AdminRouteNames.LogsList)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        [ValidateModelState(ModelType = typeof(AdminSearchModel))]
        public IActionResult LogsList(LogSearchModel searchModel)
        {
            var logs = _loggerEntityService.GetLogs(out var totalResults, searchModel.SearchPhrase, searchModel.Current,
                searchModel.RowCount);

            var models = logs.Select(x => _modelMapper.Map<LogModel>(x)).ToList();
            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("logs", models)
                .WithParams(searchModel)
                .Result;
        }

        /// <summary>
        /// Gets a single log entry
        /// </summary>
        /// <param name="logId">The id of the entry</param>
        /// <response code="200">A single <see cref="LogModel">log</see> object as 'log'</response>
        [DualGet("{logId}", Name = AdminRouteNames.GetLog)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult LogViewer(int logId)
        {
            var log = logId > 0 ? _loggerEntityService.Get(logId) : null;
            if (log == null)
                return NotFound();
            var logModel = _modelMapper.Map<LogModel>(log);
            return R.Success.With("log", logModel).Result;
        }

        /// <summary>
        /// Deletes a specific log entry from the system
        /// </summary>
        /// <param name="logId">The id of the log</param>
        /// <response code="200">A success response object.</response>
        [DualPost("", Name = AdminRouteNames.DeleteLog, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult DeleteLog(int logId)
        {
            var log = logId > 0 ? _loggerEntityService.Get(logId) : null;
            if (log == null)
                return NotFound();
            _loggerEntityService.Delete(log);
            return R.Success.Result;
        }

        /// <summary>
        /// Clears all the logs from the system. WARNING: Use this only if you are sure what you are doing.
        /// </summary>
        /// <response code="200">A success response object.</response>
        [DualPost("clear", Name = AdminRouteNames.ClearLogs, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult ClearLogs()
        {
            _loggerEntityService.Delete(x => true);
            return R.Success.Result;
        }
    }
}