using System;

namespace EvenCart.Core.Infrastructure.Attributes
{
    /// <summary>
    /// Specifies that current field shouldn't be patched with patch request
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NonPatchableAttribute : Attribute
    {
        /// <summary>
        /// The role names which are not allowed to patch
        /// </summary>
        public string[] RoleNames { get; set; }
        /// <summary>
        /// Creates a non patchable attribute to identify fields which shouldn't be patched by any user
        /// </summary>
        public NonPatchableAttribute()
        {
            RoleNames = new string[] {};
        }
        /// <summary>
        /// Creates a non patchable attribute to identify fields which can not be patched by the users with specified roles
        /// </summary>
        /// <param name="roleNames"></param>
        public NonPatchableAttribute(params string[] roleNames)
        {
            RoleNames = roleNames;
        }

    }
}