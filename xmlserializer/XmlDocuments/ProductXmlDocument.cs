using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;
using xmlserializer.Models.Products;

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
            try { 
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


            if (prod.getProductType().Equals(typeof(Fliese))) {

                Fliese FliesenProduct = (Fliese)prod;
                
                //Create WidthElement
                XmlElement Width = doc.CreateElement(string.Empty, "Width", string.Empty);
                Width.AppendChild(
                    doc.CreateTextNode(FliesenProduct.Breite.ToString())//Create and add TextNode to WidthElement child
                );
                root.AppendChild(Width);//Add WidthElement to root Element

                //Create LenghtElement
                XmlElement Lenght = doc.CreateElement(string.Empty, "Length", string.Empty);
                Lenght.AppendChild(
                    doc.CreateTextNode(FliesenProduct.Laenge.ToString())//Create and add TextNode to LenghtElement child
                );
                root.AppendChild(Lenght);//Add LenghtElement to root Element

                //Create PacketSizeElement
                XmlElement PacketSize = doc.CreateElement(string.Empty, "PacketSize", string.Empty);
                PacketSize.AppendChild(
                    doc.CreateTextNode(FliesenProduct.Laenge.ToString())//Create and add TextNode to PacketSizeElement child
                );
                root.AppendChild(PacketSize);//Add PacketSizeElement to root Element

            } else if (prod.getProductType().Equals(typeof(Tapete)))
            {
                Tapete TapetenProduct = (Tapete)prod;

                //Create WidthElement
                XmlElement Width = doc.CreateElement(string.Empty, "Width", string.Empty);
                Width.AppendChild(
                    doc.CreateTextNode(TapetenProduct.Breite.ToString())//Create and add TextNode to WidthElement child
                );
                root.AppendChild(Width);//Add WidthElement to root Element

                //Create LenghtElement
                XmlElement Lenght = doc.CreateElement(string.Empty, "Length", string.Empty);
                Lenght.AppendChild(
                    doc.CreateTextNode(TapetenProduct.Laenge.ToString())//Create and add TextNode to LenghtElement child
                );
                root.AppendChild(Lenght);//Add LenghtElement to root Element

                //Create RapportElement
                XmlElement Rapport = doc.CreateElement(string.Empty, "Rapport", string.Empty);
                Rapport.AppendChild(
                    doc.CreateTextNode(TapetenProduct.Rapport.ToString())//Create and add TextNode to RapportElement child
                );
                root.AppendChild(Rapport);//Add RapportElement to root Element

            }
            else if (prod.getProductType().Equals(typeof(Hilfsmittel)))
            {
                //Create ProductivityElement
                XmlElement Productivity = doc.CreateElement(string.Empty, "Productivity", string.Empty);
                Productivity.AppendChild(
                    doc.CreateTextNode(((Hilfsmittel)prod).Ergiebigkeit.ToString())//Create and add TextNode to ProductivityElement child
                );
                root.AppendChild(Productivity);//Add ProductivityElement to root Element
            }
                



                return root;//Return root Element

            }
            catch (Exception ex)
            {
                    throw new InvalidOperationException("Error while creating product xml-element", ex);
            }
        }

        /// <summary>
        /// Extention method for creating a product Element out of an product instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="ProductNode">Product Element</param>
        /// <returns>Product instance</returns>
        public static Product GetProductFromNode(this XmlDocument doc, XmlNode ProductNode)
        {
            try { 
            String AssemblyQualifiedName = ProductNode.SelectSingleNode("ProductType").InnerText;//Get AssemblyQualifiedName to identify class type

            //Instances created from CalculationType
            Type ProductType = Type.GetType(AssemblyQualifiedName);

            //Set Properties from Nodes of the xml-file
            Product LoadingProduct = (Product)Activator.CreateInstance(ProductType);
            LoadingProduct.setArtikelnummer(Int32.Parse(ProductNode.SelectSingleNode("Articlenumber").InnerText));
            LoadingProduct.setArtikelbezeichnung(ProductNode.SelectSingleNode("Articledescription").InnerText);
            LoadingProduct.setPreis(Decimal.Parse(ProductNode.SelectSingleNode("Price").InnerText));
            LoadingProduct.setProductType(ProductType);

            if (ProductType.Equals(typeof(Fliese)))
            {
                ((Fliese)LoadingProduct).Laenge = Decimal.Parse(ProductNode.SelectSingleNode("Length").InnerText);
                ((Fliese)LoadingProduct).Breite = Decimal.Parse(ProductNode.SelectSingleNode("Width").InnerText);
                ((Fliese)LoadingProduct).Paketgroesse = Int32.Parse(ProductNode.SelectSingleNode("PacketSize").InnerText);
                
            }
            else if (ProductType.Equals(typeof(Tapete)))
            {
                ((Tapete)LoadingProduct).Laenge = Decimal.Parse(ProductNode.SelectSingleNode("Length").InnerText);
                ((Tapete)LoadingProduct).Breite = Decimal.Parse(ProductNode.SelectSingleNode("Width").InnerText);
                ((Tapete)LoadingProduct).Rapport = Decimal.Parse(ProductNode.SelectSingleNode("Rapport").InnerText);

            }
            else if (ProductType.Equals(typeof(Hilfsmittel)))
            {
               ((Hilfsmittel)LoadingProduct).Ergiebigkeit = Decimal.Parse(ProductNode.SelectSingleNode("Productivity").InnerText);
            }

            return LoadingProduct;//Return Calculation
            }
            catch (Exception ex)
            {
                if (ex.GetType().Equals(typeof(InvalidOperationException)))
                {
                    throw (InvalidOperationException)ex;
                }
                else
                {
                    throw new InvalidOperationException("Error while loading product xml-node", ex);
                }
            }

        }
    }
}
