using System;

namespace EvenCart.Core.Infrastructure.Attributes
{
    /// <summary>
    /// Specifies that current field can be patched with patch request
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PatchableAttribute : Attribute
    {
        /// <summary>
        /// The role names which are allowed to patch
        /// </summary>
        public string[] RoleNames { get; set; }
        /// <summary>
        /// Creates a non patchable attribute to identify fields which can be patched by any user
        /// </summary>
        public PatchableAttribute()
        {
            RoleNames = new string[] {};
        }
        /// <summary>
        /// Creates a non patchable attribute to identify fields which can be patched by the users with specified roles
        /// </summary>
        /// <param name="roleNames"></param>
        public PatchableAttribute(params string[] roleNames)
        {
            RoleNames = roleNames;
        }

    }
}