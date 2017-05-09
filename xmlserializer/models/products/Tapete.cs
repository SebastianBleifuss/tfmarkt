using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace xmlserializer.Models.Products
{
    public class Tapete: Product
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
        private decimal rapport;

        public decimal Rapport
        {
            get { return rapport; }
            set { rapport = value; }
        }

        public Tapete(int artikelnummer, String Artikelbezeichnung, decimal Laenge, decimal Breite, decimal Rapport, decimal Preis)
        {
            this.artikelnummer = artikelnummer;
            this.artikelbezeichnung = Artikelbezeichnung;
            this.laenge = Laenge;
            this.breite = Breite;
            this.rapport = Rapport;
            this.preis = Preis;
            this.ProductType = typeof(Tapete);
        }

        public Tapete()
        {

        }

        
    }
}
