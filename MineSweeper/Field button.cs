using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MineSweeper
{
    class Field_button: Button
    {
        private bool flag;
        public int XGridPosition { get; }
        public int YGridPosition { get; }
        public bool Flag { get {return flag; } }
        public Field_button(int x, int y, bool flag)
        {
            XGridPosition = x;
            YGridPosition = y;
            flag = this.flag;
        }
        public void FlagBomb(ImageBrush brush)
        {
            if (flag)
            {
                flag = false;
                this.Background = Brushes.LightGray;
            }
            else
            {
                flag = true;
                this.Background = brush;
            }
        }
    }
}
