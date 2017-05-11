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
using xmlserializer.Models.Products;
using tfMarktMain.Export;
using AdministrationDerProdukte;


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
        //List<KalkulationsTab<Calculation>> tabList;
        List<TabItem> tabList;
        private PDFFactory.CustomerPDFDocument GesamtkalkualtionsPDF;
        private TabItem GesamtKalkulationsTab;

        public MainWindow()
        {
            InitializeComponent();
            //tabList = new List<KalkulationsTab<Calculation>>();
            tabList = new List<TabItem>();
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


            KundenNachnameTextbox.Text = namen[0];
            KundenNameTextbox.Text = namen[1];
            KundenNummerTextbox.Text = SelectedCustomer.Customernumber.ToString();

            CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;

        }

        private void CalculationListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CalculationListBox.SelectedIndex != -1)
            {
                Calculation calc = SelectedCustomer.Calculations.Values.ToArray()[CalculationListBox.SelectedIndex];
                
                if (calc.CalculationType.Equals(typeof(Tapetenkalkulation.Tapetenkalkulation))) 
                {
                    Console.WriteLine("Tapetenkalkulation");
                    Console.WriteLine((Tapetenkalkulation.Tapetenkalkulation)calc);
                    Tapetenkalkulation.Tapetenkalkulation tapCalc = (Tapetenkalkulation.Tapetenkalkulation)calc;
                    TapetenTab tab= neueTapetenKalkulationTab(tapCalc.Description, tapCalc);
                }
                if (calc.CalculationType.Equals(typeof(Fliesenkalkulation.Fliesenkalkulation))) 
                {
                    Console.WriteLine("Fliesenkalkulation");
                }
                MessageBox.Show(calc.Identifier.ToString() + " - " + calc.Description);
                MessageBox.Show(calc.SelectedProduct.getArtikelbezeichnung());
            }
        }

        private void CustomersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CustomersBox.SelectedIndex == 0)
            {
                SelectedCustomer = new Customer();
                KundenNameTextbox.Clear();
                KundenNachnameTextbox.Clear();
                KundenNummerTextbox.Text = SelectedCustomer.Customernumber.ToString();
                CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;
            }
            entferneAlleTabs();
        }

        private void Save_Customer_Click(object sender, RoutedEventArgs e)
        {
            if (isCustomerChanged)
            {
                if (SelectedCustomer.Calculations.Count > 0)
                {
                    xmlserializer.xmlserializer.serialize(SelectedCustomer);

                    if (CustomersBox.SelectedIndex != 0)
                    {
                        CustomersBox.Items.Remove(CustomersBox.SelectedItem);
                    }

                    ComboBoxItem NewComboItem = new ComboBoxItem();
                    NewComboItem.Content = SelectedCustomer.Name;
                    NewComboItem.Selected += customer_selected;
                    NewComboItem.ToolTip = SelectedCustomer.Customernumber;
                    CustomersBox.Items.Add(NewComboItem);

                    CustomersBox.SelectedItem = NewComboItem;

                }
                else
                {
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
            neueTapetenKalkulationTab("Tapete", new Tapetenkalkulation.Tapetenkalkulation());
        }

        private TapetenTab neueTapetenKalkulationTab(String tabname, Tapetenkalkulation.Tapetenkalkulation kalkulation) 
        {
            TapetenTab tab = new TapetenTab();
            if (tapetenTabs > 0)
            {
                tab.Name = tabname + tapetenTabs;
                tab.Header = tabname + tapetenTabs;
            }
            else
            {
                tab.Name = tabname;
                tab.Header = tabname;
            }
            ContextMenu TabContextMenue = new ContextMenu();
            MenuItem SpeicherItem = new MenuItem();
            SpeicherItem.Header = "Speichern";
            SpeicherItem.Click += SpeicherItem_Click;
            SpeicherItem.Tag = tab;
            MenuItem VerwerfItem = new MenuItem();
            VerwerfItem.Header = "Verwerfen";
            VerwerfItem.Click += VerwerfItem_Click;
            VerwerfItem.Tag = tab;
            TabContextMenue.Items.Add(SpeicherItem);
            TabContextMenue.Items.Add(VerwerfItem);
            tab.ContextMenu = TabContextMenue;
            tabList.Add(tab);
            tabAnsicht.Items.Add(tab);
            tabAnsicht.SelectedItem = tab;
            Frame tabFrame = new Frame();
            tab.setKalkulation(kalkulation);
            tabFrame.Content = tab.getTapetenGUI().Content;
            tab.Content = tabFrame;
            tab.Focus();
            tapetenTabs++;
            return tab;
        }

        private void cmdGesamtbetragAuf_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer.Calculations.Count > 0) { 
                if (gesamtTab == 0)
                {
                    gesamtTab++;
                    GesamtkalkualtionsPDF = new PDFFactory.CustomerPDFDocument(SelectedCustomer);
                    TabItem PDFTab = GesamtkalkualtionsPDF.displayPDF();

                    ContextMenu PDFTabContextMenue = new ContextMenu();
                    MenuItem VerwerfItem = new MenuItem();
                    VerwerfItem.Header = "Verwerfen";
                    VerwerfItem.Click += VerwerfItem_Click;
                    VerwerfItem.Tag = PDFTab;
                    PDFTabContextMenue.Items.Add(VerwerfItem);
                    PDFTab.ContextMenu = PDFTabContextMenue;
                    tabAnsicht.Items.Add(PDFTab);
                    GesamtKalkulationsTab = PDFTab;
                    PDFTab.Focus();
                }
                else
                {
                    tabAnsicht.Items.Remove(GesamtKalkulationsTab);
                    gesamtTab = 0;
                    cmdGesamtbetragAuf_Click(sender, e);
                }
            }
        }
