using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmlserializer.Models;

namespace tfMarktMain.Fliesenkalkulation
{
    public class Fliesenkalkulation: Calculation
    {
        private decimal flaeche;
        private static Fliesenkalkulation instance;

        public Fliesenkalkulation()
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

        public static Fliesenkalkulation getInstance()
        {
            return instance;
        }

    }
}
