using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;
using xmlserializer.Models.Products;

namespace tfMarktMain.Fliesenkalkulation
{
    public class Fliesenkalkulation: Calculation
    {
        private decimal flaeche;
        private static Fliesenkalkulation aktuelleInstanz;
        private List<Fliese> fliesenliste;

        public Fliesenkalkulation()
        {
            fliesenliste = new List<Fliese>();
            aktuelleInstanz = this;
            fuelleFliesenliste();
        }


        public decimal getFlaeche()
        {
            return flaeche;
        }

        private void fuelleFliesenliste()
        {
            List<Product> alleProdukte = xmlserializer.xmlserializer.deserializeAllProducts();
            foreach (Product produkt in alleProdukte)
            {
                Type produktTyp = produkt.getProductType();
                if (produktTyp.Equals(typeof(Fliese)))
                {
                    fliesenliste.Add((Fliese)produkt);
                }
            }
        }

        public void setFlaeche(decimal flaeche)
        {
            this.flaeche = flaeche;
        }

        public static Fliesenkalkulation getMe()
        {
            return aktuelleInstanz;
        }

        public void berechneFliesen(String fliesenname)
        {
            Fliese ausgewaehlteFliese = getFliesenObjektZuArtikelbezeichnung(fliesenname);
        }

        private Fliese getFliesenObjektZuArtikelbezeichnung(String name)
        {
            foreach (Fliese fliese in fliesenliste)
            {
                if (fliese.getArtikelbezeichnung() == name)
                {
                    return fliese;
                }
            }
            return null;
        }

        public List<Fliese> getFliesenListe()
        {
            return fliesenliste;
        }

    }
}
