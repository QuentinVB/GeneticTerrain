using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public class Node
    {

    }

    public class Algorithm : IComparable<object>
    {
        Node _rootNode;
        double _delta;

        int nodeCount;

        public Algorithm(Node rootNode, double delta)
        {
            _rootNode = rootNode;
            _delta = delta;
        }

        public int NodeCount { get => nodeCount; internal set => nodeCount = value; }
        public double Delta { get => _delta; set => _delta = value; }

        public int CompareTo(object other)
        {
            if (other == null) return 1;

            if (other is Algorithm otherAlgorithm)
                return this._delta.CompareTo(otherAlgorithm._delta);
            else
                throw new ArgumentException("Object is not an Algorithm");
        }
    }
}
