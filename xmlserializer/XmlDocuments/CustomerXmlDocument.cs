﻿using System;
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
            try {
            //Create root Element
            XmlElement root = doc.CreateElement(string.Empty, "Customer", string.Empty);

            //Create NameElement
            XmlElement NameElement = doc.CreateElement(string.Empty, "Name", string.Empty);
            NameElement.AppendChild(
                doc.CreateTextNode(c.Name) //Create and add TextNode to NameElement child
            );
            root.AppendChild(NameElement);//Add NameElement to root Element

            //Create CustomernumberElement
            XmlElement CustomernumberElement = doc.CreateElement(string.Empty, "Customernumber", string.Empty);
            CustomernumberElement.AppendChild(
                doc.CreateTextNode(c.Customernumber.ToString()) //Create and add TextNode to CustomernumberElement child
            );
            root.AppendChild(CustomernumberElement);//Add CustomernumberElement to root Element

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
            catch (Exception ex)
            {
                if (ex.GetType().Equals(typeof(InvalidOperationException)))
                {
                    throw (InvalidOperationException)ex;
                }
                else
                {
                    throw new InvalidOperationException("Error while creating customer xml-element",ex);
                }
            }
        }

        /// <summary>
        /// Extention method for creating a customer Element out of an customer instance
        /// </summary>
        /// <param name="doc">XmlDocument instance</param>
        /// <param name="CustomerElement">Customer Element</param>
        /// <returns>Customer instance</returns>
        public static Customer GetCustomerFromElement(this XmlDocument doc, XmlElement CustomerElement)
        {
            try { 
            Customer LoadingCustomer = new Customer(new Guid(CustomerElement.SelectSingleNode("Customernumber").InnerText));

            //Set Properties from Nodes of the xml-file
            LoadingCustomer.Name = CustomerElement.SelectSingleNode("Name").InnerText;
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
            catch (Exception ex)
            {
                if (ex.GetType().Equals(typeof(InvalidOperationException)))
                {
                    throw (InvalidOperationException)ex;
                }
                else
                {
                    throw new InvalidOperationException("Error while loading customer xml-element", ex);
                }
            }
        }
    }
}
