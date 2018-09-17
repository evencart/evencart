using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{

    public class RoleCapabilityService : FoundationEntityService<RoleCapability>, IRoleCapabilityService
    {

       
        public IList<Capability> GetRoleCapabilities(string roleSystemName)
        {
            throw new System.NotImplementedException();
        }

        public IList<Capability> GetRoleCapabilities(int roleId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Capability> GetConsolidatedCapabilities(int[] roleIds)
        {
            throw new System.NotImplementedException();
        }

        public IList<Capability> GetConsolidatedCapabilities(string[] roleSystemNames)
        {
            throw new System.NotImplementedException();
        }

        public void SetRoleCapabilities(int roleId, int[] capabilityIds, bool deleteOtherEntries = true)
        {
            /*
            List<int> allCapabilities = null;
            if (deleteOtherEntries)
            {
                //get all capabilities of role
                allCapabilities = Get(x => x.RoleId == roleId).Select(x => x.CapabilityId).ToList();

                //find all to delete
                var toDelete = allCapabilities.Except(capabilityIds).ToList();

                //delete all these
                Delete(x => toDelete.Contains(x.CapabilityId));
                //and remove them from all list
                allCapabilities.RemoveAll(x => toDelete.Contains(x));
            }

            //find the ones to insert now
            var toInsert = deleteOtherEntries ? capabilityIds.Except(allCapabilities) : capabilityIds;

            //insert them all
            foreach(var insertCapabilityId in toInsert)
                Insert(new RoleCapability()
                {
                    RoleId = roleId,
                    CapabilityId = insertCapabilityId
                });
                */

        }

       
    }
}