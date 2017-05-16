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
using xmlserializer.Models;
using System.Collections.ObjectModel;

namespace AdministrationDerProdukte
{
    /// <summary>
    /// Interaktionslogik für AdministrationDerProdukteGUI.xaml
    /// </summary>
    public partial class AdministrationDerProdukteGUI : Window
    {
        private ObservableCollection<GridItem> gridListe;
        private List<Product> productList;
        private GridItem selectedItem;

        public AdministrationDerProdukteGUI()
        {
            Login loginFenster = new Login();
            loginFenster.ShowDialog();
            if (loginFenster.isRichtigeEingabe())
            {
                InitializeComponent();
                AblaufProduktListeNeuLaden();
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            Hinzufuegen fenster = new Hinzufuegen();
            fenster.setArtikelnummerBeimHinzufuegenAufruf(gridListe.Last().ArtikelNr + 1);
            fenster.ShowDialog();
            AblaufProduktListeNeuLaden();
        }

        private void fuelleProduktListe()
        {
            productList = xmlserializer.xmlserializer.deserializeAllProducts();
            
            foreach (Product p in productList)
            {
                gridListe.Add(new GridItem()
                {
                    ArtikelNr = p.getArtikelnummer(),
                    Name = p.getArtikelbezeichnung(),
                    Preis = p.getPreis().ToString("C"),
                });
            }

        }

        private void AblaufProduktListeNeuLaden()
        {
            gridListe = new ObservableCollection<GridItem>();
            fuelleProduktListe();
            ProdukteGrid.ItemsSource = gridListe;
            ProdukteGrid.Items.Refresh();
        }

        private void ItemAuswahl(object sender, MouseButtonEventArgs e)
        {
            if (selectedItem != null)
            {
                Product selectedProduct = null;
                foreach (Product p in productList)
                {
                    if (p.getArtikelnummer() == selectedItem.ArtikelNr)
                    {
                            selectedProduct = p;
                    }
                }
                Hinzufuegen gui = new Hinzufuegen();
                gui.setProdukt(selectedProduct);
                gui.setIsAenderung();
                gui.ShowDialog();
                AblaufProduktListeNeuLaden();
            }
        }

        class GridItem
        {
            public int ArtikelNr { get; set; }
            public String Name { get; set; }
            public String Preis { get; set; }
        }

        private void ProdukteGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedItem = (GridItem)ProdukteGrid.SelectedItem;
        }


    }
}
