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
using xmlserializer.Models.Products;

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
                txtOptionOne.TextChanged += checkDecimal_TextChanged;

                lblOptionTwo.Content = "Tapetenbreite";
                lblOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.TextChanged += checkDecimal_TextChanged;

                lblOptionThree.Content = "Rapport";
                lblOptionThree.Visibility = Visibility.Visible;
                txtOptionThree.Visibility = Visibility.Visible;
                txtOptionThree.TextChanged += checkDecimal_TextChanged;
            }
            else if (sender == rbFliese)
            {
                lblOptionOne.Content = "Länge";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.TextChanged += checkDecimal_TextChanged;

                lblOptionTwo.Content = "Breite";
                lblOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.TextChanged += checkDecimal_TextChanged;

                lblOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.TextChanged -= checkDecimal_TextChanged;
            }
            else if (sender == rbFugenfüller || sender == rbFliesenkleber || sender == rbTapetenkleber)
            {
                lblOptionOne.Content = "Ergiebigkeit";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.TextChanged += checkDecimal_TextChanged;

                lblOptionTwo.Visibility = Visibility.Hidden;
                txtOptionTwo.Visibility = Visibility.Hidden;
                txtOptionTwo.TextChanged -= checkDecimal_TextChanged;

                lblOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.TextChanged -= checkDecimal_TextChanged;
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

        private void speicherHilfsmittel()
        {
            Hilfsmittel neuesHilfsmittel = new Hilfsmittel(txtArtikelbezeichnung.Text, Convert.ToDecimal(txtOptionOne.Text), Convert.ToDecimal(txtPreis.Text));
            xmlserializer.xmlserializer.serialize(neuesHilfsmittel);
        }
    }
}
