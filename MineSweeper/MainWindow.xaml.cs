using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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



namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int row;
        int column;
        string highScorePath = @"High scores\HighScores.dat";
        HighScoresTable highScoresTable;
        Field_button[,] buttons;
        MineField mineFiled;
        ImageBrush BombBrush = new ImageBrush();
        ImageBrush FlagBrush = new ImageBrush();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DateTime startDateTime = new DateTime();
        DateTime endDateTime= new DateTime();

        ScoreComparer scoreComparer = new ScoreComparer();
        public MainWindow()
        {
            InitializeComponent();
            BombBrush.ImageSource = new BitmapImage(new Uri(@"Images\\bomb.png", UriKind.Relative));
            BombBrush.Stretch = Stretch.None;
            FlagBrush.ImageSource = new BitmapImage(new Uri(@"Images\\flag.png", UriKind.Relative));
            FlagBrush.Stretch = Stretch.None;
            highScoresTable = (HighScoresTable)Serielalization.Initaialization(highScoresTable, highScorePath);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }
        public void GameGridBuilder(int numberOfRows, int numberOfColumns)
        {
            GameField.RowDefinitions.Clear();
            GameField.ColumnDefinitions.Clear();
            GameField.Children.Clear();
            EndGameLabel.Content = "";

            GameField.Height = (numberOfRows) * 30;
            GameField.Width = (numberOfColumns) * 30;

            for (int i = 0; i < numberOfRows; i++)
            {
                GameField.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < numberOfColumns; i++)
            {
                GameField.ColumnDefinitions.Add(new ColumnDefinition());
            }
            this.Height = GameField.Height + 200;
            this.Width = GameField.Width + 100;

            buttons = new Field_button[numberOfRows, numberOfColumns];

            while (true)
            {
                int i = 0;
                foreach (RowDefinition row in GameField.RowDefinitions)
                {
                    int j = 0;
                    foreach (ColumnDefinition column in GameField.ColumnDefinitions)
                    {
                        buttons[i, j] = new Field_button(i, j, false);
                        buttons[i, j].Background = Brushes.LightGray;
                        buttons[i, j].FontWeight = FontWeights.Bold;
                        buttons[i, j].MouseDown += new MouseButtonEventHandler(Field_Button_MouseDown);
                        buttons[i, j].Click += new RoutedEventHandler(Field_Button_Click);
                        GameField.Children.Add(buttons[i, j]);
                        Grid.SetRow(buttons[i, j], i);
                        Grid.SetColumn(buttons[i, j], j);
                        j++;
                    }
                    i++;
                }
                break;
            }
        }
        private void WinLost(WinCase winCase)
        {
            dispatcherTimer.Stop();

            switch (winCase)
            {
                case WinCase.lost:
                    foreach (Field_button button in buttons)
                    {
                        button.MouseDown -= new MouseButtonEventHandler(Field_Button_MouseDown);
                        button.Click -= new RoutedEventHandler(Field_Button_Click);
                    }
                    for (int i = 0; i < buttons.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < buttons.GetUpperBound(1) + 1; j++)
                        {
                            if (mineFiled.IsBomb(buttons[i, j].XGridPosition, buttons[i, j].YGridPosition))
                            {
                                buttons[i, j].Background = BombBrush;
                            }
                        }
                    }
                    EndGameLabel.Content = "Game over - Failure!";
                    break;
                case WinCase.win:
                    foreach (Field_button button in buttons)
                    {
                        button.MouseDown -= new MouseButtonEventHandler(Field_Button_MouseDown);
                        button.Click -= new RoutedEventHandler(Field_Button_Click);
                    }
                    for (int i = 0; i < buttons.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < buttons.GetUpperBound(1) + 1; j++)
                        {
                            if (mineFiled.IsBomb(buttons[i, j].XGridPosition, buttons[i, j].YGridPosition))
                            {
                                buttons[i, j].Background = BombBrush;
                            }
                        }
                    }
                    EndGameLabel.Content = "Game over - Victory!";

                    TimeSpan timeSpan = new TimeSpan();
                    timeSpan = endDateTime - startDateTime;
                    DialogInupt nameInupt = new DialogInupt();
                    nameInupt.ShowDialog();
                    highScoresTable.SetHighScore(nameInupt.Name, DifficultyBox.SelectedIndex, timeSpan);
                    highScoresTable = (HighScoresTable)Serielalization.SaveToFile(highScoresTable, highScorePath);
                    break;
                default:
                    break;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (DifficultyBox.SelectedIndex)
            {
                case 0:
                    row = 8;
                    column = 8;
                    break;
                case 1:
                    row = 16;
                    column = 16;
                    break;
                case 2:
                    row = 16;
                    column = 30;
                    break;
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GameGridBuilder(row, column);
            mineFiled = new MineField(row, column);
            RemainingBombsLabel.Content = mineFiled.BombsCount;
            startDateTime = DateTime.Now;
            dispatcherTimer.Start();
        }
        private void HighScoresButtton_Click(object sender, RoutedEventArgs e)
        {
            HighScores highScores = new HighScores(highScoresTable);
            highScores.ShowDialog();
        }
        private void Field_Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Field_button b = (Field_button)sender;
            int remainingBombs = mineFiled.BombsCount;

            if (e.ChangedButton == MouseButton.Right)
            {
                b.FlagBomb(FlagBrush);
            }
            foreach (Field_button button in buttons)
            {
                if (button.Flag)
                {
                    remainingBombs--;
                }
            }
            RemainingBombsLabel.Content = remainingBombs;
        }
        private void Field_Button_Click(object sender, RoutedEventArgs e)
        {
            Field_button b = (Field_button)sender;

            if (mineFiled.IsBomb(b.XGridPosition,b.YGridPosition))
            {
                WinLost(WinCase.lost);
            }
            else
            {
                int remainingButtons = 0;
                int howManyBombs= mineFiled.HowManySurroundingBombs(b.XGridPosition, b.YGridPosition);

                b.Content = howManyBombs;
                b.Foreground = mineFiled.NumberColor(howManyBombs);
                b.Background = null;
                b.IsEnabled = false;
                foreach (Field_button button in buttons)
                {
                    if (button.Content == null)
                    {
                        remainingButtons++;
                    }
                }
                if (remainingButtons == mineFiled.BombsCount)
                {
                    WinLost(WinCase.win);
                }
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            endDateTime = DateTime.Now;
            TimeLabel.Content = endDateTime -startDateTime;
        }
    }
}
