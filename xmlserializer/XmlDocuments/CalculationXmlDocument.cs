using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;


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

            //Create AmountElement
            XmlElement Amount = doc.CreateElement(string.Empty, "Amount", string.Empty);
            Amount.AppendChild(
                doc.CreateTextNode(calc.Amount.ToString())//Create and add TextNode to AmountElement child
            );
            root.AppendChild(Amount);

            //Create ProductElement
            XmlElement Product = doc.CreateElement(string.Empty, "Product", string.Empty);

            root.AppendChild(doc.CreateProductElement(calc.SelectedProduct));//Create product-XmlElement from product instance

            return root;//Return CalculationElement
        }

        /// <summary>
        /// Extention method for creating a product Element out of an product instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="CalculationNode">Calculation Element</param>
        /// <returns>Calculation instance</returns>
        public static Calculation GetCalculationFromNode(this XmlDocument doc, XmlNode CalculationNode)
        {
            String AssemblyQualifiedName = CalculationNode.SelectSingleNode("CalculationType").InnerText;//Get AssemblyQualifiedName to identify class type
            Type CalculationType = Type.GetType(AssemblyQualifiedName);

            
            //Instances created from CalculationType
            Calculation LoadingCalculation = (Calculation)Activator.CreateInstance(CalculationType);


            //Set Properties from Nodes of the xml-file
            LoadingCalculation.Identifier = Guid.Parse(CalculationNode.SelectSingleNode("Identifier").InnerText);
            LoadingCalculation.Description = CalculationNode.SelectSingleNode("Description").InnerText;
            LoadingCalculation.CalculationType = CalculationType;
            LoadingCalculation.Amount = Int32.Parse(CalculationNode.SelectSingleNode("Amount").InnerText);
            LoadingCalculation.SelectedProduct = doc.GetProductFromNode(CalculationNode.SelectSingleNode("Product"));

            return LoadingCalculation;//Return Calculation
        }
    }
}
