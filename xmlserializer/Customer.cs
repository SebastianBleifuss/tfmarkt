using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer
{
    public class Customer
    {

        public static List<String> getCustomerNames() {
        List<String> CustomerNames = new List<string>();
        foreach (String Name in Directory.GetFiles(xmlserializer.DATASTORAGEPATH + "\\customer")) {
            CustomerNames.Add(Name.Replace(".xml", ""));
        }
        return CustomerNames;
        }
    }
}
