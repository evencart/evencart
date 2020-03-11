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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.ScheduledTasks
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