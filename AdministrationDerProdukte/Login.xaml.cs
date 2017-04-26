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

namespace AdministrationDerProdukte
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private String passwort;
        
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtBenutzer.Text == "admin" && txtPasswort.Password == "admin")
            {
                AdministrationDerProdukteGUI starti = new AdministrationDerProdukteGUI();
                starti.Show();
                this.Hide();
            }
        }

        private void txtPasswort_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
