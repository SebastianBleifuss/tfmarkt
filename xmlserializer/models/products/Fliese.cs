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
        private decimal breite;


        public Fliese(String Artikelbezeichnung, decimal Laenge, decimal Breite, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.Laenge = Laenge;
            this.breite = Breite;
            this.preis = Preis;
            this.ProductType = typeof(Fliese);
        }

        public decimal Laenge { get => laenge; set => laenge = value; }
        public decimal Breite { get => breite; set => breite = value; }
    }
}
