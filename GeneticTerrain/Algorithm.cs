using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    class Algorithm
    {
        Node _rootNode;
        double _delta;

        public Algorithm(Node rootNode, double delta)
        {
            _rootNode = rootNode;
            _delta = delta;
        }
    }
}
