using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xmlserializer.Models;
using tfMarktMain;
using System.Collections.Generic;
using xmlserializer.Models.Products;
using xmlserializer.Models.Calculations;
using System.IO;

namespace tfMarktUnitTest
{
    [TestClass]
    public class UnitTestSerializer
    {
        [TestMethod]
        public void LoadCustomer()
        {
            xmlserializer.xmlserializer.deserialize("Gonzales, SirPeter",new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        }

        [TestMethod]
        public void SaveCustomer()
        {
            Customer NewCustomer = new Customer(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"))
            {
                Name = "FooMan, Hans Joseph"
            };



            xmlserializer.Models.Products.Hilfsmittel Filler = new xmlserializer.Models.Products.Hilfsmittel(42, "Filler", 42m, 42m);
            xmlserializer.Models.Products.Hilfsmittel Glue = new xmlserializer.Models.Products.Hilfsmittel(42, "Glue", 42m, 42m);
            List<xmlserializer.Models.Products.Fliese> Fliesen = new List<xmlserializer.Models.Products.Fliese>();

            List<Product> alleProdukte = xmlserializer.xmlserializer.deserializeAllProducts();
            foreach (Product produkt in alleProdukte)
            {
                Type produktTyp = produkt.getProductType();
                if (produktTyp.Equals(typeof(Fliese)))
                {
                    Fliesen.Add((Fliese)produkt);
                }
            }


            xmlserializer.Models.Calculations.Fliesenkalkulation NewFliesenCalc = new xmlserializer.Models.Calculations.Fliesenkalkulation("Fliese grau", "UnitTestFliesenCalc", true, 42m,
                Fliesen,
                Filler,
                Glue
                )
            { ausgewaehlteFliese = new xmlserializer.Models.Products.Fliese(42, "Fliese", 42m, 42m, 42, 42m) };



            xmlserializer.Models.Calculations.Tapetenkalkulation NewTapetenCalc = new xmlserializer.Models.Calculations.Tapetenkalkulation() {
                Description = "UnitTestTapetenCalc",
                Identifier = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Length = 42,
                Width = 42,
                WithExtraProduct = true,
                tapete = new xmlserializer.Models.Products.Tapete(42, "Tapete", 42m, 42m, 42, 42m)
                
            };


            
            NewCustomer.addCalculation(NewFliesenCalc, true);
            NewCustomer.addCalculation(NewTapetenCalc, true);
            xmlserializer.xmlserializer.serialize(NewCustomer);
            Assert.AreEqual(true,File.Exists(xmlserializer.xmlserializer.DATASTORAGEPATH + "\\customers\\aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa_FooMan_Hans Joseph.xml"));
        }

        [TestMethod]
        public void LoadAllProducts()
        {
            Assert.AreEqual(Directory.GetFiles(xmlserializer.xmlserializer.DATASTORAGEPATH + "\\products").Length, xmlserializer.xmlserializer.deserializeAllProducts().Count);
        }

        [TestMethod]
        public void SaveProducts()
        {
            xmlserializer.xmlserializer.serialize(new xmlserializer.Models.Products.Fliese(42, "UnitTestFliese", 42m, 42m, 42, 42m));
        }
    }
}
