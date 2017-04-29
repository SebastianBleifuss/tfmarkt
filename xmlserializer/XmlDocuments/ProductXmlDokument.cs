using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;

namespace xmlserializer.XmlDocuments
{
    public static class ProductXmlDokument
    {
        public static XmlElement CreateProductElement(this XmlDocument doc, Product prod) {
            XmlElement root = doc.CreateElement(string.Empty, "Product", string.Empty);

            XmlElement Articlenumber = doc.CreateElement(string.Empty, "Articlenumber", string.Empty);
            Articlenumber.AppendChild(
                doc.CreateTextNode(prod.getArtikelnummer().ToString())
            );
            root.AppendChild(Articlenumber);

            XmlElement Articledescription = doc.CreateElement(string.Empty, "Articledescription", string.Empty);
            Articledescription.AppendChild(
                doc.CreateTextNode(prod.getArtikelbezeichnung())
            );
            root.AppendChild(Articledescription);

            XmlElement Price = doc.CreateElement(string.Empty, "Price", string.Empty);
            Price.AppendChild(
                doc.CreateTextNode(prod.getPreis().ToString())
            );
            root.AppendChild(Price);

            XmlElement ProductType = doc.CreateElement(string.Empty, "ProductType", string.Empty);
            ProductType.AppendChild(
                doc.CreateTextNode(prod.getProductType().AssemblyQualifiedName)
            );
            root.AppendChild(ProductType);

            return root;
        }

        public static Product GetProductFromNode(this XmlDocument doc, XmlNode ProductNode) {

            String AssemblyQualifiedName = ProductNode.SelectSingleNode("ProductType").InnerText;
            Type ProductType = Product.GetType(AssemblyQualifiedName);


            Product LoadingProduct = (Product)Activator.CreateInstance(ProductType);
            LoadingProduct.setArtikelnummer(Int32.Parse(ProductNode.SelectSingleNode("Articlenumber").InnerText));
            LoadingProduct.setArtikelbezeichnung(ProductNode.SelectSingleNode("Articledescription").InnerText);
            LoadingProduct.setPreis(Decimal.Parse(ProductNode.SelectSingleNode("Price").InnerText));
            LoadingProduct.setProductType(ProductType);

            return LoadingProduct;

        }
    }
}
