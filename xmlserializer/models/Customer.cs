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
    public class Customer
    {
        public String Name;
        public Guid Customernumber = Guid.NewGuid();
        public Dictionary<Guid,Calculation> Calculations = new Dictionary<Guid, Calculation>();


        public static List<String> getCustomerNames() {
        List<String> CustomerNames = new List<string>();
        foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customers")) {
            CustomerNames.Add(Name.Replace(".xml", "").Replace("_",", ").Replace("..\\datastorage\\customers\\",""));
        }
        return CustomerNames;
        }
    }
}
