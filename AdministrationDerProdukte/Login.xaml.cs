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
        private bool RichtigeEingabe;
        
        public Login()
        {
            InitializeComponent();
            RichtigeEingabe = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtBenutzer.Text == "admin" && txtPasswort.Password == "admin")
            {
                RichtigeEingabe = true;
                this.Hide();
            }
        }

        private void txtPasswort_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

        public bool isRichtigeEingabe()
        {
            return RichtigeEingabe;
        }
    }
}
