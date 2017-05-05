using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models.Products;

namespace xmlserializer.Models
{
    /// <summary>
    /// Abstract Class defines Product
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Articlenumber
        /// </summary>
        protected int artikelnummer;

        /// <summary>
        /// Articledescrption
        /// </summary>
        protected String artikelbezeichnung;

        /// <summary>
        /// Articleprice
        /// </summary>
        protected decimal preis;

        /// <summary>
        /// ProductType
        /// </summary>
        protected Type ProductType;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// set ProductType
        /// </summary>
        /// <param name="t">Type of product</param>
        public void setProductType(Type t)
        {
            if (typeof(Product).IsAssignableFrom(t))//Check if passed type inherit base type
            {
                this.ProductType = t;
            }
            else
            {
                throw new InvalidOperationException("Not inherit Product");
            }
        }

        public Type getProductType()
        {
            return ProductType;
        }

        public int getArtikelnummer()
        {
            return artikelnummer;
        }

        public void setArtikelnummer(int nummer)
        {
            artikelnummer = nummer;
        }

        public void setArtikelbezeichnung(String bez)
        {
            artikelbezeichnung = bez;
        }

        public String getArtikelbezeichnung()
        {
            return artikelbezeichnung;
        }

        public decimal getPreis()
        {
            return preis;
        }

        public void setPreis(decimal price)
        {
            preis = price;
        }

        /// <summary>
        /// Return ProductType of product if the AssemblyQualifiedName matches
        /// </summary>
        /// <param name="AssemblyQualifiedName">AssemblyQualifiedName</param>
        /// <returns>Type of calculation</returns>
        public static Type GetType(String AssemblyQualifiedName)
        {
            if (AssemblyQualifiedName.Equals(typeof(FooBarProduct).AssemblyQualifiedName))//Check if passed type inherit base type
            {
                return typeof(FooBarProduct);
            }
            else if (AssemblyQualifiedName.Equals(typeof(Fliese).AssemblyQualifiedName))//Check if passed type inherit base type
            {
                return typeof(Fliese);
            }
            else if (AssemblyQualifiedName.Equals(typeof(Tapete).AssemblyQualifiedName))//Check if passed type inherit base type
            {
                return typeof(Tapete);
            }
            else if (AssemblyQualifiedName.Equals(typeof(Hilfsmittel).AssemblyQualifiedName))//Check if passed type inherit base type
            {
                return typeof(Hilfsmittel);
            }
            else
            {
                throw new NotSupportedException(AssemblyQualifiedName + " is not supported!");
            }
            //Erweitern!
        }

    }
}
