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

        public void setProductType(Type t) {
            if (t.IsAssignableFrom(typeof(Product)))
            {
                this.ProductType = t;
            }
            else
            {
                throw new InvalidOperationException("Not inherit Product");
            }
        }

        public Type getProductType() {
            return ProductType;
        }

        public int getArtikelnummer()
        {
            return artikelnummer;
        }

        public String getArtikelbezeichnung()
        {
            return artikelbezeichnung;
        }

        public decimal getPreis()
        {
            return preis;
        }
    }
}
