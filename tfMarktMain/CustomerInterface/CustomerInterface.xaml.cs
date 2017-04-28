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

namespace tfMarktMain.CustomerInterface
{
    /// <summary>
    /// Interaktionslogik für CustomerInterface.xaml
    /// </summary>
    public partial class CustomerInterface : Window
    {
        public CustomerInterface()
        {
            InitializeComponent();
            foreach (String Customername in Customer.getCustomerNames()) { 
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
            if (CustomersBox.SelectedIndex == 0) {
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
    }
}
