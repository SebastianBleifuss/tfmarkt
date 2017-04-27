using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.models;

namespace xmlserializer.models
{
    public class Customer
    {
        public String Name;
        public List<Calculation> Calculations;


        public static List<String> getCustomerNames() {
        List<String> CustomerNames = new List<string>();
        foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customers")) {
            CustomerNames.Add(Name.Replace(".xml", "").Replace("_",", ").Replace("..\\datastorage\\customers\\",""));
        }
        return CustomerNames;
        }
    }
}
