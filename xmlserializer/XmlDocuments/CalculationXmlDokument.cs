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
    public static class CalculationXmlDokument
    {
        public static XmlElement CreateCalculationElement(this XmlDocument doc, Calculation calc)
        {
            XmlElement root = doc.CreateElement(string.Empty, "Calculation", string.Empty);

            XmlElement Identifier = doc.CreateElement(string.Empty, "Identifier", string.Empty);
            Identifier.AppendChild(
                doc.CreateTextNode(calc.Identifier.ToString())
            );
            root.AppendChild(Identifier);

            XmlElement Description = doc.CreateElement(string.Empty, "Description", string.Empty);
            Description.AppendChild(
                doc.CreateTextNode(calc.Description)
            );
            root.AppendChild(Description);

            XmlElement CalculationType = doc.CreateElement(string.Empty, "CalculationType", string.Empty);
            CalculationType.AppendChild(
                doc.CreateTextNode(calc.CalculationType.AssemblyQualifiedName)
            );
            root.AppendChild(CalculationType);

            XmlElement Amount = doc.CreateElement(string.Empty, "Amount", string.Empty);
            Amount.AppendChild(
                doc.CreateTextNode(calc.Amount.ToString())
            );
            root.AppendChild(Amount);

            XmlElement Product = doc.CreateElement(string.Empty, "Product", string.Empty);

            root.AppendChild(doc.CreateProductElement(calc.SelectedProduct));



            Type calcType = calc.CalculationType;

                if (calcType.Equals(typeof(FooBarCalculation)))
                {
                    
                }
                else if (calcType.Equals(typeof(FooBarCalculation)))
                {

                }
                else if (calcType.Equals(typeof(FooBarCalculation)))
                {

                }
                else if (calcType.Equals(typeof(FooBarCalculation)))
                {

                }
                else if (calcType.Equals(typeof(FooBarCalculation)))
                {

                }
                else {
                    throw new NotSupportedException(calcType.Name + " is not supported!");
                }


            return root;
        }

        public static Calculation GetCalculationFromNode(this XmlDocument doc, XmlNode CalculationNode)
        {
            String AssemblyQualifiedName = CalculationNode.SelectSingleNode("CalculationType").InnerText;
            Type CalculationType = Calculation.GetType(AssemblyQualifiedName);



            Calculation LoadingCalculation = (Calculation)Activator.CreateInstance(CalculationType);

            LoadingCalculation.Identifier = Guid.Parse(CalculationNode.SelectSingleNode("Identifier").InnerText);
            LoadingCalculation.Description = CalculationNode.SelectSingleNode("Description").InnerText;
            LoadingCalculation.CalculationType = CalculationType;
            LoadingCalculation.Amount = Int32.Parse(CalculationNode.SelectSingleNode("Amount").InnerText);
            LoadingCalculation.SelectedProduct = doc.GetProductFromNode(CalculationNode.SelectSingleNode("Product"));

            if (CalculationType.Equals(typeof(FooBarCalculation)))
            {

            }
            else if (CalculationType.Equals(typeof(FooBarCalculation)))
            {

            }
            else if (CalculationType.Equals(typeof(FooBarCalculation)))
            {

            }
            else if (CalculationType.Equals(typeof(FooBarCalculation)))
            {

            }
            else if (CalculationType.Equals(typeof(FooBarCalculation)))
            {

            }
            else
            {
                throw new NotSupportedException(CalculationType.Name + " is not supported!");
            }

            return LoadingCalculation;
        }
    }
}
