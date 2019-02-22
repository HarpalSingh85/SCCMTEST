using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMTEST1
{
    class WQLConnector
    {
        public WqlConnectionManager Connect(string serverName, string userName, string userPassword)
        {
            try
            {
                SmsNamedValuesDictionary namedValues = new SmsNamedValuesDictionary();
                WqlConnectionManager connection = new WqlConnectionManager(namedValues);
                if (System.Net.Dns.GetHostName().ToUpper() == serverName.ToUpper())
                {
                    connection.Connect(serverName);
                }
                else
                {
                    connection.Connect(serverName, userName, userPassword);
                }
                return connection;
            }
            catch (SmsException ex)
            {
                Console.WriteLine("Failed to connect. Error: " + ex.Message);
                return null;

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Failed to authenticate. Error:" + ex.Message);
                throw;
            }
        }

        public void ListPackages(WqlConnectionManager connection)
        {
            try
            {
                // This query selects all packages (instances of SMS_Package).  
                string query = "SELECT * FROM SMS_Package";

                // Run query, which populates 'listOfPackages' with a collection of package objects.   
                IResultObject listOfPackages = connection.QueryProcessor.ExecuteQuery(query);

                // Output header for list of distribution points.  
                Console.WriteLine(" ");
                Console.WriteLine("List of packages:  ");
                Console.WriteLine("-------------------");

                // Enumerate through the collection of objects returned by the query. 
                int count = 0;
                foreach (IResultObject package in listOfPackages)
                {
                    // Output the package name for each package object.  
                    Console.WriteLine("Package ID: {0} Package Name: {1}", package["PackageID"].StringValue, package["Name"].StringValue);
                    count++;
                }
                Console.WriteLine("-------------------");
                Console.WriteLine($"Total Packages {count}");
            }
            catch (SmsException ex)
            {
                Console.WriteLine("Failed to list packages. Error: " + ex.Message);
                throw;
            }
        }
    }

}