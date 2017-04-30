using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using xmlserializer.Models;

namespace tfMarktMain
{
    class KalkulationsTab<T> : TabItem
    {
        private Calculation kalkulation;

        public KalkulationsTab() 
        {
        }

        public KalkulationsTab(Calculation kalkulation)
        {
            this.kalkulation = kalkulation;
        }
    }
}
