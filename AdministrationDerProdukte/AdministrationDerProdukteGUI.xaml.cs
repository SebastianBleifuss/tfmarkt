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

namespace AdministrationDerProdukte
{
    /// <summary>
    /// Interaktionslogik für AdministrationDerProdukteGUI.xaml
    /// </summary>
    public partial class AdministrationDerProdukteGUI : Window
    {
        public AdministrationDerProdukteGUI()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hinzufuegen fenster = new Hinzufuegen();
            fenster.ShowDialog();
        }
    }
}
