using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MineSweeper
{
    [Serializable]
    public class HighScoresTable
    {
        Dictionary<int, List<SingleScore>> highScores;
        public HighScoresTable()
        {
            highScores = new Dictionary<int, List<SingleScore>>()
            {
                {0, new List<SingleScore>() },
                {1, new List<SingleScore>() },
                {2, new List<SingleScore>() },
            };
        }
        public string GetHighScores(int difficulty)
        {
            string textToReturn = "";
            int counter = 1;

            foreach (SingleScore score in highScores[difficulty])
                {
                textToReturn += counter + ". " + score.Name + "\t" + score.Time + "\n\r";
                counter++;
                }
            if (textToReturn == "")
            {
                textToReturn = "No high scores yet.";
            }
            return textToReturn;
        }
        public void SetHighScore (string name, int difficulty, TimeSpan time)
        {
            ScoreComparer scoreComparer = new ScoreComparer();
            SingleScore score = new SingleScore(name, difficulty, time);

            highScores[difficulty].Add(score);
            highScores[difficulty].Sort(scoreComparer);

            if (highScores[difficulty].Count > 10) 
            {
                highScores[difficulty].RemoveAt(10);
            }
        }
    }
}
