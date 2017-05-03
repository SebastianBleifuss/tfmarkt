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
using System.Text.RegularExpressions;

namespace AdministrationDerProdukte
{
    /// <summary>
    /// Interaktionslogik für Hinzufuegen.xaml
    /// </summary>
    public partial class Hinzufuegen : Window
    {
        public Hinzufuegen()
        {
            InitializeComponent();
            txtArtikelnummer.IsReadOnly = true;
            rbTapete.IsChecked = true;
        }

        private void RadioButtons_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == rbTapete)
            {
                lblOptionOne.Content = "Länge Tapetenrolle";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.Visibility = Visibility.Visible;
                lblOptionTwo.Content = "Tapetenbreite";
                lblOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.Visibility = Visibility.Visible;
                lblOptionThree.Content = "Rapport";
                lblOptionThree.Visibility = Visibility.Visible;
                txtOptionThree.Visibility = Visibility.Visible;
            }
            else if (sender == rbFliese)
            {
                lblOptionOne.Content = "Länge";
                lblOptionOne.Visibility = Visibility.Visible;
                lblOptionTwo.Content = "Breite";
                lblOptionTwo.Visibility = Visibility.Visible;
                lblOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.Visibility = Visibility.Hidden;
            }
            else if (sender == rbFugenfüller || sender == rbFliesenkleber || sender == rbTapetenkleber)
            {
                lblOptionOne.Content = "Ergiebigkeit";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.Visibility = Visibility.Visible;
                lblOptionTwo.Visibility = Visibility.Hidden;
                txtOptionTwo.Visibility = Visibility.Hidden;
                lblOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.Visibility = Visibility.Hidden;
            }
        }



        private bool checkTxtPreis()
        {
            decimal temp;
            return Decimal.TryParse(txtPreis.Text, out temp);
        }

        private void checkDecimal_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtSender = (TextBox) sender;
            if (checkTxtPreis())
            {
                txtSender.Foreground = Brushes.Black;
            }
            else
            {
                txtSender.Foreground = Brushes.Red;
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
