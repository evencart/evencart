#region Author Information
// TicketField.cs
// 
// 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using EvenCart.Core.Data;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.ExtraFields
{
    public class ExtraField : FoundationEntity, ISoftDeletable
    {
        public string EntityName { get; set; }

        public string LabelBackOffice { get; set; }

        public string LabelFrontEnd { get; set; }

        public string Description { get; set; }

        public InputFieldType FieldType { get; set; }

        public bool VisibleToAgents { get; set; }

        public bool RequiredForAgents { get; set; }

        public bool VisibleToUsers { get; set; }

        public bool RequiredForUsers { get; set; }

        public bool IsUserEditable { get; set; }

        public string DefaultValue { get; set; }

        public int? ParentFieldId { get; set; }

        public virtual ExtraField ParentField { get; set; }

        public string ParentFieldValue { get; set; }

        public string FieldGeneratorMarkup { get; set; }

        public int DisplayOrder { get; set; }

        public bool System { get; set; }

        public bool Deleted { get; set; }

        public string MinimumValue { get; set; }

        public string MaximumValue { get; set; }

        public string AvailableValues { get; set; }
    }
}