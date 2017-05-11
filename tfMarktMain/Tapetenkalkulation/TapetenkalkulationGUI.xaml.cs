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

namespace tfMarktMain.Tapetenkalkulation
{
    /// <summary>
    /// Interaktionslogik für TapetenkalkulationGUI.xaml
    /// </summary>
    public partial class TapetenkalkulationGUI : Window
    {
        private Tapetenkalkulation kalkulation;
        private List<Product> productList;
        private bool istLaengeBreiteBekannt = false;

        public TapetenkalkulationGUI()
        {
            InitializeComponent();
            kalkulation = new Tapetenkalkulation();
            holeTapetenListe();
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {
            istLaengeBreiteBekannt = true;
            FlaecheBerechnen FlaecheBerechnenFenster = new  FlaecheBerechnen();
            FlaecheBerechnenFenster.ShowDialog();
            decimal flaeche = Math.Round(Convert.ToDecimal(FlaecheBerechnenFenster.getBreite()) * Convert.ToDecimal(FlaecheBerechnenFenster.getLaenge()));
            txtGroesse.Text = flaeche.ToString();
            this.kalkulation.setLaenge(FlaecheBerechnenFenster.getLaenge());
            this.kalkulation.setBreite(FlaecheBerechnenFenster.getBreite());
        }

        private void btnRollenBerechnen_Click(object sender, RoutedEventArgs e)
        {
            txbErgebnis.Visibility = System.Windows.Visibility.Visible;
            kalkulation.Amount = kalkulation.rollenBerechnen(gewaehlteTapete());
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
            }
        }
        public Tapetenkalkulation getKalkulation()
        {
            kalkulation.SelectedProduct = gewaehlteTapete();
            return this.kalkulation;
        }

        public void setKalkulation(Tapetenkalkulation kalkulation) 
        {
            Console.WriteLine("Kalkulation");
            this.kalkulation = kalkulation;
            Console.WriteLine(this.kalkulation.getBreite());
            Console.WriteLine(this.kalkulation.getLaenge());
            txtGroesse.Text = kalkulation.getFlaeche().ToString();
            txtKalkulationsBeschreibung.Text = kalkulation.Description;
        }

        private void txtKalkulationsBeschreibung_TextChanged(object sender, TextChangedEventArgs e)
        {
            kalkulation.Description = txtKalkulationsBeschreibung.Text;
        }

        private Tapete gewaehlteTapete()
        {
            int artikelnummer;
            productList = xmlserializer.xmlserializer.deserializeAllProducts();
            ComboBoxItem item = (ComboBoxItem)tapetenComboBox.SelectedItem;
            if (item.Name != null)
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
            if (tapetenComboBox.SelectedItem != null)
            {
                Console.WriteLine("SelectBox gefüllt.");
                if (this.kalkulation.getLaenge() == 0 || this.kalkulation.getBreite() == 0 || !istLaengeBreiteBekannt)
                {
                    lblRollen.Content = kalkulation.rollenBerechnen(gewaehlteTapete());
                    lblGesamt.Content = kalkulation.getGesamtpreis();
                    lblKleister.Content = kalkulation.getKleistermenge();
                }
                else if(txtGroesse.Text!=null)
                {
                    lblRollen.Content = kalkulation.rollenBerechnen(gewaehlteTapete());
                    lblGesamt.Content = kalkulation.getGesamtpreis();
                    lblKleister.Content = kalkulation.getKleistermenge();
                }
            }
            else { Console.WriteLine("SelectBox nicht gefüllt."); }
            
        }

        private void tapetenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ergebnisAusgabeAendern();
        }

        private void txtGroesse_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ergebnisAusgabeAendern();
        }
    }
}
