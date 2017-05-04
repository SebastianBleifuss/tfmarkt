using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace xmlserializer.Models.Products
{
    public class Hilfsmittel: Product
    {
        private decimal ergiebigkeit;

        public decimal Ergiebigkeit
        {
            get { return ergiebigkeit; }
            set { ergiebigkeit = value; }
        }

        public Hilfsmittel(String Artikelbezeichnung, decimal Ergiebigkeit, decimal Preis)
        {
            this.artikelbezeichnung = Artikelbezeichnung;
            this.ergiebigkeit = Ergiebigkeit;
            this.preis = Preis;
            this.ProductType = typeof(Hilfsmittel);
        }

        
    }
}
