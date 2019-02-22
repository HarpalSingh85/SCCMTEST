using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMTEST1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Objects  
            WQLConnector connector = new WQLConnector();

            // Setup a connection to the SMS Provider.  
            // Passing in <server name>, <domain\\account>, <password>.   
            WqlConnectionManager WMIConnection = connector.Connect("vsvaasryapp2005", "vaa\\asinghh8", "Monday@12345678");

            // List all packages (instances of SMS_Package).  
            GetMembers.EnumerateCollectionMembers(WMIConnection);
            //connector.ListPackages(WMIConnection);

            // Create a new package.  
            // Note: This is not a useful package (too few properties), just a demonstration of creating a Configuration Manager object.  
            //connector.CreatePackage(WMIConnection, "New Package", "This is the new package.");

            // Modifies a specific package (instance of SMS_Package).  
            // A valid PackageID needs to be passed to the ModifyPackage method - replace "ABC00000".  
            //BasicCMAppSnippets.ModifyPackage(WMIConnection, "ABC00000");  

            // Deletes a specific package (instance of SMS_Package).  
            // A valid PackageID needs to be passed to the DeletePackage method - replace "ABC00000".  
            //BasicCMAppSnippets.DeletePackage(WMIConnection, "ABC00000");  

            // Delay to keep the console output visible.  
            Console.ReadLine();
            
        }
    }
}
