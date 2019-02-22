using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMTEST1
{
    class AddToCollection
    {

        public int AddNewComputer( WqlConnectionManager connection, string netBiosName, string smBiosGuid,string macAddress)
        {
            try
            {
                if (smBiosGuid == null && macAddress == null)
                {
                    throw new ArgumentNullException("smBiosGuid or macAddress must be defined");
                }

                // Reformat macAddress to : separator.
                if (string.IsNullOrEmpty(macAddress) == false)
                {
                    macAddress = macAddress.Replace("-", ":");
                }

                // Create the computer.
                Dictionary<string, object> inParams = new Dictionary<string, object>();
                inParams.Add("NetbiosName", netBiosName);
                inParams.Add("SMBIOSGUID", smBiosGuid);
                inParams.Add("MACAddress", macAddress);
                inParams.Add("OverwriteExistingRecord", false);

                IResultObject outParams = connection.ExecuteMethod("SMS_Site","ImportMachineEntry",inParams);

                // Add to All System collection.
                IResultObject collection = connection.GetInstance("SMS_Collection.collectionId='ABC0000A'");
                IResultObject collectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect");
                collectionRule["ResourceClassName"].StringValue = "SMS_R_System";
                collectionRule["ResourceID"].IntegerValue = outParams["ResourceID"].IntegerValue;

                Dictionary<string, object> inParams2 = new Dictionary<string, object>();
                inParams2.Add("collectionRule", collectionRule);

                collection.ExecuteMethod("AddMembershipRule", inParams2);

                return outParams["ResourceID"].IntegerValue;
            }
            catch (SmsException e)
            {
                Console.WriteLine("failed to add the computer" + e.Message);
                throw;
            }
        }
    }
}
