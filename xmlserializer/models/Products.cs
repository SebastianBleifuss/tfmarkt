using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlserializer.models
{
    public abstract class Product
    {
        private int artikelnummer;
        private String artikelbezeichnung;
        private decimal preis;

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
