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
        internal protected void setProductType(Type t)
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

        internal protected void setArtikelnummer(int nummer)
        {
            artikelnummer = nummer;
        }

        internal protected void setArtikelbezeichnung(String bez)
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

        internal protected void setPreis(decimal price)
        {
            preis = price;
        }

    }
}
