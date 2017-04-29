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
    /// <summary>
    /// Serializer class for serialization of customer and product instances
    /// </summary>
    public class xmlserializer
    {
        /// <summary>
        /// Path of the datastorage where the serialized xml-files are placed
        /// </summary>
        public static readonly string DATASTORAGEPATH = "..\\datastorage";

        /// <summary>
        /// Serialize customer instances
        /// </summary>
        /// <param name="Customer">Customer instance which will be serialized</param>
        public static void serialize(Customer Customer)
        {
            XmlDocument doc = new XmlDocument();

            //Create document declaration
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docElement = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, docElement);

            //Create root element as customer element
            XmlElement root = doc.CreateCustomerElement(Customer);
            doc.AppendChild(root);

            //Save document to passed path
            doc.Save(DATASTORAGEPATH + "\\customers\\" + Customer.Name.Replace(", ", "_") + ".xml");
        }

        /// <summary>
        /// Deserialize customer xml-file defined by passed customername
        /// </summary>
        /// <param name="Customername">String defines which xml-file will be deserialize</param>
        /// <returns>Customer instance defined by customername</returns>
        public static Customer deserialize(String Customername)
        {
            //Path defines where the xml-file will be located
            String XmlPath = DATASTORAGEPATH + "\\customers\\" + Customername.Replace(", ", "_") + ".xml";
            if (File.Exists(XmlPath))//Check if xml-file exists
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XmlPath);//Load xml-file
                return doc.GetCustomerFromElement(doc.DocumentElement);//return Customer instance from loaded xml-element
            }
            else
            {
                throw new FileNotFoundException("XML-File \"" + XmlPath + "\" not found!");
            }
        }
        /// <summary>
        /// Serialize product instance
        /// </summary>
        /// <param name="Product">Product instance which will be serialized</param>
        public static void serialize(Product Product)
        {
            XmlDocument doc = new XmlDocument();

            //Create document declaration
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement docElement = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, docElement);

            //Create root element as customer element
            XmlElement root = doc.CreateProductElement(Product);
            doc.AppendChild(root);

            //Save document to passed path
            doc.Save(DATASTORAGEPATH+"\\products\\" + Product.getArtikelbezeichnung().Replace(" ","_") + ".xml");
        }

        /// <summary>
        ///  Deserialize all product-xml-file located at defined Path
        /// </summary>
        /// <returns>List of product instances</returns>
        public static List<Product> deserializeAllProducts()
        {
            XmlDocument doc = new XmlDocument();

            List<Product> ProductList = new List<Product>();
            foreach (String Name in Directory.GetFiles(DATASTORAGEPATH + "\\products"))//Forach filename located in defined Path
            {
                doc.Load(Name);//Load xml-file
                ProductList.Add(doc.GetProductFromNode(doc.DocumentElement));//Add instanced class from loaded element to list
            }

            return ProductList;
        }


    }
}
