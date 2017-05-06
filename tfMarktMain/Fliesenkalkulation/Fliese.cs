using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace Fliesenkalkulation
{
    class Fliese : Product
    {
        private decimal laenge;
        private decimal breite;


        public Fliese(String Artikelbezeichnung, decimal Laenge, decimal Breite, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.laenge = Laenge;
            this.breite = Breite;
            this.preis = Preis;
        }
    }
}
