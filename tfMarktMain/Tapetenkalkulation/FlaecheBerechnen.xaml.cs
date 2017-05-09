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

namespace tfMarktMain.Tapetenkalkulation
{
    /// <summary>
    /// Interaktionslogik für FlaecheBerechnen.xaml
    /// </summary>
    public partial class FlaecheBerechnen : Window
    {
        private Tapetenkalkulation kalkulation;

        public FlaecheBerechnen()
        {
            InitializeComponent();
            kalkulation = new Tapetenkalkulation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kalkulation.setBreite(Convert.ToDecimal(txtBreite.Text));
            kalkulation.setLaenge(Convert.ToDecimal(txtLaenge.Text));
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

        public decimal getLaenge() 
        {
            return Convert.ToDecimal(txtLaenge.Text);
        }

        public decimal getBreite() 
        {
            return Convert.ToDecimal(txtBreite.Text);
        }
    }
}