//>>>>>>>>>>>>>>>>>>>Kalkulationstab muss noch entfernt werden. Ist noch drin wegen FliesenTab 
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

            ContextMenu TabContextMenue = new ContextMenu();
            MenuItem SpeicherItem = new MenuItem();
            SpeicherItem.Header = "Speichern";
            SpeicherItem.Click += SpeicherItem_Click;
            SpeicherItem.Tag = tab;


            MenuItem VerwerfItem = new MenuItem();
            VerwerfItem.Header = "Verwerfen";
            VerwerfItem.Click += VerwerfItem_Click;
            VerwerfItem.Tag = tab;
            TabContextMenue.Items.Add(SpeicherItem);
            TabContextMenue.Items.Add(VerwerfItem);
            tab.ContextMenu = TabContextMenue;
            tabAnsicht.Items.Add(tab);
            tabAnsicht.SelectedItem = tab;
            tabList.Add(tab);
            return tab;
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private void VerwerfItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ConItem = (MenuItem)sender;
            TabItem TabItem = (TabItem)ConItem.Tag;
            tabList.Remove(TabItem);
            tabAnsicht.Items.Remove(TabItem);
        }

        private void SpeicherItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ConItem = (MenuItem)sender;
            Console.WriteLine(ConItem.Tag.GetType());
            if (ConItem.Tag.GetType().Equals(typeof(tfMarktMain.TapetenTab)))
            {
                TapetenTab tabItem = (TapetenTab)ConItem.Tag;
                tabItem.setKalkulation(tabItem.getTapetenGUI().getKalkulation());
                Tapetenkalkulation.Tapetenkalkulation tapetenKalkulation = tabItem.getKalkulation();
                tapetenKalkulation.Identifier = generateGuid();
                tabItem.setKalkulation(tapetenKalkulation);
                SelectedCustomer.addCalculation(tabItem.getKalkulation(), /*OVERRIDE SETZEN!*/ true); //Wirft Exception wenn die Kalkulation nicht vollständig initialisiert wurde
                CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;
               // tabAnsicht.Items.Remove(tabItem);
            }
            if (ConItem.Tag.GetType().Equals("tfMarktMain.FliesenTab"))
            {
                //FliesenTab tabItem = (FliesenTab)ConItem.Tag;
                //SelectedCustomer.addCalculation(tabItem.getKalkulation(), /*OVERRIDE SETZEN!*/ true); //Wirft Exception wenn die Kalkulation nicht vollständig initialisiert wurde
                //CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;
                //tabAnsicht.Items.Remove(tabItem);
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
            foreach (TabItem tab in tabList)
            {
                tabAnsicht.Items.Remove(tab);
            }
        }

        private void cmdStarteAdministration_Click(object sender, RoutedEventArgs e)
        {
            new AdministrationDerProdukteGUI().Show();
        }
        private void saveGesamtkalkulation(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer.Calculations.Count > 0)
            {
                if (GesamtkalkualtionsPDF == null)
                {
                    GesamtkalkualtionsPDF = new PDFFactory.CustomerPDFDocument(SelectedCustomer);
                }
                GesamtkalkualtionsPDF.savePDF(true);
            }
        }

        private void printGesamtkalkulation(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer.Calculations.Count > 0)
            {
                if (GesamtkalkualtionsPDF == null)
                {
                    GesamtkalkualtionsPDF = new PDFFactory.CustomerPDFDocument(SelectedCustomer);
                }
                GesamtkalkualtionsPDF.printPDF();
            }
        }


    }
}
