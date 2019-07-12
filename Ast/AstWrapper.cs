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
        /// Print a NodeTree
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        public string Print(Node rootNode)
        {
            PrintVisitor printvisitor = new PrintVisitor();
            printvisitor.VisitNode(rootNode);
            return printvisitor.Result;
        }

        /// <summary>
        /// NodeTree count
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        public int NodeCount(Node rootNode)
        {
            PrintVisitor printvisitor = new PrintVisitor();
            printvisitor.VisitNode(rootNode);
            return printvisitor.NodeCount;
        }

        /// <summary>
        /// Try to mutate the given graph with the given mutation ratio
        /// </summary>
        /// <param name="n">The root node</param>
        /// <param name="mutationRatio">The threshold to mutate</param>
        /// <returns>The root node of the mutated graph</returns>
        public Node MutateGraph(Node n, double mutationRatio, out int mutationCount)
        {
            RandomMutator randomMutator = new RandomMutator(mutationRatio);
            Node newGraph = randomMutator.VisitNode(n);
            mutationCount = randomMutator.MutationCount;

            return newGraph;
        }

        /// <summary>
        /// Optimize the given graph
        /// </summary>
        /// <param name="n">The root node</param>
        /// <returns>The root node of the optimized graph</returns>
        public Node OptimizeGraph(Node n)
        {

            //node count via Ref ?
            OptimizationVisitor optimizationVisitor = new OptimizationVisitor();

            return optimizationVisitor.VisitNode(n);
        }

    }
}
