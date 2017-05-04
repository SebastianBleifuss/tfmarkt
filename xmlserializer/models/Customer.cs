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
        public Guid Customernumber { get; private set; }

        /// <summary>
        /// Dictionary with Identifier|Calculations
        /// </summary>
        public Dictionary<Guid, Calculation> Calculations = new Dictionary<Guid, Calculation>();

        private static Dictionary<Guid, String> Customers = new Dictionary<Guid, string>();

        public Customer(Guid Customernumber)
        {
            this.Customernumber = Customernumber;
        }

        public Customer()
        {
            Customernumber = getNewUniqueGuid();
            Customers.Add(this.Customernumber, "initialised");
        }

        public void addCalculation(Calculation calc, bool Override)
        {
            if (calculationExists(calc.Identifier))
            {
                if (Override)
                {
                    replaceCalculation(calc);
                }
                else
                {
                    throw new InvalidOperationException("Calculation already exists!");
                }
            }
            else {
            this.Calculations.Add(calc.Identifier, calc);
            }           
        }

        private void replaceCalculation(Calculation calc) {
            Calculation calculationsaver;//Pretend losing the whole calculation
            
            if(this.Calculations.TryGetValue(calc.Identifier, out calculationsaver))
            {
                try {
                    this.Calculations.Remove(calc.Identifier);
                    this.Calculations.Add(calc.Identifier,calc);
                }
                catch (Exception ex) {
                    this.Calculations.Add(calculationsaver.Identifier, calculationsaver);
                    throw ex;
                }
            }
        }

        public bool calculationExists(Guid Identifier)
        {
            Calculation tmp;
            return this.Calculations.TryGetValue(Identifier, out tmp);
        }

        public static bool customerExists(Guid Identifier)
        {
            String tmp;
            return Customers.TryGetValue(Identifier, out tmp);
        }

        private Guid getNewUniqueGuid()
        {
            Guid NewGuid = Guid.NewGuid();
            String tmp;
            while (Customers.TryGetValue(NewGuid, out tmp))
            {
                NewGuid = Guid.NewGuid();
            }
            return NewGuid;
        }

        public static void removeCustomer(Customer c)
        {
            if (customerExists(c.Customernumber))
            {
                File.Delete(xmlserializer.DATASTORAGEPATH + "\\customers\\" + c.Customernumber + "_" + c.Name.Replace(", ", "_") + ".xml");
                Customers.Remove(c.Customernumber);
            }

        }

        /// <summary>
        /// Get all customers name located in datastorage
        /// </summary>
        /// <returns>List of customer names</returns>
        public static List<String> getCustomerNames()
        {
            List<String> CustomerNames = new List<string>();
            Customers.Clear();
            foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customers"))
            { //Get all xml files from defined directory
                String CustomerInfo = Name.Replace(".xml", "").Replace("..\\datastorage\\customers\\", "");
                CustomerNames.Add(CustomerInfo);
                String[] CustomerInfoSet = CustomerInfo.Split('_');
                Customers.Add(new Guid(CustomerInfoSet[0]), CustomerInfoSet[1] + "_" + CustomerInfoSet[2]);
            }


            return CustomerNames;
        }
    }
}
