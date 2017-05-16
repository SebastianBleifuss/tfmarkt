using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using xmlserializer.Models;

namespace tfMarktMain
{
    class TapetenTab : TabItem
    {
        private xmlserializer.Models.Calculations.Tapetenkalkulation kalkulation;
        private Tapetenkalkulation.TapetenkalkulationGUI tapetenSeite;

        public TapetenTab() 
        {
            kalkulation = new xmlserializer.Models.Calculations.Tapetenkalkulation();
            tapetenSeite = new Tapetenkalkulation.TapetenkalkulationGUI();
        }

        public xmlserializer.Models.Calculations.Tapetenkalkulation getKalkulation() 
        {
            return this.kalkulation;
        }

        public void setKalkulation(xmlserializer.Models.Calculations.Tapetenkalkulation kalkulation) 
        {
            if (kalkulation != null)
            {
                Console.WriteLine("Tapetentab");
                tapetenSeite.setKalkulation(kalkulation);
                this.kalkulation = kalkulation;
            }
        }

        public Tapetenkalkulation.TapetenkalkulationGUI getTapetenGUI()
        {
            return this.tapetenSeite;
        }

        public void setTapetenGUI(Tapetenkalkulation.TapetenkalkulationGUI GUI) 
        {
            this.tapetenSeite = GUI;
        }


    }
}
