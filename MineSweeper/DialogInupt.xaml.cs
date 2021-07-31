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

namespace MineSweeper
{
    /// <summary>
    /// Logika interakcji dla klasy DialogInupt.xaml
    /// </summary>
    public partial class DialogInupt : Window
    {
        public string Name = "";
        public DialogInupt()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Name = NameBox.Text;
            this.Close();
        }
    }
}
