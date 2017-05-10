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

        public TapetenkalkulationGUI()
        {
            InitializeComponent();
            kalkulation = new Tapetenkalkulation();
            holeTapetenListe();
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {

            FlaecheBerechnen FlaecheBerechnenFenster = new  FlaecheBerechnen();
            FlaecheBerechnenFenster.ShowDialog();
            decimal flaeche = Math.Round(Convert.ToDecimal(FlaecheBerechnenFenster.getBreite()) * Convert.ToDecimal(FlaecheBerechnenFenster.getLaenge()));
            txtGroesse.Text = flaeche.ToString();
        }

        private void btnRollenBerechnen_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
