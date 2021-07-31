using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class ScoreComparer : IComparer<SingleScore>
    {
        public int Compare(SingleScore x, SingleScore y)
        {
            if (x.Time < y.Time)
            {
                return -1;
            }
            if (x.Time > y.Time)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
