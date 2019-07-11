using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public class Logger
    {
        List<string> _logContainer;
        public Logger()
        {
            _logContainer = new List<String>();
        }


        public void Log(string line)
        {
            string formatedLine = $"{DateTimeOffset.UtcNow.ToLocalTime().TimeOfDay} : {line}";
            _logContainer.Add(formatedLine);
            Console.WriteLine(formatedLine);
        }

        public void Print()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\logs\\"));
            File.WriteAllLines($".\\logs\\log-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.txt", _logContainer);
        }
    }
}
