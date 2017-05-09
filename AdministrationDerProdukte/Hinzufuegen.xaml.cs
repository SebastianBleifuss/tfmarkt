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
using xmlserializer.Models;

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
                lblOptionOne.Content = "Länge Tapetenrolle (in m)";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.TextChanged += checkDecimal_TextChanged;

                lblOptionTwo.Content = "Tapetenbreite (in m)";
                lblOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.TextChanged += checkDecimal_TextChanged;

                lblOptionThree.Content = "Rapport (in m)";
                lblOptionThree.Visibility = Visibility.Visible;
                txtOptionThree.Visibility = Visibility.Visible;
                txtOptionThree.TextChanged += checkDecimal_TextChanged;
            }
            else if (sender == rbFliese)
            {
                lblOptionOne.Content = "Länge (in cm)";
                lblOptionOne.Visibility = Visibility.Visible;
                txtOptionOne.TextChanged += checkDecimal_TextChanged;

                lblOptionTwo.Content = "Breite (in cm)";
                lblOptionTwo.Visibility = Visibility.Visible;
                txtOptionTwo.TextChanged += checkDecimal_TextChanged;

                lblOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.Visibility = Visibility.Hidden;
                txtOptionThree.TextChanged -= checkDecimal_TextChanged;
            }
            else if (sender == rbHilfsmittel)
            {
                lblOptionOne.Content = "Ergiebigkeit (in m²)";
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
                btnSpeichern.IsEnabled = true;
            }
            else
            {
                txtSender.Foreground = Brushes.Red;
                btnSpeichern.IsEnabled = false;
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbHilfsmittel.IsChecked)
            {
                speicherHilfsmittel();
            }
            else if ((bool)rbFliese.IsChecked)
            {
                speicherFliese();
            }
            else if ((bool)rbTapete.IsChecked)
            {
                speicherTapete();
            }
            this.Close();
        }

        private void speicherHilfsmittel()
        {
            Hilfsmittel neuesHilfsmittel = new Hilfsmittel(Convert.ToInt32(txtArtikelnummer.Text), txtArtikelbezeichnung.Text, Convert.ToDecimal(txtOptionOne.Text), Convert.ToDecimal(txtPreis.Text));
            xmlserializer.xmlserializer.serialize(neuesHilfsmittel);
        }

        private void speicherFliese()
        {
            Fliese neueFliese = new Fliese(Convert.ToInt32(txtArtikelnummer.Text), txtArtikelbezeichnung.Text, Convert.ToDecimal(txtOptionOne.Text), Convert.ToDecimal(txtOptionTwo.Text), Convert.ToDecimal(txtPreis.Text));
            xmlserializer.xmlserializer.serialize(neueFliese);
        }

        private void speicherTapete()
        {
            Tapete neueTapete = new Tapete(Convert.ToInt32(txtArtikelnummer.Text), txtArtikelbezeichnung.Text, Convert.ToDecimal(txtOptionOne.Text), Convert.ToDecimal(txtOptionTwo.Text), Convert.ToDecimal(txtOptionThree.Text), Convert.ToDecimal(txtPreis.Text));
            xmlserializer.xmlserializer.serialize(neueTapete);
        }

        public void setProdukt(Product produkt)
        {
            Type produktTyp = produkt.getProductType();
            if (produktTyp.Equals(typeof(Fliese)))
            {
                loadGUIFliese((Fliese)produkt);
            }
            else if (produktTyp.Equals(typeof(Tapete)))
            {
                loadGUITapete((Tapete)produkt);
            }
            else if (produktTyp.Equals(typeof(Hilfsmittel)))
            {
                loadGUIHilfsmittel((Hilfsmittel)produkt);
            }
        }

        public void setArtikelnummerBeimHinzufuegenAufruf(int artikelnummer)
        {
            txtArtikelnummer.Text = artikelnummer + "";
        }

        private void loadGUIFliese(Fliese fliese)
        {
            rbFliese.IsChecked = true;
            txtArtikelbezeichnung.Text = fliese.getArtikelbezeichnung();
            txtArtikelnummer.Text = fliese.getArtikelnummer().ToString();
            txtPreis.Text = fliese.getPreis().ToString();
            txtOptionOne.Text = fliese.Laenge.ToString();
            txtOptionTwo.Text = fliese.Breite.ToString();
        }

        private void loadGUITapete(Tapete tapete)
        {
            rbTapete.IsChecked = true;
            txtArtikelnummer.Text = tapete.getArtikelnummer().ToString();
            txtArtikelbezeichnung.Text = tapete.getArtikelbezeichnung();
            txtPreis.Text = tapete.getPreis().ToString();
            txtOptionTwo.Text = tapete.Breite.ToString();
            txtOptionThree.Text = tapete.Rapport.ToString();
        }

        private void loadGUIHilfsmittel(Hilfsmittel hilfsmittel)
        {
            rbHilfsmittel.IsChecked = true;
            txtArtikelbezeichnung.Text = hilfsmittel.getArtikelbezeichnung();
            txtArtikelnummer.Text = hilfsmittel.getArtikelnummer().ToString();
            txtPreis.Text = hilfsmittel.getPreis().ToString();
            txtOptionOne.Text = hilfsmittel.Ergiebigkeit.ToString();
        }
    }
}
