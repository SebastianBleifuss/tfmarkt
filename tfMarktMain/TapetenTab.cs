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
        private Tapetenkalkulation.Tapetenkalkulation kalkulation;
        private Tapetenkalkulation.TapetenkalkulationGUI tapetenSeite;

        public TapetenTab() 
        {
            kalkulation = new Tapetenkalkulation.Tapetenkalkulation();
            tapetenSeite = new Tapetenkalkulation.TapetenkalkulationGUI();
        }

        public Tapetenkalkulation.Tapetenkalkulation getKalkulation() 
        {
            return this.kalkulation;
        }

        public void setKalkulation(Tapetenkalkulation.Tapetenkalkulation kalkulation) 
        {
            this.kalkulation = kalkulation;
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
