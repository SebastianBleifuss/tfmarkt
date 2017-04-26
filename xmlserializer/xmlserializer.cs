using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer
{

    public class xmlserializer
    {
        public static const string DATASTORAGEPATH = "..\\datastorage";

        public static void serialize(Customer Customer)
        { 
        
        }

        public static void serialize<T>(T Product) where T : Products
        {

        }

        public static Customer deserialize(String Customername)
        {
            return null;
        }

        public static List<Products> deserializeAllProducts()
        {
            return null;
        }
    }
}
