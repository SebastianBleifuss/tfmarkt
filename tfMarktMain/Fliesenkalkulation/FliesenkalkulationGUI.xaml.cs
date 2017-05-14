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
using System.Collections.ObjectModel;

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
        private Fliesenkalkulation kalkulation;
        private ObservableCollection<Kalkulationsanzeige> dataGridSource;
        
        public FliesenkalkulationGUI()
        {
            this.fliesenliste = new List<Fliese>();
            InitializeComponent();
            fuelleComboBox();
            dgAnzeigeDerKalkulation.Visibility = Visibility.Hidden;
            lblGesamtsumme.Visibility = Visibility.Hidden;
            lblAngebot.Visibility = Visibility.Hidden;
            dataGridSource = new ObservableCollection<Kalkulationsanzeige>();
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
            kalkulation = new Fliesenkalkulation(cbFliese.SelectedValue.ToString(), (bool)chkFliesenkleber.IsChecked, Convert.ToDecimal(txtGroesse.Text), fliesenliste, fugenfueller, fliesenkleber);
            dgAnzeigeDerKalkulation.Visibility = Visibility.Visible;
            lblGesamtsumme.Visibility = Visibility.Visible;
            lblAngebot.Visibility = Visibility.Visible;
            ladeKalkulationInDasGrid();
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

        public void ladeVorhandeneKalkulation(Calculation vorhandeneKalkulation) 
        {
            this.kalkulation = new Fliesenkalkulation(vorhandeneKalkulation.SelectedProduct.getArtikelbezeichnung(), this.fliesenliste,vorhandeneKalkulation.WithExtraProduct, (vorhandeneKalkulation.Length * vorhandeneKalkulation.Width), this.fugenfueller, this.fliesenkleber);
            dgAnzeigeDerKalkulation.Visibility = Visibility.Visible;
            lblGesamtsumme.Visibility = Visibility.Visible;
            ladeKalkulationInDasGrid();
            txtGroesse.Text = kalkulation.raumFlaeche + "";
            for (int i = 0; i < cbFliese.Items.Count; i++)
            {
                if (cbFliese.Items[i].ToString() == kalkulation.ausgewaehlteFliese.getArtikelbezeichnung())
                {
                    cbFliese.SelectedIndex = i;
                }
            }
        }

        private void ladeKalkulationInDasGrid()
        {
            decimal gesamtpreis = 0;
            
            Kalkulationsanzeige fliese = new Kalkulationsanzeige();
            fliese.Artikelbezeichnung = kalkulation.ausgewaehlteFliese.getArtikelbezeichnung();
            fliese.ArtNr = kalkulation.ausgewaehlteFliese.getArtikelnummer()+"";
            fliese.Einzelpreis = kalkulation.ausgewaehlteFliese.getPreis().ToString("C");
            fliese.Menge = kalkulation.anzahlFliesenPakete + "";
            fliese.Gesamtpreis = (kalkulation.ausgewaehlteFliese.getPreis() * (decimal)kalkulation.anzahlFliesenPakete).ToString("C");
            dataGridSource.Add(fliese);
            gesamtpreis += kalkulation.ausgewaehlteFliese.getPreis() * (decimal)kalkulation.anzahlFliesenPakete;

            Kalkulationsanzeige fugenfueller = new Kalkulationsanzeige();
            fugenfueller.Artikelbezeichnung = kalkulation.fugenfueller.getArtikelbezeichnung();
            fugenfueller.ArtNr = kalkulation.fugenfueller.getArtikelnummer() + "";
            fugenfueller.Einzelpreis = kalkulation.fugenfueller.getPreis().ToString("C");
            fugenfueller.Menge = kalkulation.anzahlFugenfueller + "";
            fugenfueller.Gesamtpreis = (kalkulation.fugenfueller.getPreis() * (decimal)kalkulation.anzahlFugenfueller).ToString("C");
            dataGridSource.Add(fugenfueller);
            gesamtpreis += kalkulation.fugenfueller.getPreis() * (decimal)kalkulation.anzahlFugenfueller;

            if (kalkulation.mitFliesenkleber)
            {
                Kalkulationsanzeige fliesenkleber = new Kalkulationsanzeige();
                fliesenkleber.Artikelbezeichnung = kalkulation.fliesenkleber.getArtikelbezeichnung();
                fliesenkleber.ArtNr = kalkulation.fliesenkleber.getArtikelnummer() + "";
                fliesenkleber.Einzelpreis = kalkulation.fliesenkleber.getPreis().ToString("C");
                fliesenkleber.Menge = kalkulation.anzahlFliesenkleber + "";
                fliesenkleber.Gesamtpreis = (kalkulation.fliesenkleber.getPreis() * (decimal)kalkulation.anzahlFliesenkleber).ToString("C");
                dataGridSource.Add(fliesenkleber);
                gesamtpreis += kalkulation.fliesenkleber.getPreis() * (decimal)kalkulation.anzahlFliesenkleber;
            }

            dgAnzeigeDerKalkulation.ItemsSource = dataGridSource;
            dgAnzeigeDerKalkulation.Items.Refresh();
            lblGesamtsumme.Content = "Gesamtsumme: " + gesamtpreis.ToString("C");
        }
    }

    class Kalkulationsanzeige
    {
        public string ArtNr { get; set; }
        public string Artikelbezeichnung { get; set; }
        public string Menge { get; set; }
        public string Einzelpreis { get; set; }
        public string Gesamtpreis { get; set; }
    }
}
