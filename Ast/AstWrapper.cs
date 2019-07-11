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
        public Node Parse(string expression)
        {

            return simpleAnalyzer.Parse(expression);
        }

        public double Compute(Node n, double x, double y)
        {
            computeVisitor = new ComputeVisitor(id => id == "x" ? x : id == "y" ? y : throw new ArgumentException("The identifier doesn't exist"));
            computeVisitor.VisitNode(n);
            return computeVisitor.Result;
        }

    }
}
