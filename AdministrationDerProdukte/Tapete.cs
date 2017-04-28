using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.models;

namespace AdministrationDerProdukte
{
    class Tapete: Product
    {
        private decimal laenge;
        private decimal breite;
        private decimal rapport;

        public Tapete(String Artikelbezeichnung, decimal Laenge, decimal Breite, decimal Rapport, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.laenge = Laenge;
            this.breite = Breite;
            this.rapport = Rapport;
            this.preis = Preis;
        }
    }
}
