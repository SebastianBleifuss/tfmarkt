using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;
using xmlserializer.Models.Products;

namespace tfMarktMain.Tapetenkalkulation
{
    class Tapetenkalkulation : Calculation
    {
        private decimal laenge, breite;

        public Tapetenkalkulation()
        {
        }

        public decimal getLaenge()
        {
            return this.laenge;
        }

        public void setLaenge(decimal laenge)
        {
            this.laenge = laenge;
        }

        public decimal getBreite()
        {
            return this.breite;
        }

        public void setBreite(decimal breite)
        {
            this.breite = breite;
        }

        public decimal getFlaeche()
        {
            return this.laenge * this.breite;
        }

        public int rollenBerechnen(Tapete tapete, decimal laenge, decimal breite) 
        {
            //Anzahl benötigter Bahnen Wandbreite:
            int benoetigteBahnen = Convert.ToInt32(breite / tapete.Breite);
            //Berechnen der Länge einer Bahn:
            decimal bahnlaenge = Convert.ToInt32(tapete.Laenge / tapete.Rapport)*tapete.Rapport;
            Console.WriteLine(bahnlaenge);
            //http://www.meinewand.de/wie-berechne-tapetenbedarf-tapeten-rapport
            return 0;
        }

        public int rollenBerechnen(Tapete tapete)
        {
            //bei Tapeten in EUnorm(Breite:0,53m  Länge?):
            int rollen = Convert.ToInt32(this.laenge*this.breite / 5 + 2);
            return rollen;
            //Noch bestimmen bei anderen tapeten 
        }



    }
}
