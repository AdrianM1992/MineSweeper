using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace MineSweeper
{
    [Serializable]
    public class SingleScore
    {
        string name;
        int difficulty;
        TimeSpan time;
        public string Name { get { return name; } }
        public int Difficulty { get {return difficulty; } }
        public TimeSpan Time { get { return time; } }

        public  SingleScore(string name, int difficulty, TimeSpan time)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.time = time;
        }
    }
}
