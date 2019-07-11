using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public struct Couple
    {
        public Couple(Algorithm parent1, Algorithm parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
        }
        public Algorithm Parent1 { get; set; }
        public Algorithm Parent2 { get; set; }
    }
}
