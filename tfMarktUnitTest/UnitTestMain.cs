using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xmlserializer.Models;
using xmlserializer.Models.Products;
using System.Collections.Generic;

namespace tfMarktUnitTest
{
    [TestClass]
    public class UnitTestMain
    {

        [TestMethod]
        public void PDFGenerieren()
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


            tfMarktMain.Fliesenkalkulation.Fliesenkalkulation NewFliesenCalc = new tfMarktMain.Fliesenkalkulation.Fliesenkalkulation("Fliese grau", true, 42m,
                Fliesen,
                Filler,
                Glue
                )
            { ausgewaehlteFliese = new xmlserializer.Models.Products.Fliese(42, "Fliese", 42m, 42m, 42, 42m) };

            Customer NewCustomer = new Customer(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"))
            {
                Name = "FooMan, Hans Joseph"
            };
            NewCustomer.addCalculation(NewFliesenCalc, true);

            tfMarktMain.Export.PDFFactory.CustomerPDFDocument PDF = new tfMarktMain.Export.PDFFactory.CustomerPDFDocument(NewCustomer);
            PDF.printPDF();
        }

       
    }
}
