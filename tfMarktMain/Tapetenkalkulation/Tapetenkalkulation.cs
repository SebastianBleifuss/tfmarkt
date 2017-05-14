using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;
using xmlserializer.Models.Products;

namespace tfMarktMain.Tapetenkalkulation
{
    public class Tapetenkalkulation : Calculation
    {
        private decimal laenge, breite;
        private Hilfsmittel hilfsmittel;

        public Tapetenkalkulation()
        {
            this.CalculationType = typeof(Tapetenkalkulation);
            this.hilfsmittel = new Hilfsmittel();
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

        public void setHilfsmittel(Hilfsmittel hilfsmittel)
        {
            this.hilfsmittel = hilfsmittel;
        }

        public int rollenBerechnen(Tapete tapete)
        {
            //Nur so lange bis sie überegben werden: 
            if (breite != 0 && laenge != 0) 
            {
                int muster;
                decimal bahnlaenge;
                //Anzahl benötigter Bahnen Wandbreite:
                int benoetigteBahnen = Convert.ToInt32(Math.Ceiling(breite / tapete.Breite));
                //Berechnen der Länge einer Bahn:
                if (tapete.Rapport != 0) 
                {
                    //Wieviel Muster sind pro bahn?
                    muster = Convert.ToInt32(laenge / tapete.Rapport);
                    bahnlaenge = muster * tapete.Rapport;
                }
                else
                {
                    //kein rapport, keine Muster
                    bahnlaenge = laenge;
                }

                int bahnenProRolle = Convert.ToInt32(Math.Round(tapete.Laenge / bahnlaenge, MidpointRounding.ToEven));
                if (bahnenProRolle == 0)
                {
                    bahnenProRolle = 1;
                }
                return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(benoetigteBahnen / bahnenProRolle)));
                //Was ist mit verschnitt? https://www.blitzrechner.de/tapetenrollen-bedarf/
            }
            return -1;
            //Quelle: http://www.meinewand.de/wie-berechne-tapetenbedarf-tapeten-rapport
        }

        public decimal getGesamtpreis(Tapete tapete, int rollen) 
        {
            decimal rollenpreis=tapete.getPreis()* rollen;
            decimal kleisterpreis = getKleistermenge() * hilfsmittel.getPreis();
            return rollenpreis+kleisterpreis;
        }

        public int getKleistermenge() 
        {
            // verwendeter Tapetenkleister: https://www.amazon.de/Metylan-spezial-Kleister-Leistungsplus-Henkel-MCX-Technologie/dp/B002QHH5Z8/ref=sr_1_1?s=diy&ie=UTF8&qid=1494791983&sr=1-1
            if (hilfsmittel.Ergiebigkeit != 0)
            {
                decimal gesamtFlaeche = laenge * breite;
                decimal pakete = gesamtFlaeche / hilfsmittel.Ergiebigkeit;
                return Convert.ToInt32(Math.Ceiling(pakete));
            }
            return -1;
        }



    }
}
