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
        public Guid Customernumber = Guid.NewGuid();

        /// <summary>
        /// Dictionary with Identifier|Calculations
        /// </summary>
        public Dictionary<Guid,Calculation> Calculations = new Dictionary<Guid, Calculation>();

        /// <summary>
        /// Get all customers name located in datastorage
        /// </summary>
        /// <returns></returns>
        public static List<String> getCustomerNames() {
        List<String> CustomerNames = new List<string>();
        foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customers")) {
            CustomerNames.Add(Name.Replace(".xml", "").Replace("_",", ").Replace("..\\datastorage\\customers\\",""));
        }
        return CustomerNames;
        }
    }
}
