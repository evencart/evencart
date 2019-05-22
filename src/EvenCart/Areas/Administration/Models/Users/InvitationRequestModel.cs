using System;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class InvitationRequestModel : FoundationEntityModel
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
        /// Was the request accepted previously
        /// </summary>
        public bool Accepted { get; set; }
    }
}