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
using xmlserializer.Models.Products;
using xmlserializer.Models.Calculations;

namespace tfMarktMain.Tapetenkalkulation
{
    /// <summary>
    /// Interaktionslogik für TapetenkalkulationGUI.xaml
    /// </summary>
    public partial class TapetenkalkulationGUI : Window
    {
        private xmlserializer.Models.Calculations.Tapetenkalkulation kalkulation;
        private List<Product> productList;

        public TapetenkalkulationGUI()
        {
            InitializeComponent();
            kalkulation = new xmlserializer.Models.Calculations.Tapetenkalkulation();
            holeTapetenListe();
        }


        private void btnRollenBerechnen_Click(object sender, RoutedEventArgs e)
        {
            txbErgebnis.Visibility = System.Windows.Visibility.Visible;
            //kalkulation.Amount = kalkulation.rollenBerechnen(gewaehlteTapete());
        }

        private void holeTapetenListe() 
        {
            productList = xmlserializer.xmlserializer.deserializeAllProducts();
            foreach (Product tapete in productList) 
            {
                if (tapete.GetType().Equals(typeof(Tapete)))
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = tapete.getArtikelbezeichnung();
                    item.Name = "_" + tapete.getArtikelnummer().ToString();
                    tapetenComboBox.Items.Add(item);
                } 
                if(tapete.GetType().Equals(typeof(Hilfsmittel)))
                {
                    if(tapete.getArtikelbezeichnung().Equals("Tapetenkleister"))
                    {
                        this.kalkulation.setHilfsmittel((Hilfsmittel)tapete);
                    }
                }
            }
        }
        public xmlserializer.Models.Calculations.Tapetenkalkulation getKalkulation()
        {
            kalkulation.tapete = gewaehlteTapete();
            return this.kalkulation;
        }

        public void setKalkulation(xmlserializer.Models.Calculations.Tapetenkalkulation kalkulation) 
        {
            Console.WriteLine(kalkulation);
            if (kalkulation != null)
            {
                this.kalkulation = kalkulation;
                txtKalkulationsBeschreibung.Text = kalkulation.Description;
                if (kalkulation.Width != 0 && kalkulation.Length != 0)
                {
                    txtBreite.Text = kalkulation.Width.ToString();
                    txtLaenge.Text = kalkulation.Length.ToString();
                }
                else
                {
                    txtBreite.Text = "0";
                    txtLaenge.Text = "0";
                }
                for (int i = 0; i < tapetenComboBox.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)tapetenComboBox.Items[i];
                    if (item.Content.ToString() == kalkulation.tapete.getArtikelbezeichnung())
                    {
                        tapetenComboBox.SelectedIndex = i;
                    }
                }
            }
        }


        private void txtKalkulationsBeschreibung_TextChanged(object sender, TextChangedEventArgs e)
        {
            String beschreibung = txtKalkulationsBeschreibung.Text.Replace(' ', '_');
            kalkulation.Description = beschreibung;
        }

        private Tapete gewaehlteTapete()
        {
            int artikelnummer;
            productList = xmlserializer.xmlserializer.deserializeAllProducts();
            ComboBoxItem item = (ComboBoxItem)tapetenComboBox.SelectedItem;
            if (item !=null && item.Name != null)
            {
                artikelnummer = Convert.ToInt32(item.Name.TrimStart('_'));
                foreach (Product tapete in productList)
                {
                    if (tapete.GetType().Equals(typeof(Tapete)))
                    {
                        if (artikelnummer == tapete.getArtikelnummer())
                        {
                            return (Tapete)tapete;
                        }
                    }
                }
            }
            return null;
        }

        private void ergebnisAusgabeAendern() 
        {
            if (tapetenComboBox.SelectedIndex > 0)
            {
                if (!String.IsNullOrWhiteSpace(txtBreite.Text) && !String.IsNullOrWhiteSpace(txtLaenge.Text))
                {
                    this.kalkulation.Length=Convert.ToDecimal(txtLaenge.Text);
                    this.kalkulation.Width=Convert.ToDecimal(txtBreite.Text);
                    int rollen = this.kalkulation.rollenBerechnen(gewaehlteTapete());
                    lblRollen.Content = rollen;
                    lblGesamt.Content = this.kalkulation.getGesamtpreis(gewaehlteTapete(), rollen);
                    lblKleister.Content = this.kalkulation.getKleistermenge();
                }
            }
            else { Console.WriteLine("SelectBox nicht gefüllt."); }
            
        }

        private void tapetenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kalkulation != null)
            {
                ergebnisAusgabeAendern();
            }
        }

        private void txtGroesse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (kalkulation != null)
            {
                ergebnisAusgabeAendern();
            }
        }
    }
}
