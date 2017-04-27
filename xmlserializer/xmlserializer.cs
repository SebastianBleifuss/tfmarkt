using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.models;

namespace xmlserializer
{

    public class xmlserializer
    {
        public static readonly string DATASTORAGEPATH = "..\\datastorage";

        public static void serialize(Customer Customer)
        { 
        
        }

        public static void serialize<T>(T Product) where T : IProducts
        {

        }

        public static Customer deserialize(String Customername)
        {
            return null;
        }

        public static List<IProducts> deserializeAllProducts()
        {
            //DATASTORAGEPATH + "\\products\\products.xml"
            return null;
        }
    }
}
