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
        private decimal flaeche;
        private static Tapetenkalkulation instance;

        public Tapetenkalkulation()
        {
            instance = this;
        }

        public decimal getFlaeche()
        {
            return flaeche;
        }

        public void setFlaeche(decimal flaeche)
        {
            this.flaeche = flaeche;
        }

        public static Tapetenkalkulation getInstance()
        {
            return instance;
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
            int rollen = Convert.ToInt32(this.flaeche / 5 + 2);
            return rollen;
            //Noch bestimmen bei anderen tapeten 
        }



    }
}
