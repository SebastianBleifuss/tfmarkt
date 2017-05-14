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

        private int paketgroesse;
        public int Paketgroesse
        {
            get { return paketgroesse; }
            set { paketgroesse = value; }
        }

        public Fliese(int artikelnummer, String Artikelbezeichnung, decimal Laenge, decimal Breite, int Paketgroesse, decimal Preis)
        {
            this.artikelnummer = artikelnummer;
            this.artikelbezeichnung = Artikelbezeichnung;
            this.Laenge = Laenge;
            this.breite = Breite;
            this.paketgroesse = Paketgroesse;
            this.preis = Preis;
            this.ProductType = typeof(Fliese);
        }

        public Fliese()
        {

        }
    }
}
