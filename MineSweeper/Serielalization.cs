using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MineSweeper
{
    public static class Serielalization
    {
        public static Object Initaialization(Object highScoresTable, string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                using (Stream output = File.Create(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    highScoresTable = new HighScoresTable();
                    formatter.Serialize(output, highScoresTable);
                }
            }
            else
            {
                using (Stream input = File.OpenRead(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    highScoresTable = (HighScoresTable)formatter.Deserialize(input);
                }
            }
            return highScoresTable;
        }
        public static Object SaveToFile(Object highScoresTable, string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                using (Stream output = File.Create(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    highScoresTable = new HighScoresTable();
                    formatter.Serialize(output, highScoresTable);
                }
            }

            else
            {
                using (Stream output = File.OpenWrite(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(output, highScoresTable);
                } 
            }
            return highScoresTable;
        }
    }
}
