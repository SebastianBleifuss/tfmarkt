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
        public decimal raumFlaeche;
        public bool mitFliesenkleber;
        public Fliese ausgewaehlteFliese;
        public Hilfsmittel fugenfueller, fliesenkleber;
        public int anzahlFliesenPakete, anzahlFugenfueller, anzahlFliesenkleber;

        public Fliesenkalkulation(String Artikelbezeichnung, bool mitFliesenkleber, decimal raumFlaeche, List<Fliese> fliesenliste, Hilfsmittel fugenfueller, Hilfsmittel fliesenkleber)
        {
            this.ausgewaehlteFliese = getFliesenObjektZuArtikelbezeichnung(Artikelbezeichnung, fliesenliste);
            this.mitFliesenkleber = mitFliesenkleber;
            this.raumFlaeche = raumFlaeche;
            this.fliesenkleber = fliesenkleber;
            this.fugenfueller = fugenfueller;
            if (mitFliesenkleber)
            {
                this.anzahlFliesenkleber = berechneAnzahlFliesenkleber();
            }
            this.anzahlFugenfueller = berechneAnzahlFugenfueller();
            this.anzahlFliesenPakete = berechneAnzahlPakete();
        }

        public Fliesenkalkulation(Fliese ausgewaehlteFliese, bool mitFliesenkleber,decimal raumFlaeche, Hilfsmittel fugenfueller, Hilfsmittel fliesenkleber, int anzahlFliesenPakete, int anzahlFugenfueller, int anzahlFliesenkleber)
        {
            this.ausgewaehlteFliese = ausgewaehlteFliese;
            this.raumFlaeche = raumFlaeche;
            this.fugenfueller = fugenfueller;
            this.fliesenkleber = fliesenkleber;
            this.mitFliesenkleber = mitFliesenkleber;
            this.anzahlFliesenPakete = anzahlFliesenPakete;
            this.anzahlFliesenkleber = anzahlFliesenkleber;
            this.anzahlFugenfueller = anzahlFugenfueller;
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
