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

            Window FlaecheBerechnenFenster = new  FlaecheBerechnen();
            FlaecheBerechnenFenster.ShowDialog();
            txtGroesse.Text = kalkulation.getFlaeche().ToString();
        }

        private void btnRollenBerechnen_Click(object sender, RoutedEventArgs e)
        {
            Tapete tapete = new Tapete(1, "Testtapete", 0.53m,10.05m,0.72m, 2.99m);
            kalkulation.rollenBerechnen(tapete, 2.65m, 5.00m);

        }

        private void holeTapetenListe() 
        {
            //productList = xmlserializer.xmlserializer.deserializeAllProducts();
            //foreach (Product tapete in productList) 
            //{
                //MessageBox.Show(tapete.ToString());
                //MessageBox.Show(tapete.GetType().ToString());
               // if (tapete.GetType().ToString().Equals("Tapete")) 
               // {

               // }
                //Hilfmittel für Tapete auch ziehen
            //}
            for (int i = 1; i <= 10; i++) 
            {
                ComboBoxItem item= new ComboBoxItem();
                item.Content="Tapete"+i;//Tapetenbezeichnung
                item.Name = "_" + i;   //guid des Produkts 
                tapetenComboBox.Items.Add(item);
            }
        }
    }
}
