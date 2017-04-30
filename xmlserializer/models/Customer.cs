using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace xmlserializer.Models
{
    /// <summary>
    /// Abstract class defines all customers
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customers name
        /// </summary>
        public String Name;

        /// <summary>
        /// Customernumber unique define customer
        /// </summary>
        public Guid Customernumber;

        /// <summary>
        /// Dictionary with Identifier|Calculations
        /// </summary>
        public Dictionary<Guid, Calculation> Calculations;

        private static Dictionary<Guid, String> Customers = new Dictionary<Guid, string>();

        public Customer(Guid Customernumber)
        {
            Calculations = new Dictionary<Guid, Calculation>();
        }
        public Customer() {
            Customernumber = getNewUniqueGuid();
        }

        public void addCalculation(Calculation calc, Guid Identifier)
        {
            this.Calculations.Add(Identifier, calc);
        }

        public bool calculationExists(Guid Identifier)
        {
            Calculation tmp;
            return this.Calculations.TryGetValue(Identifier, out tmp);
        }

        private Guid getNewUniqueGuid() {
            Guid NewGuid = Guid.NewGuid();
            String tmp;
            while (Customers.TryGetValue(NewGuid, out tmp))
            {
                NewGuid = Guid.NewGuid();
            }
            return NewGuid;
        }

        /// <summary>
        /// Get all customers name located in datastorage
        /// </summary>
        /// <returns>List of customer names</returns>
        public static List<String> getCustomerNames() {
        List<String> CustomerNames = new List<string>();
        foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customers")) { //Get all xml files from defined directory
                String CustomerInfo = Name.Replace(".xml", "").Replace("..\\datastorage\\customers\\", "");
                CustomerNames.Add(CustomerInfo);
                String[] CustomerInfoSet = CustomerInfo.Split('_');
                Customers.Add(new Guid(CustomerInfoSet[0]), CustomerInfoSet[1] + "_" + CustomerInfoSet[2]);
        }
            

            return CustomerNames;
        }
    }
}
