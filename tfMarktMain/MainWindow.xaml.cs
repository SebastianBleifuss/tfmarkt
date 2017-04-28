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
            TabItem fliesenAnsicht = new TabItem();
            fliesenTabs++;
            fliesenAnsicht.Name = "tabfliesenAnsicht" + fliesenTabs;
            fliesenAnsicht.Header = "Fliesen " + fliesenTabs;
            tabAnsicht.Items.Add(fliesenAnsicht);
        }

        private void cmdTapetenAuf_Click(object sender, RoutedEventArgs e)
        {
            TabItem tapetenAnsicht = new TabItem();
            fliesenTabs++;
            tapetenAnsicht.Name = "tabfliesenAnsicht" + tapetenTabs;
            tapetenAnsicht.Header = "Fliesen " + tapetenTabs;
            tabAnsicht.Items.Add(tapetenAnsicht);
        }

        private void cmdGesamtbetragAuf_Click(object sender, RoutedEventArgs e)
        {
            TabItem gesamt = new TabItem();
            gesamt.Name = "Gesamt";
            gesamt.Header = "Gesamt";
            tabAnsicht.Items.Add(gesamt);
        }

        private void Generate_TotalCalculation_Click(object sender, RoutedEventArgs e)
        {
            Export.TotalCalculation.GenerateTotalCalculation(SelectedCustomer);
        }

      
    }
}
