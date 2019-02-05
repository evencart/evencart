using System;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.ScheduledTasks
{
    public class ScheduledTaskModel : FoundationEntityModel, IRequiresValidations<ScheduledTaskModel>
    {
        public string Name { get; set; }

        public int Seconds { get; set; }

        public string SystemName { get; set; }

        public bool Enabled { get; set; }

        public bool IsRunning { get; set; }

        public DateTime? LastStartDateTime { get; set; }

        public DateTime? LastEndDateTime { get; set; }

        public DateTime? LastSuccessDateTime { get; set; }

        public bool StopOnError { get; set; }

        public void SetupValidationRules(ModelValidator<ScheduledTaskModel> v)
        {
            v.RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            v.RuleFor(x => x.Seconds).NotEmpty().GreaterThanOrEqualTo(10);
        }
    }
}