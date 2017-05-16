using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;
using xmlserializer.Models.Calculations;
using xmlserializer.Models.Products;

namespace xmlserializer.XmlDocuments
{
    /// <summary>
    /// Extention class for XmlDocument
    /// </summary>
    public static class CalculationXmlDocument
    {
        /// <summary>
        /// Extention method for creating a Calculation-XmlElement out of an customer instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="calc">Calculation instance for creating element</param>
        /// <returns>Calculation-XmlElement</returns>
        public static XmlElement CreateCalculationElement(this XmlDocument doc, Calculation calc)
        {
            try { 
            //Create root Element
            XmlElement root = doc.CreateElement(string.Empty, "Calculation", string.Empty);

            //Create IdentifierElement
            XmlElement Identifier = doc.CreateElement(string.Empty, "Identifier", string.Empty);
            Identifier.AppendChild(
                doc.CreateTextNode(calc.Identifier.ToString())//Create and add TextNode to IdentifierElement child
            );
            root.AppendChild(Identifier);

            //Create DescriptionElement
            XmlElement Description = doc.CreateElement(string.Empty, "Description", string.Empty);
            Description.AppendChild(
                doc.CreateTextNode(calc.Description)//Create and add TextNode to DescriptionElement child
            );
            root.AppendChild(Description);

            //Create CalculationTypeElement
            XmlElement CalculationType = doc.CreateElement(string.Empty, "CalculationType", string.Empty);
            CalculationType.AppendChild(
                doc.CreateTextNode(calc.CalculationType.AssemblyQualifiedName)//Create and add TextNode to CalculationTypeElement child
            );
            root.AppendChild(CalculationType);


            //Create LengthElement
            XmlElement Length = doc.CreateElement(string.Empty, "Length", string.Empty);
            Length.AppendChild(
                doc.CreateTextNode(calc.Length.ToString())//Create and add TextNode to LengthElement child
            );
            root.AppendChild(Length);

            //Create WidthElement
            XmlElement Width = doc.CreateElement(string.Empty, "Width", string.Empty);
            Width.AppendChild(
                doc.CreateTextNode(calc.Width.ToString())//Create and add TextNode to WidthElement child
            );
            root.AppendChild(Width);

            //Create WithExtraProductElement
            XmlElement WithExtraProduct = doc.CreateElement(string.Empty, "WithExtraProduct", string.Empty);
            WithExtraProduct.AppendChild(
                doc.CreateTextNode(calc.WithExtraProduct.ToString())//Create and add TextNode to WithExtraProductElement child
            );
            root.AppendChild(WithExtraProduct);

            //Create ProductElement
            XmlElement Products = doc.CreateElement(string.Empty, "Products", string.Empty);

            root.AppendChild(Products);//Create product-XmlElement from product instance

                XmlElement Product;
                XmlElement Amount;
                if (calc.CalculationType.Equals(typeof(Fliesenkalkulation)))
                {
                    Fliesenkalkulation FliesenCalc = (Fliesenkalkulation)calc;

                    Product = doc.CreateProductElement(FliesenCalc.ausgewaehlteFliese);
                    Amount = doc.CreateElement(string.Empty, "AmountTile", string.Empty);
                    Amount.AppendChild(
                        doc.CreateTextNode(FliesenCalc.anzahlFliesenPakete.ToString())
                        );

                    Product.AppendChild( Amount);
                    Products.AppendChild(Product);

                    Product = doc.CreateProductElement(FliesenCalc.fugenfueller);
                    Amount = doc.CreateElement(string.Empty, "AmountFiller", string.Empty);
                    Amount.AppendChild(
                        doc.CreateTextNode(FliesenCalc.anzahlFugenfueller.ToString())
                        );
                    
                    Product.AppendChild(Amount);
                    Products.AppendChild(Product);


                    if (FliesenCalc.WithExtraProduct) { 
                    Product = doc.CreateProductElement(FliesenCalc.fliesenkleber);
                        Amount = doc.CreateElement(string.Empty, "AmountGlue", string.Empty);
                        Amount.AppendChild(
                            doc.CreateTextNode(FliesenCalc.anzahlFliesenkleber.ToString())
                            );




                        Product.AppendChild(Amount);
                        Products.AppendChild(Product);
                    }

                }
                else if (calc.CalculationType.Equals(typeof(Tapetenkalkulation)))
                {
                    Tapetenkalkulation TapetenCalc = (Tapetenkalkulation)calc;

                    Product = doc.CreateProductElement(TapetenCalc.tapete);
                    Amount = doc.CreateElement(string.Empty, "AmountWallpaper", string.Empty);
                    Amount.AppendChild(
                        doc.CreateTextNode(TapetenCalc.rollen.ToString())
                        );

                    Product.AppendChild(Amount);
                    Products.AppendChild(Product);

                    Product = doc.CreateProductElement(TapetenCalc.tapetenkleister);
                    Amount = doc.CreateElement(string.Empty, "AmountPaste", string.Empty);
                    Amount.AppendChild(
                        doc.CreateTextNode(TapetenCalc.kleisterpakete.ToString())
                        );

                    Product.AppendChild(Amount);
                    Products.AppendChild(Product);
                }



                return root;//Return CalculationElement
            }
            catch (Exception ex)
            {
                if (ex.GetType().Equals(typeof(InvalidOperationException)))
                {
                    throw (InvalidOperationException)ex;
                }
                else
                {
                    throw new InvalidOperationException("Error while creating calculation xml-element",ex);
                }
            }
        }

