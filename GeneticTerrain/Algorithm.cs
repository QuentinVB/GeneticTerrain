using Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{ 
    public class Algorithm : IComparable<object>
    {
        Node _rootNode;
        double _delta;

        int _nodeCount;

        public Algorithm(Node rootNode, double delta)
        {
            _rootNode = rootNode;
            _delta = delta;
        }

        public int NodeCount { get => _nodeCount; set => _nodeCount = value; }
        public double Delta { get => _delta; set => _delta = value; }
        public Node RootNode { get => _rootNode; set => _rootNode = value; }

        public int CompareTo(object other)
        {
            if (other == null) return 1;

            if (other is Algorithm otherAlgorithm)
                return -this._delta.CompareTo(otherAlgorithm._delta); // the best algorithm is the lower one
            else
                throw new ArgumentException("Object is not an Algorithm");
            }
        public override string ToString()
        {
            return $"Algorithm,Delta:{_delta},NodeCount:{_nodeCount},Tree:PRINTVISITOR";
        }
    }
}
