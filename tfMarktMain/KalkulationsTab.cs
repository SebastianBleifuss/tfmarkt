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
        private T kalkulation;

        public KalkulationsTab() 
        {
        }

        public KalkulationsTab(T kalkulation)
        {
            this.kalkulation = kalkulation;
        }

        public T getKalkulation() {
            return kalkulation;
        }
    }
}
