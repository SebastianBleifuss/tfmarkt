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
        private decimal raumFlaeche;
        private bool mitFliesenkleber;
        private Fliese ausgewaehlteFliese;
        private Hilfsmittel fugenfueller, fliesenkleber;

        public Fliesenkalkulation(String Artikelbezeichnung, bool mitFliesenkleber, decimal raumFlaeche, List<Fliese> fliesenliste, Hilfsmittel fugenfueller, Hilfsmittel fliesenkleber)
        {
            this.ausgewaehlteFliese = getFliesenObjektZuArtikelbezeichnung(Artikelbezeichnung, fliesenliste);
            this.mitFliesenkleber = mitFliesenkleber;
            this.raumFlaeche = raumFlaeche;
            this.fliesenkleber = fliesenkleber;
            this.fugenfueller = fugenfueller;
        }


        public decimal getFlaeche()
        {
            return raumFlaeche;
        }

        private int berechneAnzahlPakete()
        {
            decimal fliesenFlaeche = (ausgewaehlteFliese.Breite / 100) * (ausgewaehlteFliese.Laenge / 100);
            int anzFliesen = (int)Math.Ceiling(raumFlaeche * 1.05m / fliesenFlaeche);
            int anzPakete = (int) Math.Ceiling((decimal)anzFliesen / (decimal)ausgewaehlteFliese.Paketgroesse);
            return anzPakete;
        }

        private int berechneAnzahlFugenfueller()
        {
            return (int) Math.Ceiling(raumFlaeche / fugenfueller.Ergiebigkeit);
        }

        private int berechneAnzahlFliesenkleber()
        {
            return (int) Math.Ceiling(raumFlaeche / fliesenkleber.Ergiebigkeit);
        }

        private Fliese getFliesenObjektZuArtikelbezeichnung(String name, List<Fliese> fliesenliste)
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
    }
}
