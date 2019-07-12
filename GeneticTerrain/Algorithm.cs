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

        AstWrapper _wrapper;

        public Algorithm(Node rootNode)
        {
            _wrapper = new AstWrapper();

            _rootNode = rootNode;
            _delta = 0;

            _nodeCount= _wrapper.NodeCount(RootNode);
        }

        public int NodeCount { get => _nodeCount; }
        public double Delta { get => _delta; set => _delta = value; }
        public Node RootNode { get => _rootNode; set => _rootNode = value; }

        public int CompareTo(object other)
        {
            if (other == null) return 1;

            if (other is Algorithm otherAlgorithm)// the best algorithm is the lower one
                if (this._delta - otherAlgorithm._delta < 0) return 1;
                else if (this._delta - otherAlgorithm._delta > 0) return -1;
                else return 0;
            else
                throw new ArgumentException("Object is not an Algorithm");
            }
        public override string ToString()
        {
            return $"Algorithm,Delta:{_delta},NodeCount:{_nodeCount},Tree:{_wrapper.Print(_rootNode)}";
        }
    }
}
