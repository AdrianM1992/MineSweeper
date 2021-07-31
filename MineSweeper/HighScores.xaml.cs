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
using System.Runtime.Serialization.Formatters.Binary;

namespace MineSweeper
{
    /// <summary>
    /// Logika interakcji dla klasy HighScores.xaml
    /// </summary>
    public partial class HighScores : Window
    {
        HighScoresTable highScoresTable = new HighScoresTable();
        public HighScores(HighScoresTable highScoresTable)
        {
            InitializeComponent();
            this.highScoresTable = highScoresTable;
            EasyTextBlock.Text = highScoresTable.GetHighScores(0);
            MediumTextBlock.Text = highScoresTable.GetHighScores(1);
            HighTextBlock.Text = highScoresTable.GetHighScores(2);
        }
    }
}
