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
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    /// <summary>
    /// Represents a request to join the store
    /// </summary>
    public class InviteRequest : FoundationEntity
    {
        /// <summary>
        /// The email which sent the request
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The date of request
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// If the request has been accepted or not
        /// </summary>
        public bool Accepted { get; set; }
    }
}