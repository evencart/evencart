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