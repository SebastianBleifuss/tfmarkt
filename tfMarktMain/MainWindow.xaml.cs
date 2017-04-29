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
using tfMarktMain.Export;

namespace tfMarktMain
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int fliesenTabs = 0;
        private static int tapetenTabs = 0;

        public MainWindow()
        {
            InitializeComponent();
            foreach (String Customername in Customer.getCustomerNames())
            {
                ComboBoxItem NewComboItem = new ComboBoxItem();
                NewComboItem.Content = Customername;
                NewComboItem.Selected += customer_selected;
                CustomersBox.Items.Add(NewComboItem);
            }
            CustomersBox.SelectedIndex = 0;
            SelectedCustomer = new Customer();
        }

        private Customer SelectedCustomer;

        private void customer_selected(object sender, RoutedEventArgs e)
        {
            SelectedCustomer = xmlserializer.xmlserializer.deserialize(((ComboBoxItem)sender).Content.ToString());
            CalculationListBox.Items.Clear();

            foreach (Calculation calc in SelectedCustomer.Calculations.Values)
            {
                CalculationListBox.Items.Add(calc);
            }
        }

        private void CalculationListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CalculationListBox.SelectedIndex != -1)
            {
                Calculation calc = SelectedCustomer.Calculations.Values.ToArray()[CalculationListBox.SelectedIndex];
                MessageBox.Show(calc.Identifier.ToString() + " - " + calc.Description);
                MessageBox.Show(calc.SelectedProduct.getArtikelbezeichnung());
            }
        }

        private void CustomersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersBox.SelectedIndex == 0)
            {
                CalculationListBox.Items.Clear();
            }
        }

        private void Save_Customer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer != null)
            {
                xmlserializer.xmlserializer.serialize(SelectedCustomer);
            }
        }

        private void cmdBeenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void cmdFliesenAuf_Click(object sender, RoutedEventArgs e)
        {
            neuerTab("Fliesen", "tabFliesenAnsicht", fliesenTabs);
        }

        private void cmdTapetenAuf_Click(object sender, RoutedEventArgs e)
        {
            neuerTab("Tapeten", "tabTapetenAnsicht", tapetenTabs);
        }

        private void cmdGesamtbetragAuf_Click(object sender, RoutedEventArgs e)
        {
            neuerTab("Gesamt", "tabGesamt", 0);
        }

        private void neuerTab(String tabname, String tabBezeichnung, int anzahl)
        {
            TabItem tab = new TabItem();
            if (anzahl > 0)
            {
                tab.Name = tabname + anzahl;
                tab.Header = tabname + anzahl;
            }
            else
            {
                tab.Name = tabname;
                tab.Header = tabname;
            }
            tabAnsicht.Items.Add(tab);
        }

        private void Generate_TotalCalculation_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer.Calculations.Count > 0)
            {
                tfMarktMain.Export.PDFFactory.CustomerPDFDocument cpd = new tfMarktMain.Export.PDFFactory.CustomerPDFDocument(SelectedCustomer);
                cpd.showPDF();
                cpd.printPDF(false);
                //cpd.savePDF(true);
            }
            else {
                MessageBox.Show("Keine Kalkulationen vorhanden!");
            }
        }

        private Guid generateGuid()
        {
            // Prüfen, ob Kunde schon Kalkulation mit GUID hat.
            Guid NewGuid = Guid.NewGuid();
            Calculation tmp;
            while (SelectedCustomer.Calculations.TryGetValue(NewGuid, out tmp))
            {
                NewGuid = Guid.NewGuid();
            }
            return NewGuid;
        }
      
    }
}
