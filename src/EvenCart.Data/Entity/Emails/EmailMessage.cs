using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using EvenCart.Core.Data;
using Newtonsoft.Json;

namespace EvenCart.Data.Entity.Emails
{
    /// <summary>
    /// Specifies fields used for sending emails
    /// </summary>
    public class EmailMessage : FoundationEntity
    {
        public string TosSerialized
        {
            get => JsonConvert.SerializeObject(Tos);
            set => Tos = JsonConvert.DeserializeObject<IList<UserInfo>>(value);
        } 

        public string CcsSerialized
        {
            get => JsonConvert.SerializeObject(Ccs);
            set => Ccs = JsonConvert.DeserializeObject<IList<UserInfo>>(value);
        }

        public string BccsSerialized
        {
            get => JsonConvert.SerializeObject(Bccs);
            set => Bccs = JsonConvert.DeserializeObject<IList<UserInfo>>(value);
        }

        public string ReplyTosSerialized
        {
            get => JsonConvert.SerializeObject(ReplyTos);
            set => ReplyTos = JsonConvert.DeserializeObject<IList<UserInfo>>(value);
        }

        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsEmailBodyHtml { get; set; }

        public string HeadersSerialized
        {
            get => JsonConvert.SerializeObject(Headers);
            set => Headers = JsonConvert.DeserializeObject<IDictionary<string, string>>(value);
        }

        private string _attachmentSerialized = string.Empty;

        public string AttachmentsSerialized
        {
            get => JsonConvert.SerializeObject(Attachments);
            set => _attachmentSerialized = value;
        }

        public int EmailAccountId { get; set; }

        public DateTime SendingDate { get; set; }

        public bool IsSent { get; set; }

        public void AddAttachment(string attachmentPath, string attachmentName = null)
        {
            var attachment = new Attachment(attachmentPath);
            attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentPath);
            attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentPath);
            attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentPath);
            if (!string.IsNullOrEmpty(attachmentName))
            {
                attachment.Name = attachmentName;
            }
            Attachments.Add(attachment);
        }

        public void AddAttachment(byte[] attachmentBytes, string attachmentName)
        {
            var ms = new MemoryStream(attachmentBytes);
            var attachment = new Attachment(ms, attachmentName);
            attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
            attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
            attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
            Attachments.Add(attachment);
        }

        /// <summary>
        /// Specifies a user in email communication
        /// </summary>
        public class UserInfo
        {
            public string Name { get; set; }

            public string Email { get; set; }

            public UserInfo(string name, string email)
            {
                Name = name;
                Email = email;
            }
        }

        #region Virtual Properties
        public virtual IList<UserInfo> Tos { get; set; }

        public virtual IList<UserInfo> Ccs { get; set; }

        public virtual IList<UserInfo> Bccs { get; set; }

        public virtual IList<UserInfo> ReplyTos { get; set; }

        public virtual IDictionary<string, string> Headers { get; set; }

        public virtual IList<Attachment> Attachments => string.IsNullOrEmpty(_attachmentSerialized)
            ? new List<Attachment>()
            : JsonConvert.DeserializeObject<List<Attachment>>(_attachmentSerialized);

        public virtual EmailTemplate OriginalEmailTemplate { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }
        #endregion
    }

}
