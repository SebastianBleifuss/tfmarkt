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
using System.Windows.Shapes;
using xmlserializer.Models;


namespace tfMarktMain.Fliesenkalkulation
{
    /// <summary>
    /// Interaktionslogik für FlaecheBerechnen.xaml
    /// </summary>
    public partial class FlaecheBerechnen : Window
    {
        private Fliesenkalkulation kalkulation;

        public FlaecheBerechnen()
        {
            this.kalkulation = Fliesenkalkulation.getMe();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kalkulation.setFlaeche(Math.Round(Convert.ToDecimal(txtBreite.Text) * Convert.ToDecimal(txtLaenge.Text)));
            this.Hide();
        }

        private void DecimalAllower_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox derSender = (TextBox)sender;
            decimal temp;
            if (!Decimal.TryParse(derSender.Text, out temp))
            {
                derSender.Foreground = Brushes.Red;
                btnBerechnen.IsEnabled = false;
            }
            else
            {
                derSender.Foreground = Brushes.Black;
                btnBerechnen.IsEnabled = true;
            }
        }
    }
}
