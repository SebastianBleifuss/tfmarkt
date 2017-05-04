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

namespace tfMarktMain.Tapetenkalkulation
{
    /// <summary>
    /// Interaktionslogik für TapetenkalkulationGUI.xaml
    /// </summary>
    public partial class TapetenkalkulationGUI : Window
    {
        public TapetenkalkulationGUI()
        {
            InitializeComponent();
        }

        private void btnFlaecheBerechnen_Click(object sender, RoutedEventArgs e)
        {
            //FlächeBerechnenfenster muss noch eingefügt werden.

            //Window FlaecheBerechnenFenster = new FlaecheBerechnen();
            //FlaecheBerechnenFenster.ShowDialog();
            //txtGroesse.Text = kalkulation.getFlaeche().ToString();
        }

        private void btnRollenBerechnen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
