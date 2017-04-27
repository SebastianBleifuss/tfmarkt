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
            if (File.Exists(DATASTORAGEPATH + "\\customers\\" + Customername.Replace(", ", "_") + ".xml")) {
                List<Calculation> ExampleCalcs = new List<Calculation>();
                ExampleCalcs.Add(new FooBarCalculation());
                return new Customer()
                {
                    Name = "example, customer",
                    Calculations = ExampleCalcs
                };
            }else{
            throw new FileNotFoundException();
            }
           
        }

        public static List<IProducts> deserializeAllProducts()
        {
            //DATASTORAGEPATH + "\\products\\products.xml"
            return null;
        }
    }
}
