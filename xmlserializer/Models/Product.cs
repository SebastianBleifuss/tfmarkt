using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer.Models
{
    public abstract class Product
    {
        protected int artikelnummer;
        protected String artikelbezeichnung;
        protected decimal preis;
        protected Type ProductType;

        public Product()
        {
        }

        public void setProductType(Type t)
        {
            if (typeof(Product).IsAssignableFrom(t))
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

        public static Type GetType(String AssemblyQualifiedName)
        {
            if (AssemblyQualifiedName.Equals(typeof(FooBarProduct).AssemblyQualifiedName))
            {
                return typeof(FooBarProduct);
            }
            else
            {
                throw new NotSupportedException(AssemblyQualifiedName + " is not supported!");
            }
            //Erweitern!
        }

    }
}
