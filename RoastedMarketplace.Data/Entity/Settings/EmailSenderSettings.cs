#region Author Information
// EmailSenderSettings.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class EmailSenderSettings : ISettingGroup
    {
        public bool UserRegisteredEmailEnabled { get; set; }

        public bool UserRegisteredEmailToAdminEnabled { get; set; }

        public bool UserActivationEmailEnabled { get; set; }

        public bool PasswordChangedEmailEnabled { get; set; }

        public bool UserDeactivationEmailEnabled { get; set; }

        public bool UserDeactivationEmailToAdminEnabled { get; set; }

        public bool UserDeletedEmailEnabled { get; set; }

        public bool UserDeletedEmailToAdminEnabled { get; set; }

        public bool TicketCreatedByUserEmailEnabled { get; set; }

        public bool TicketCreatedByUserEmailToAdminEnabled { get; set; }

        public bool TicketCreatedByAgentEmailEnabled { get; set; }

        public bool TicketCreatedByAgentEmailToAdminEnabled { get; set; }

        public bool TicketUpdatedEmailEnabled { get; set; }

        public bool TicketUpdatedEmailToAdminEnabled { get; set; }

        public bool TicketClosedEmailEnabled { get; set; }

        public bool TicketClosedEmailToAdminEnabled { get; set; }

        public bool TicketResolvedEmailEnabled { get; set; }

        public bool TicketResolvedEmailToAdminEnabled { get; set; }

        public bool TicketDeletedEmailEnabled { get; set; }

        public bool TicketDeletedEmailToAdminEnabled { get; set; }

        public bool SlaViolatedEmailToAdminEnabled { get; set; }

        public bool SlaModifiedEmailToAdminEnabled { get; set; }

    }
}