using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xmlserializer.Models;

namespace xmlserializer.XmlDocuments
{
    public static class CustomerXmlDocument
    {
        public static XmlElement CreateCustomerElement(this XmlDocument doc, Customer c)
        {
            XmlElement root = doc.CreateElement(string.Empty, "Customer", string.Empty);

            XmlElement NameElement = doc.CreateElement(string.Empty, "Name", string.Empty);
            NameElement.AppendChild(
                doc.CreateTextNode(c.Name)
            );
            root.AppendChild(NameElement);

            XmlElement CalculationsElement = doc.CreateElement(string.Empty, "Calculations", string.Empty);
            root.AppendChild(CalculationsElement);

            foreach (Calculation calc in c.Calculations.Values)
            {
                CalculationsElement.AppendChild(
                        doc.CreateCalculationElement(calc)
                );
            }


            return root;
        }

        public static Customer GetCustomerFromElement(this XmlDocument doc, XmlElement CustomerElement)
        {
            Customer LoadingCustomer = new Customer();
            LoadingCustomer.Name = CustomerElement.SelectSingleNode("Name").InnerText;
            LoadingCustomer.Calculations = new Dictionary<Guid, Calculation>();
            foreach (XmlNode calcNode in CustomerElement.SelectSingleNode("Calculations").ChildNodes)
            {
                if (calcNode.Name.Equals("Calculation"))
                {
                    Calculation calc = doc.GetCalculationFromNode(calcNode);
                    LoadingCustomer.Calculations.Add(calc.Identifier, calc);
                }
            }

            return LoadingCustomer;
        }
    }
}
