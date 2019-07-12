using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public class GenerateStringGraph
    {
        private static Random random = new Random();
        private static string RandomStringXY(int length)
        {
            const string chars = "xy";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static string RandomStringOpperator(int length)
        {
            const string chars = "-+";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static string RandomStringNumber(int length)
        {
            const string chars = "01";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string CreateRandomPopulation(int maxPop)
        {
            string sb = string.Empty;
            for (int i = 0; i <= maxPop; i++)
            {
                if(i == maxPop)
                {
                    sb += RandomStringXY(1) + "" + RandomStringOpperator(1) + "" + RandomStringNumber(1);
                }
                else if (i % 2 == 0)
                {
                    if (i <= 12)
                    {
                        sb += RandomStringXY(1) + ",";
                    }
                    else if (i <= 24)
                    {
                        sb += RandomStringOpperator(1) + "" + RandomStringXY(1) + ",";
                    }
                    else
                    {
                        sb += RandomStringXY(1) + "" + RandomStringOpperator(1) + "" + RandomStringNumber(1) + ",";
                    }

                }
                else
                {
                    sb += RandomStringXY(1) + "" + RandomStringOpperator(1) + "" + RandomStringNumber(1) + ",";
                }
            }

            return sb;
        }
    }
}
