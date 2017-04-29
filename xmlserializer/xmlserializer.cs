using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;
using xmlserializer.XmlDocuments;

namespace xmlserializer
{

    public class xmlserializer
    {
        public static readonly string DATASTORAGEPATH = "..\\datastorage";

        public static void serialize(Customer Customer)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docElement = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, docElement);

            XmlElement root = doc.CreateCustomerElement(Customer);
            doc.AppendChild(root);

            doc.Save(DATASTORAGEPATH + "\\customers\\" + Customer.Name.Replace(", ", "_") + ".xml");
        }


        public static Customer deserialize(String Customername)
        {
            String XmlPath = DATASTORAGEPATH + "\\customers\\" + Customername.Replace(", ", "_") + ".xml";
            if (File.Exists(XmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XmlPath);
                return doc.GetCustomerFromElement(doc.DocumentElement);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public static void serialize(Product Product)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docElement = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, docElement);

            XmlElement root = doc.CreateProductElement(Product);
            doc.AppendChild(root);

            doc.Save(DATASTORAGEPATH+"\\products\\" + Product.getArtikelbezeichnung().Replace(" ","_") + ".xml");
        }

        public static List<Product> deserializeAllProducts()
        {
            XmlDocument doc = new XmlDocument();

            List<Product> ProductList = new List<Product>();
            foreach (String Name in Directory.GetFiles(DATASTORAGEPATH + "\\products"))
            {
                doc.Load(Name);
                ProductList.Add(doc.GetProductFromNode(doc.DocumentElement));
            }

            return ProductList;
        }


    }
}
