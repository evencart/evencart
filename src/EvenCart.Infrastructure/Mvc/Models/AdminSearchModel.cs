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

using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Infrastructure.Mvc.Models
{
    /// <summary>
    /// The common search class that is used across administration queries
    /// </summary>
    public abstract class AdminSearchModel : FoundationModel, IRequiresValidations<AdminSearchModel>
    {
        /// <summary>
        /// The text to search within the query
        /// </summary>
        public string SearchPhrase { get; set; }

        /// <summary>
        /// The page being requested in a paginated request. Default is 1.
        /// </summary>
        public int Current { get; set; } = 1;

        /// <summary>
        /// The total number of result rows to be returned
        /// </summary>
        public int RowCount { get; set; } = 15;

        public void SetupValidationRules(ModelValidator<AdminSearchModel> v)
        {
            v.RuleFor(x => x.Current).GreaterThan(0);
            v.RuleFor(x => x.RowCount).GreaterThan(0);
        }
    }
}