        /// <summary>
        /// Extention method for creating a product Element out of an product instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="CalculationNode">Calculation Element</param>
        /// <returns>Calculation instance</returns>
        public static Calculation GetCalculationFromNode(this XmlDocument doc, XmlNode CalculationNode)
        {
            try { 
            String AssemblyQualifiedName = CalculationNode.SelectSingleNode("CalculationType").InnerText;//Get AssemblyQualifiedName to identify class type
            Type CalculationType = Type.GetType(AssemblyQualifiedName);

            
            //Instances created from CalculationType
            Calculation LoadingCalculation = (Calculation)Activator.CreateInstance(CalculationType);


            //Set Properties from Nodes of the xml-file
            LoadingCalculation.Identifier = Guid.Parse(CalculationNode.SelectSingleNode("Identifier").InnerText);
            LoadingCalculation.Description = CalculationNode.SelectSingleNode("Description").InnerText;
            LoadingCalculation.CalculationType = CalculationType;
            LoadingCalculation.Length = Decimal.Parse(CalculationNode.SelectSingleNode("Length").InnerText);
            LoadingCalculation.Width = Decimal.Parse(CalculationNode.SelectSingleNode("Width").InnerText);
            LoadingCalculation.WithExtraProduct = Boolean.Parse(CalculationNode.SelectSingleNode("WithExtraProduct").InnerText);


                XmlNode ProductNode;
                if (CalculationType.Equals(typeof(Fliesenkalkulation)))
                {
                    Fliesenkalkulation LoadingTile = (Fliesenkalkulation)LoadingCalculation;

                    XmlNodeList ProductNodes = CalculationNode.SelectSingleNode("Products").ChildNodes;





                    ProductNode = ProductNodes[0];
                    LoadingTile.ausgewaehlteFliese = (Fliese) doc.GetProductFromNode(ProductNode);
                    LoadingTile.anzahlFliesenPakete = Int32.Parse(ProductNode.SelectSingleNode("AmountTile").InnerText);

                    ProductNode = ProductNodes[1];
                    LoadingTile.fugenfueller = (Hilfsmittel)doc.GetProductFromNode(ProductNode);
                    LoadingTile.anzahlFliesenPakete = Int32.Parse(ProductNode.SelectSingleNode("AmountFiller").InnerText);
                    if (LoadingTile.WithExtraProduct) {

                    ProductNode = ProductNodes[3];
                    LoadingTile.ausgewaehlteFliese = (Fliese)doc.GetProductFromNode(ProductNode);
                    LoadingTile.anzahlFliesenPakete = Int32.Parse(ProductNode.SelectSingleNode("AmountGlue").InnerText);
                    }

                }
                else if (CalculationType.Equals(typeof(Tapetenkalkulation)))
                {
                    Tapetenkalkulation LaodingWallpaper = (Tapetenkalkulation)LoadingCalculation;

                    XmlNodeList ProductNodes = CalculationNode.SelectSingleNode("Products").ChildNodes;





                    ProductNode = ProductNodes[0];
                    LaodingWallpaper.tapete = (Tapete)doc.GetProductFromNode(ProductNode);
                    LaodingWallpaper.rollen = Int32.Parse(ProductNode.SelectSingleNode("AmountWallpaper").InnerText);

                    ProductNode = ProductNodes[1];
                    LaodingWallpaper.tapetenkleister = (Hilfsmittel)doc.GetProductFromNode(ProductNode);
                    LaodingWallpaper.kleisterpakete = Int32.Parse(ProductNode.SelectSingleNode("AmountPaste").InnerText);


                }

                return LoadingCalculation;//Return Calculation
            }
            catch (Exception ex)
            {
                if (ex.GetType().Equals(typeof(InvalidOperationException)))
                {
                    throw (InvalidOperationException)ex;
                }
                else
                {
                    throw new InvalidOperationException("Error while loading calculation xml-node", ex);
                }
            }
        }
    }
}
