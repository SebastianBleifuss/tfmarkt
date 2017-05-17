using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;
using xmlserializer.Models.Products;

namespace xmlserializer.Models.Calculations
{
    public class Tapetenkalkulation : Calculation
    {
        public Hilfsmittel tapetenkleister;
        public Tapete tapete;
        public int rollen, kleisterpakete;


        public Tapetenkalkulation()
        {
            this.CalculationType = typeof(Tapetenkalkulation);
            this.tapetenkleister = new Hilfsmittel();
        }

        public decimal getFlaeche()
        {
            return this.Width * this.Length;
        }

        public void setHilfsmittel(Hilfsmittel hilfsmittel)
        {
            this.tapetenkleister = hilfsmittel;
        }

        public int rollenBerechnen(Tapete tapete)
        {
            //Nur so lange bis sie überegben werden: 
            if (Width != 0 && Length != 0)
            {
                //Zur Eingrenzung der gültigen Werte ist die Maximale Breite einer Wand auf 250 m und die maximale Länge auf 100m begrenzt.
                if (Width <= 250 && Length <= 100)
                {
                    try
                    {
                        decimal verschnitt = 0.15m;//pauschal 15cm Verschnitt
                        int muster;
                        decimal bahnlaenge;
                        //Anzahl benötigter Bahnen Wandbreite:
                        int benoetigteBahnen = Convert.ToInt32(Math.Ceiling(Width / tapete.Breite));
                        //Berechnen der Länge einer Bahn:
                        if (tapete.Rapport != 0)
                        {
                            //Wieviel Muster sind pro bahn?
                            muster = Convert.ToInt32(Length / tapete.Rapport);
                            bahnlaenge = muster * tapete.Rapport;
                        }
                        else
                        {
                            //kein rapport, keine Muster
                            bahnlaenge = Length;
                        }
                        //Zur Sicherheit wird pauschal pro Bahn ein Verschnitt hinzugefügt unabhängig davon, dass durch den rapport schon ein Verschnitt entstanden sein könnte.
                        bahnlaenge += verschnitt;

                        int bahnenProRolle = Convert.ToInt32(Math.Round(tapete.Laenge / bahnlaenge, MidpointRounding.ToEven));
                        if (bahnenProRolle == 0)
                        {
                            bahnenProRolle = 1;
                        }
                        //Berechnen der Rollen. Das Ergebnis wird auf die nächstgerade Rolenanzahl aufgerundet.
                        this.rollen = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(benoetigteBahnen / (bahnenProRolle *1m))));
                        return this.rollen;
                        //Was ist mit verschnitt? https://www.blitzrechner.de/tapetenrollen-bedarf/
                    }
                    catch 
                    {
                        //mach mal nichts
                    }
                }
            }
            //Es ist ein Fehler aufgetreten, gekennzeichnet durch Rückgabewert -1:
            return -1;
            //Quelle: http://www.meinewand.de/wie-berechne-tapetenbedarf-tapeten-rapport
        }

        public decimal getGesamtpreis(Tapete tapete, int rollen)
        {
            try
            {
                decimal rollenpreis = tapete.getPreis() * rollen;
                decimal kleisterpreis = getKleistermenge() * tapetenkleister.getPreis();
                return rollenpreis + kleisterpreis;
            }
            catch 
            {
                return -1;
            }
            
        }

        public int getKleistermenge()
        {
            // verwendeter Tapetenkleister: https://www.amazon.de/Metylan-spezial-Kleister-Leistungsplus-Henkel-MCX-Technologie/dp/B002QHH5Z8/ref=sr_1_1?s=diy&ie=UTF8&qid=1494791983&sr=1-1
            if (tapetenkleister.Ergiebigkeit != 0)
            {
                try
                {
                    decimal gesamtFlaeche = Length * Width;
                    decimal pakete = gesamtFlaeche / tapetenkleister.Ergiebigkeit;

                    kleisterpakete = Convert.ToInt32(Math.Ceiling(pakete));
                    return kleisterpakete;
                }
                catch 
                {
                    //
                }
            }
            return -1;
        }



    }
}
