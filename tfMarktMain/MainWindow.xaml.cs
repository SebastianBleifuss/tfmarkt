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
            new CustomerInterface.CustomerInterface().Show();
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
    }
}
