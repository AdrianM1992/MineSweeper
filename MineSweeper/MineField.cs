using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MineSweeper
{
    class MineField
    {
        private bool[,] mineFiled;
        private int bombsCount;
        private int[,] surroundingBombs;
        private Random random;
        SolidColorBrush brush = new SolidColorBrush();
        public int BombsCount { get { return bombsCount; } }
        public int ButtonsCount { get; private set; }
        
        public MineField(int numberOfRows, int numberOfColumns)
        {
            random = new Random();
            ButtonsCount = numberOfRows * numberOfColumns;
            bombsCount = (int)((numberOfRows) * (numberOfColumns) * 0.15);
            mineFiled = PlantBombs(numberOfRows, numberOfColumns);
            surroundingBombs = CalculateSurroundingBombs(mineFiled);
        }
        private bool [,] PlantBombs(int numberOfRows, int numberOfColumns)
        {
            int[] bombsVector = new int[bombsCount];
            bool[,] bombsLocations = new bool[numberOfRows, numberOfColumns];
            int tempBomb;
            int counter = 0;
            bool uniqueLocation = true;

            while (counter < bombsCount)
            {
                tempBomb = random.Next(1, numberOfRows * numberOfColumns);

                for (int i = 0; i < counter; i++)
                {
                    if (bombsVector[i] == tempBomb)
                    {
                        uniqueLocation = false;
                        break;
                    }
                    else
                    {
                        uniqueLocation = true;
                    }
                }
                if (uniqueLocation)
                {
                    bombsVector[counter] = tempBomb;
                    counter++;
                }
            }
            for (int i = 0; i < bombsVector.Length; i++)
            {
                int x;
                int y;
                x = bombsVector[i] / numberOfColumns;
                y = bombsVector[i] - x * numberOfColumns-1;
                if (y < 0)
                {
                    y = numberOfColumns - 1;
                    x -= 1;
                }
                bombsLocations[x, y] = true;
            }
            return bombsLocations;
        }
        private int [,] CalculateSurroundingBombs ( bool [,] mineFiled)
        {
            int[,] bombCount = new int[mineFiled.GetUpperBound(0) + 1, mineFiled.GetUpperBound(1) + 1];

            for (int i = 0; i < bombCount.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < bombCount.GetUpperBound(1) + 1; j++)
                {
                    int surroundingBombs = 0;

                    if (i == 0 && j == 0)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            for (int l = 0; l < 2; l++)
                            {
                                if (mineFiled[k,l])
                                {
                                    surroundingBombs++;
                                }
                            }
                        }
                        bombCount[i, j] = surroundingBombs;
                    }
                    else if (i == 0 && j == bombCount.GetUpperBound(1))
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            for (int l = bombCount.GetUpperBound(1); l > bombCount.GetUpperBound(1)-2; l--)
                            {
                                if (mineFiled[k, l])
                                {
                                    surroundingBombs++;
                                }
                            }
                        }
                        bombCount[i, j] = surroundingBombs;
                    }
                    else if (i == bombCount.GetUpperBound(0) && j == 0)
                    {
                        for (int k = bombCount.GetUpperBound(0); k > bombCount.GetUpperBound(1) - 2; k--)
                        {
                            for (int l = 0; l < 2; l++)
                            {
                                if (mineFiled[k, l])
                                {
                                    surroundingBombs++;
                                }
                            }
                        }
                        bombCount[i, j] = surroundingBombs;
                    }
                    else if (i == bombCount.GetUpperBound(0) && j == bombCount.GetUpperBound(1))
                    {
                        for (int k = bombCount.GetUpperBound(0); k > bombCount.GetUpperBound(0) - 2; k--)
                        {
                            for (int l = bombCount.GetUpperBound(1); l > bombCount.GetUpperBound(1) - 2; l--)
                            {
                                if (mineFiled[k, l])
                                {
                                    surroundingBombs++;
                                }
                            }
                        }
                        bombCount[i, j] = surroundingBombs;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                for (int l = j - 1; l < j + 2; l++) 
                                {
                                    if (mineFiled[k, l])
                                    {
                                        surroundingBombs++;
                                    }
                                }
                            }
                            bombCount[i, j] = surroundingBombs;
                        }
                        else if (i == bombCount.GetUpperBound(0))
                        {
                            for (int k = bombCount.GetUpperBound(0); k > bombCount.GetUpperBound(0) - 2; k--)
                            {
                                for (int l = j - 1; l < j + 2; l++)
                                {
                                    if (mineFiled[k, l])
                                    {
                                        surroundingBombs++;
                                    }
                                }
                            }
                            bombCount[i, j] = surroundingBombs;
                        }
                        else if (j == 0)
                        {
                            for (int k = i - 1; k < i + 2; k++)
                            {
                                for (int l = 0; l < 2; l++)
                                {
                                    if (mineFiled[k, l])
                                    {
                                        surroundingBombs++;
                                    }
                                }
                            }
                            bombCount[i, j] = surroundingBombs;
                        }
                        else if (j== bombCount.GetUpperBound(1))
                        {
                            for (int k = i - 1; k < i + 2; k++)
                            {
                                for (int l = bombCount.GetUpperBound(1); l > bombCount.GetUpperBound(1) - 2; l--)
                                {
                                    if (mineFiled[k, l])
                                    {
                                        surroundingBombs++;
                                    }
                                }
                            }
                            bombCount[i, j] = surroundingBombs;
                        }
                        else
                        {
                            for (int k = i - 1; k < i + 2; k++)
                            {
                                for (int l = j - 1; l < j + 2; l++)
                                {
                                    if (mineFiled[k, l])
                                    {
                                        surroundingBombs++;
                                    }
                                }
                            }
                            bombCount[i, j] = surroundingBombs; 
                        }
                    }

                }
            }
            return bombCount;
        }
        
        public bool IsBomb (int xGridPosition, int yGridPosiotion)
        {
            if (mineFiled[xGridPosition, yGridPosiotion])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int HowManySurroundingBombs(int xGridPosition, int yGridPosition)
        {
            return surroundingBombs[xGridPosition, yGridPosition];
        }
        public SolidColorBrush NumberColor (int howManyBombs)
        {
            switch (howManyBombs)
            { 
                case 0:
                    return (SolidColorBrush)Brushes.Transparent;
                case 1:
                    return (SolidColorBrush)Brushes.Violet;
                case 2:
                    return (SolidColorBrush)Brushes.Indigo;
                case 3:
                    return (SolidColorBrush)Brushes.Blue;
                case 4:
                    return (SolidColorBrush)Brushes.Green;
                case 5:
                    return (SolidColorBrush)Brushes.Yellow;
                case 6:
                    return (SolidColorBrush)Brushes.Orange;
                case 7:
                    return (SolidColorBrush)Brushes.Red;
                case 8:
                    return (SolidColorBrush)Brushes.Black;
                default:
                    return (SolidColorBrush)Brushes.Transparent;
            }
        }
    }

}
