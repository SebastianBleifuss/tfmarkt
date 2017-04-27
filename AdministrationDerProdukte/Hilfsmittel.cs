using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.models;

namespace AdministrationDerProdukte
{
    class Hilfsmittel: Product
    {
        private decimal ergiebigkeit;

        public Hilfsmittel(String Artikelbezeichnung, decimal Ergiebigkeit, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.ergiebigkeit = Ergiebigkeit;
            this.preis = Preis;
        }
    }
}
