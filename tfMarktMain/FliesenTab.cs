using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using xmlserializer.Models;
using xmlserializer.Models.Products;

namespace tfMarktMain
{
    class FliesenTab : TabItem
    {
        private xmlserializer.Models.Calculations.Fliesenkalkulation kalkulation;
        private Fliesenkalkulation.FliesenkalkulationGUI fliesenSeite;

        public FliesenTab()
        {
            fliesenSeite = new Fliesenkalkulation.FliesenkalkulationGUI();
            kalkulation = fliesenSeite.getFliesenKalkulation();
            
        }

        public xmlserializer.Models.Calculations.Fliesenkalkulation getKalkulation()
        {
            return this.kalkulation;
        }

        //Entfällt, wenn Tapetenkalkulation nicht mehr so geladen werden muss.
        public void setKalkulation(xmlserializer.Models.Calculations.Fliesenkalkulation kalkulation)
        {
            //fliesenSeite.setKalkulation(kalkulation);
            this.kalkulation = kalkulation;
        }

        public Fliesenkalkulation.FliesenkalkulationGUI getFliesenGUI()
        {
            return this.fliesenSeite;
        }

        public void setFliesenGUI(Fliesenkalkulation.FliesenkalkulationGUI GUI)
        {
            this.fliesenSeite = GUI;
        }


    }
}
