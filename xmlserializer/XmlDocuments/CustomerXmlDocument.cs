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
    /// <summary>
    /// Extention class for XmlDocument
    /// </summary>
    public static class CustomerXmlDocument
    {

        /// <summary>
        /// Extention method for creating a Customer-XmlElement out of an customer instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="c">Customer instance for creating element</param>
        /// <returns>Customer-XmlElement</returns>
        public static XmlElement CreateCustomerElement(this XmlDocument doc, Customer c)
        {
            //Create root Element
            XmlElement root = doc.CreateElement(string.Empty, "Customer", string.Empty);

            //Create NameElement
            XmlElement NameElement = doc.CreateElement(string.Empty, "Name", string.Empty);
            NameElement.AppendChild(
                doc.CreateTextNode(c.Name) //Create and add TextNode to NameElement child
            );
            root.AppendChild(NameElement);//Add NameElement to root Element

            //Create CalculationsElement
            XmlElement CalculationsElement = doc.CreateElement(string.Empty, "Calculations", string.Empty);
            root.AppendChild(CalculationsElement);//Add CalculationsElement to root Element

            foreach (Calculation calc in c.Calculations.Values)//Create and add all Calculations as XmlElements
            {
                CalculationsElement.AppendChild(
                        doc.CreateCalculationElement(calc)//Create calculation-XmlElement from calculation instance
                );
            }


            return root;//Return root Element
        }

        /// <summary>
        /// Extention method for creating a customer Element out of an customer instance
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="CustomerElement">Customer Element from where the calculations are loaded</param>
        /// <returns></returns>
        public static Customer GetCustomerFromElement(this XmlDocument doc, XmlElement CustomerElement)
        {
            Customer LoadingCustomer = new Customer();
            LoadingCustomer.Name = CustomerElement.SelectSingleNode("Name").InnerText;//Set InnerText of "Name"-Node into Customer.Name-Property
            LoadingCustomer.Calculations = new Dictionary<Guid, Calculation>();
            foreach (XmlNode calcNode in CustomerElement.SelectSingleNode("Calculations").ChildNodes)
            {
                if (calcNode.Name.Equals("Calculation"))//Check if Node is Calculation
                {
                    Calculation calc = doc.GetCalculationFromNode(calcNode);//Get calculation from passed element
                    LoadingCustomer.Calculations.Add(calc.Identifier, calc);//Add Calculation 
                }
            }

            return LoadingCustomer;//Return Customer
        }
    }
}
