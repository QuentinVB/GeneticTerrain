using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    class Logger
    {
        List<string> _logContainer;
        public Logger()
        {
            _logContainer = new List<String>();
        }


        public string Log(string line)
        {
            string formatedLine = $"{DateTimeOffset.UtcNow.ToLocalTime().TimeOfDay} : {line}";
            _logContainer.Add(formatedLine);
            return formatedLine;
        }

        public void Print()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\logs\\"));
            File.WriteAllLines($".\\logs\\log-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.txt", _logContainer);
        }
    }
}
