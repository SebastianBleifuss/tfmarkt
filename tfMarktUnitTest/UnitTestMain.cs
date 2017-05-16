﻿using System;
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


            tfMarktMain.Fliesenkalkulation.Fliesenkalkulation NewFliesenCalc = new tfMarktMain.Fliesenkalkulation.Fliesenkalkulation("Fliese grau", "UnitTestFliesenCalc", true, 42m,
                Fliesen,
                Filler,
                Glue
                )
            { ausgewaehlteFliese = new xmlserializer.Models.Products.Fliese(42, "Fliese", 42m, 42m, 42, 42m) };



            tfMarktMain.Tapetenkalkulation.Tapetenkalkulation NewTepetenCalc = new tfMarktMain.Tapetenkalkulation.Tapetenkalkulation()
            {
                Description = "UnitTestTapetenCalc",
                Identifier = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Length = 42,
                Width = 42,
                WithExtraProduct = true,
                SelectedProduct = new xmlserializer.Models.Products.Tapete(42, "Tapete", 42m, 42m, 42, 42m)
            };



            NewCustomer.addCalculation(NewFliesenCalc, true);
            NewCustomer.addCalculation(NewTepetenCalc, true);

            tfMarktMain.Export.PDFFactory.CustomerPDFDocument PDF = new tfMarktMain.Export.PDFFactory.CustomerPDFDocument(NewCustomer);
            PDF.printPDF();
        }

       
    }
}
