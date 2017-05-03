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
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {
            Window FlaecheBerechnenFenster = new FlaecheBerechnen();
            FlaecheBerechnenFenster.ShowDialog();
            txtGroesse.Text = kalkulation.getFlaeche().ToString();
        }
    }
}
