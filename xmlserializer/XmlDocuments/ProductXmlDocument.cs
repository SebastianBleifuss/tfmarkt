using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;

namespace xmlserializer.XmlDocuments
{
    public static class ProductXmlDocument
    {
        /// <summary>
        /// Extention method for creating a Product-XmlElement out of an product instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="prod">Product instance for creating element</param>
        /// <returns>Product-XmlElement</returns>
        public static XmlElement CreateProductElement(this XmlDocument doc, Product prod)
        {

            //Create root Element
            XmlElement root = doc.CreateElement(string.Empty, "Product", string.Empty);

            //Create ArticlenumberElement
            XmlElement Articlenumber = doc.CreateElement(string.Empty, "Articlenumber", string.Empty);
            Articlenumber.AppendChild(
                doc.CreateTextNode(prod.getArtikelnummer().ToString())//Create and add TextNode to ArticlenumberElement child
            );
            root.AppendChild(Articlenumber);//Add ArticlenumberElement to root Element


            //Create ArticledescriptionElement
            XmlElement Articledescription = doc.CreateElement(string.Empty, "Articledescription", string.Empty);
            Articledescription.AppendChild(
                doc.CreateTextNode(prod.getArtikelbezeichnung())//Create and add TextNode to ArticledescriptionElement child
            );
            root.AppendChild(Articledescription);//Add ArticledescriptionElement to root Element


            //Create PriceElement
            XmlElement Price = doc.CreateElement(string.Empty, "Price", string.Empty);
            Price.AppendChild(
                doc.CreateTextNode(prod.getPreis().ToString())//Create and add TextNode to PriceElement child
            );
            root.AppendChild(Price);//Add PriceElement to root Element


            //Create ProductTypeElement
            XmlElement ProductType = doc.CreateElement(string.Empty, "ProductType", string.Empty);
            ProductType.AppendChild(
                doc.CreateTextNode(prod.getProductType().AssemblyQualifiedName)//Create and add TextNode to ProductTypeElement child
            );
            root.AppendChild(ProductType);//Add ProductTypeElement to root Element

            return root;//Return root Element
        }

        /// <summary>
        /// Extention method for creating a product Element out of an product instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="ProductNode">Product Element</param>
        /// <returns>Product instance</returns>
        public static Product GetProductFromNode(this XmlDocument doc, XmlNode ProductNode)
        {

            String AssemblyQualifiedName = ProductNode.SelectSingleNode("ProductType").InnerText;//Get AssemblyQualifiedName to identify class type

            //Instances created from CalculationType
            Type ProductType = Product.GetType(AssemblyQualifiedName);//Get type from AssemblyQualifiedName

            //Set Properties from Nodes of the xml-file
            Product LoadingProduct = (Product)Activator.CreateInstance(ProductType);
            LoadingProduct.setArtikelnummer(Int32.Parse(ProductNode.SelectSingleNode("Articlenumber").InnerText));
            LoadingProduct.setArtikelbezeichnung(ProductNode.SelectSingleNode("Articledescription").InnerText);
            LoadingProduct.setPreis(Decimal.Parse(ProductNode.SelectSingleNode("Price").InnerText));
            LoadingProduct.setProductType(ProductType);

            return LoadingProduct;//Return Calculation

        }
    }
}
