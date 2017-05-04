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
        private int fliesenTabs = 0;
        private int tapetenTabs = 0;
        private int gesamtTab = 0;
        private bool isCustomerChanged = true;
        private Customer SelectedCustomer;

        public MainWindow()
        {
            InitializeComponent();
            foreach (String CustomerInfo in Customer.getCustomerNames())
            {
                String[] CustomerInfoSet = CustomerInfo.Split('_');
                ComboBoxItem NewComboItem = new ComboBoxItem();
                NewComboItem.Content = CustomerInfoSet[1] +", " + CustomerInfoSet[2];
                NewComboItem.Selected += customer_selected;
                NewComboItem.ToolTip = CustomerInfoSet[0];
                CustomersBox.Items.Add(NewComboItem);
            }
            CustomersBox.SelectedIndex = 0;
            SelectedCustomer = new Customer();
        }

        private void customer_selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem CustomerItem = (ComboBoxItem)sender;
            SelectedCustomer = xmlserializer.xmlserializer.deserialize(CustomerItem.Content.ToString(), new Guid(CustomerItem.ToolTip.ToString()));

            String[] namen = SelectedCustomer.Name.Split(new[] { ", " }, StringSplitOptions.None);
            KundenNameTextbox.Text=namen[1];
            KundenNachnameTextbox.Text = namen[0];
            KundenNummerTextbox.Text = SelectedCustomer.Customernumber.ToString();

            CalculationListBox.Items.Clear();

            foreach (Calculation calc in SelectedCustomer.Calculations.Values)
            {
                CalculationListBox.Items.Add(calc);
            }
            //entferneAlleTabs
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
                SelectedCustomer = new Customer();
                KundenNameTextbox.Clear();
                KundenNachnameTextbox.Clear();
                KundenNummerTextbox.Text = SelectedCustomer.Customernumber.ToString();
            }
        }

        private void Save_Customer_Click(object sender, RoutedEventArgs e)
        {
            if(isCustomerChanged)
            {
                if (SelectedCustomer.Calculations.Count > 0)
                {
                    xmlserializer.xmlserializer.serialize(SelectedCustomer);
                }
                else {
                    MessageBox.Show("Keine Kalkualtionen zum speichern!");
                }
            }
        }


        private void Delete_Customer_Click(object sender, RoutedEventArgs e)
        {
            Customer.removeCustomer(SelectedCustomer);
            object selectedItem = CustomersBox.SelectedItem;
            CustomersBox.SelectedIndex = 0;
            CustomersBox.Items.Remove(selectedItem);
        }

        private void cmdBeenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void cmdFliesenAuf_Click(object sender, RoutedEventArgs e)
        {
            KalkulationsTab<Calculation> tab= neuerTab("Fliesen", "tabFliesenAnsicht", fliesenTabs);
            Frame tabFrame = new Frame();
            Fliesenkalkulation.FliesenkalkulationGUI ladeSeite = new Fliesenkalkulation.FliesenkalkulationGUI();
            tabFrame.Content = ladeSeite.Content;
            tab.Content = tabFrame;
            tab.Focus();
            fliesenTabs++;
        }

        private void cmdTapetenAuf_Click(object sender, RoutedEventArgs e)
        {
            KalkulationsTab<Calculation> tab= neuerTab("Tapeten", "tabTapetenAnsicht", tapetenTabs);
            Frame tabFrame = new Frame();
            Tapetenkalkulation.TapetenkalkulationGUI ladeSeite = new Tapetenkalkulation.TapetenkalkulationGUI();
            tabFrame.Content = ladeSeite.Content;
            tab.Content = tabFrame;
            tab.Focus();
            tapetenTabs++;
        }

        private void cmdGesamtbetragAuf_Click(object sender, RoutedEventArgs e)
        {
            //GUI für Gesamtübersicht fehlt noch
            if (gesamtTab == 0)
            {
                neuerTab("Gesamt", "tabGesamt", 0);
                gesamtTab++;
            }
            else
            {
                //Erneuern, da nur einmal Übersicht
            }
        }

        private KalkulationsTab<Calculation> neuerTab(String tabname, String tabBezeichnung, int anzahl)
        {
            KalkulationsTab<Calculation> tab = new KalkulationsTab<Calculation>();
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
            
            return tab;
        }

         private void Generate_TotalCalculation_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer.Calculations.Count > 0)
            {
                tfMarktMain.Export.PDFFactory.CustomerPDFDocument cpd = new tfMarktMain.Export.PDFFactory.CustomerPDFDocument(SelectedCustomer);
                cpd.showPDF();
                cpd.printPDF();
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

        private void KundenNameVeraendern_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectedCustomer.Name = KundenNachnameTextbox.Text + ", " + KundenNameTextbox.Text;
        }

        private void entferneAlleTabs() 
        {
            List<TabItem> tabListe = new List<TabItem>();
            ItemCollection a= tabAnsicht.Items;
            foreach(ItemsControl e in a)
            {
                Console.WriteLine(e.Name);
            }
            //tabAnsicht.Items.Remove();
        }

        private void cmdStarteAdministration_Click(object sender, RoutedEventArgs e)
        {
            //Schauen, wie man am Besten die Administartion reinbekommt
            MessageBox.Show("Hier sollte sich eigentlich die administration öffnen, aber C# ist bescheuert");
        }
      
    }
}
