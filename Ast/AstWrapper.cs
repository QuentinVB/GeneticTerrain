using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ast
{
    public class AstWrapper
    {

        private SimpleAnalyzer simpleAnalyzer;
        private ComputeVisitor computeVisitor;
        // class algorithm

        public AstWrapper()
        {
            simpleAnalyzer = new SimpleAnalyzer();
            computeVisitor = new ComputeVisitor();
        }
        /// <summary>
        /// Parse the given expression into a node graph
        /// </summary>
        /// <param name="expression">the mathematical expression</param>
        /// <returns>The root node</returns>
        public Node Parse(string expression)
        {
            return simpleAnalyzer.Parse(expression);
        }
        /// <summary>
        /// Compute the given graph using the x and y identifier
        /// </summary>
        /// <param name="n">The root node of the graph</param>
        /// <param name="x">The x</param>
        /// <param name="y">The y</param>
        /// <returns>The computation result</returns>
        public double Compute(Node n, double x, double y)
        {
            computeVisitor = new ComputeVisitor(id => id == "x" ? x : id == "y" ? y : throw new ArgumentException("The identifier doesn't exist"));
            computeVisitor.VisitNode(n);
            return computeVisitor.Result;
        }
        /// <summary>
        /// Try to mutate the given graph with the given mutation ratio
        /// </summary>
        /// <param name="n">The root node</param>
        /// <param name="mutationRatio">The threshold to mutate</param>
        /// <returns>The root node of the mutated graph</returns>
        public Node MutateGraph(Node n, double mutationRatio)
        {
            RandomMutator randomMutator = new RandomMutator(mutationRatio);

            return randomMutator.VisitNode(n);
        }

    }
}
