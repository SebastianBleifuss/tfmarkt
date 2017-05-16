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
        private bool isCustomerChanged = false;
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
            //if (isCustomerChanged)
            //{
            //    if (MessageBox.Show("Möchten Sie die Änderungen des Kunden speichern?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //    {
            //        Console.WriteLine("Nein");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Ja. Speichern");
            //    }
            //}
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
            bool istTabBekannt = false;
            if (CalculationListBox.SelectedIndex != -1)
            {
                Calculation calc = SelectedCustomer.Calculations.Values.ToArray()[CalculationListBox.SelectedIndex];
                //prüfen, ob es den tab schon gibt
                foreach (TabItem item in tabAnsicht.Items) 
                {
                    if (item.Header.ToString().Equals(calc.Description)) 
                    {
                        istTabBekannt = true;
                        break;
                    }
                }
                if (!istTabBekannt)
                {
                    if (calc.CalculationType.Equals(typeof(Tapetenkalkulation.Tapetenkalkulation)))
                    {
                        Tapetenkalkulation.Tapetenkalkulation tapCalc = (Tapetenkalkulation.Tapetenkalkulation)calc;
                        TapetenTab tab = neueTapetenKalkulationTab(tapCalc.Description, tapCalc);
                    }
                    if (calc.CalculationType.Equals(typeof(Fliesenkalkulation.Fliesenkalkulation)))
                    {
                        FliesenTab tab = neueFliesenKalkulationTab(calc);
                    }
                }
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
            //KalkulationsTab<Calculation> tab= neuerTab("Fliesen", "tabFliesenAnsicht", fliesenTabs);
            //Frame tabFrame = new Frame();
            //Fliesenkalkulation.FliesenkalkulationGUI ladeSeite = new Fliesenkalkulation.FliesenkalkulationGUI();
            //tabFrame.Content = ladeSeite.Content;
            //tab.Content = tabFrame;
            //tab.Focus();
            //fliesenTabs++;
            neueFliesenKalkulationTab(null);
        }

        private void cmdTapetenAuf_Click(object sender, RoutedEventArgs e)
        {
            neueTapetenKalkulationTab("Tapete", null);
        }

        private TapetenTab neueTapetenKalkulationTab(String tabname, Tapetenkalkulation.Tapetenkalkulation kalkulation) 
        {
            TapetenTab tab = new TapetenTab();
            if (tapetenTabs > 0 && kalkulation==null)
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

        private FliesenTab neueFliesenKalkulationTab(Calculation kalkulation)
        {
            String tabname = "Fliese";
            if (kalkulation != null)
            {
                tabname = kalkulation.Description;
            }
            FliesenTab tab = new FliesenTab();
            if (fliesenTabs > 0 && kalkulation == null)
            {
                tab.Name = tabname + fliesenTabs;
                tab.Header = tabname + fliesenTabs;
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
            tabFrame.Content = tab.getFliesenGUI().Content;
            if (kalkulation != null)
            {
                tab.getFliesenGUI().ladeVorhandeneKalkulation(kalkulation);
            }
            tab.Content = tabFrame;
            tab.Focus();
            fliesenTabs++;
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
            if (ConItem.Tag.GetType().Equals(typeof(tfMarktMain.TapetenTab)))
            {
                TapetenTab tabItem = (TapetenTab)ConItem.Tag;
                tabItem.setKalkulation(tabItem.getTapetenGUI().getKalkulation());
                Tapetenkalkulation.Tapetenkalkulation tapetenKalkulation = tabItem.getKalkulation();
                tapetenKalkulation.Identifier = generateGuid();
                tabItem.setKalkulation(tapetenKalkulation);
                SelectedCustomer.addCalculation(tabItem.getKalkulation(), /*OVERRIDE SETZEN!*/ true); //Wirft Exception wenn die Kalkulation nicht vollständig initialisiert wurde
                CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;
                isCustomerChanged = true;
               // tabAnsicht.Items.Remove(tabItem);
            }
            if (ConItem.Tag.GetType().Equals(typeof(tfMarktMain.FliesenTab)))
            {
                FliesenTab tabItem = (FliesenTab)ConItem.Tag;
                tabItem.setKalkulation(tabItem.getFliesenGUI().getFliesenKalkulation());
                Fliesenkalkulation.Fliesenkalkulation fliesenKalkulation = tabItem.getKalkulation();
                fliesenKalkulation.Identifier = generateGuid();
                tabItem.setKalkulation(fliesenKalkulation);
                SelectedCustomer.addCalculation(tabItem.getKalkulation(), /*OVERRIDE SETZEN!*/ true); //Wirft Exception wenn die Kalkulation nicht vollständig initialisiert wurde
                CalculationListBox.ItemsSource = SelectedCustomer.Calculations.Values;
                isCustomerChanged = true;
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
            isCustomerChanged = true;
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
            new AdministrationDerProdukteGUI();
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
