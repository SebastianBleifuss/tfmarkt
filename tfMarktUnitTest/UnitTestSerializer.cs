using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xmlserializer.Models;
using tfMarktMain;
using System.Collections.Generic;
using xmlserializer.Models.Products;
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


            tfMarktMain.Fliesenkalkulation.Fliesenkalkulation NewFliesenCalc = new tfMarktMain.Fliesenkalkulation.Fliesenkalkulation("Fliese grau","saveCustomer" , true, 42m,
                Fliesen,
                Filler,
                Glue
                )
            { ausgewaehlteFliese = new xmlserializer.Models.Products.Fliese(42, "Fliese", 42m, 42m, 42, 42m) };

            Customer NewCustomer = new Customer(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")) {
                Name = "FooMan, Hans Joseph"
            };
            NewCustomer.addCalculation(NewFliesenCalc, true);
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
            xmlserializer.xmlserializer.serialize(new xmlserializer.Models.Products.Fliese(42, "Fliese", 42m, 42m, 42, 42m));
        }
    }
}
