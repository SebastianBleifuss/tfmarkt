using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace xmlserializer.Models.Products
{
    public class Fliese : Product
    {
        private decimal laenge;

        public decimal Laenge
        {
            get { return laenge; }
            set { laenge = value; }
        }
        private decimal breite;

        public decimal Breite
        {
            get { return breite; }
            set { breite = value; }
        }


        public Fliese(String Artikelbezeichnung, decimal Laenge, decimal Breite, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.Laenge = Laenge;
            this.breite = Breite;
            this.preis = Preis;
            this.ProductType = typeof(Fliese);
        }

        public Fliese()
        {

        }
    }
}
