using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using xmlserializer.Models.Products;

namespace tfMarktMain.Fliesenkalkulation
{
    /// <summary>
    /// Interaktionslogik für FliesenkalkulationGUI.xaml
    /// </summary>
    public partial class FliesenkalkulationGUI : Window
    {
        private Fliesenkalkulation kalkulation;
        
        public FliesenkalkulationGUI()
        {
            InitializeComponent();
            kalkulation = new Fliesenkalkulation();
            fuelleComboBox();
        }

        private void fuelleComboBox()
        {
            foreach(Fliese fliese in kalkulation.getFliesenListe()) 
            {
                cbFliese.Items.Add(fliese.getArtikelbezeichnung());
            }
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {
            Window flaecheBerechnenFenster = new FlaecheBerechnen();
            flaecheBerechnenFenster.ShowDialog();
            txtGroesse.Text = kalkulation.getFlaeche().ToString();
        }

        private void btnKalkulieren_Click(object sender, RoutedEventArgs e)
        {
            kalkulation.berechneFliesen(cbFliese.SelectedItem + "");
        }
    }
}
