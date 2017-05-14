using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using xmlserializer.Models;

namespace tfMarktMain
{
    class FliesenTab : TabItem
    {
        private Fliesenkalkulation.Fliesenkalkulation kalkulation;
        private Fliesenkalkulation.FliesenkalkulationGUI fliesenSeite;

        public FliesenTab()
        {
            kalkulation = new Fliesenkalkulation.Fliesenkalkulation();
            fliesenSeite = new Fliesenkalkulation.FliesenkalkulationGUI();
        }

        public Fliesenkalkulation.Fliesenkalkulation getKalkulation()
        {
            return this.kalkulation;
        }

        //Entfällt, wenn Tapetenkalkulation nicht mehr so geladen werden muss.
        public void setKalkulation(Fliesenkalkulation.Fliesenkalkulation kalkulation)
        {
            Console.WriteLine("Tapetentab");
            //fliesenSeite.setKalkulation(kalkulation);
            this.kalkulation = kalkulation;
        }

        public Fliesenkalkulation.FliesenkalkulationGUI getTapetenGUI()
        {
            return this.fliesenSeite;
        }

        public void setTapetenGUI(Fliesenkalkulation.FliesenkalkulationGUI GUI)
        {
            this.fliesenSeite = GUI;
        }


    }
}
