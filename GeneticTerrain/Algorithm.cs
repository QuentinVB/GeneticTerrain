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
        Node _rightNode;
        Node _leftNode;
        double _delta;
        int _nodeCount;

        public Algorithm(Node rootNode, double delta)
        {
            _rootNode = rootNode;
            _delta = delta;
        }

        public Algorithm()
            :this(null, 0)
        {

        }

        public int NodeCount { get => _nodeCount; set => _nodeCount = value; }
        public double Delta { get => _delta; set => _delta = value; }
        public Node RootNode { get => _rootNode; set => _rootNode = value; }
        public Node LeftNode { get => _leftNode; set => _leftNode = value }
        public Node RightNode { get => _rightNode ; set => _rightNode = value }


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
            AstWrapper wrapper = new AstWrapper();
            return $"Algorithm,Delta:{_delta},NodeCount:{_nodeCount},Tree:{wrapper.Print(_rootNode)}";
        }

        public Node NodeConstructor(Node rootNode, Node leftNode,Node rightNode)
        {
            //il faut overrider la class Binary pour remplacer le node correspondant au tokentype
            return new BinaryNode(rootNode,leftNode,rightNode);
        }
    }
}
