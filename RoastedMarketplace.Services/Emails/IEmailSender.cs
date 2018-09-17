using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Tickets;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Emails
{
    public interface IEmailSender
    {
        bool SendTestEmail(string email, EmailAccount emailAccount);

        void SendUserRegisteredMessage(User user);

        void SendUserActivationLinkMessage(User user, string activationUrl);

        void SendUserActivatedMessage(User user);

        void SendTicketCreatedMessage(User user, Ticket ticket, bool agentCreatedTicket = false);

        void SendTicketUpdatedMessage(User user, Ticket ticket);

        void SendTicketClosedMessage(User user, Ticket ticket);

        void SendTicketResolvedMessage(User user, Ticket ticket);

        void SendTicketDeletedMessage(User user, Ticket ticket);

        void SendSlaViolationReminderMessage(User user, SlaPolicy slaPolicy, Ticket ticket);

        void SendSlaViolatedMessage(User user, SlaPolicy slaPolicy, Ticket ticket);

        void SendSlaModifiedMessage(User user, SlaPolicy slaPolicy);

        void SendSlaDeletedMessage(User user, SlaPolicy slaPolicy);

    }
}