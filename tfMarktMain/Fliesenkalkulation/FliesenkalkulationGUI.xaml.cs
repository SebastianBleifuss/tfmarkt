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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using xmlserializer.Models.Products;
using xmlserializer.Models;

namespace tfMarktMain.Fliesenkalkulation
{
    /// <summary>
    /// Interaktionslogik für FliesenkalkulationGUI.xaml
    /// </summary>
    public partial class FliesenkalkulationGUI : Window
    {
        private List<Fliese> fliesenliste;
        private Hilfsmittel fugenfueller;
        private Hilfsmittel fliesenkleber;
        
        public FliesenkalkulationGUI()
        {
            this.fliesenliste = new List<Fliese>();
            InitializeComponent();
            fuelleComboBox();
        }

        private void fuelleComboBox()
        {
            fuelleFliesenliste();
            foreach(Fliese fliese in fliesenliste) 
            {
                cbFliese.Items.Add(fliese.getArtikelbezeichnung());
            }
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {
            FlaecheBerechnen flaecheBerechnenFenster = new FlaecheBerechnen();
            flaecheBerechnenFenster.ShowDialog();
            txtGroesse.Text = flaecheBerechnenFenster.getFlaeche().ToString();
        }

        private void btnKalkulieren_Click(object sender, RoutedEventArgs e)
        {
            Fliesenkalkulation kalkulation = new Fliesenkalkulation(cbFliese.SelectedValue.ToString(), (bool)chkFliesenkleber.IsChecked, Convert.ToDecimal(txtGroesse.Text), fliesenliste, fugenfueller, fliesenkleber);
        }

        private void fuelleFliesenliste()
        {
            List<Product> alleProdukte = xmlserializer.xmlserializer.deserializeAllProducts();
            foreach (Product produkt in alleProdukte)
            {
                Type produktTyp = produkt.getProductType();
                if (produktTyp.Equals(typeof(Fliese)))
                {
                    fliesenliste.Add((Fliese)produkt);
                }
                else if(produktTyp.Equals(typeof(Hilfsmittel)))
                {
                    Hilfsmittel temp = (Hilfsmittel)produkt;
                    if (temp.getArtikelbezeichnung() == "Fugenfüller")
                    {
                        this.fugenfueller = temp;
                    }
                    else if (temp.getArtikelbezeichnung() == "Fliesenkleber")
                    {
                        this.fliesenkleber = temp;
                    }
                }
            }
        }
    }
}
