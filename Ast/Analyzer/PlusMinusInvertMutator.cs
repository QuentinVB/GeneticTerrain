using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ast.Analyzer
{
    public class PlusMinusInvertMutator : MutationVisitor
    {
        public override Node Visit(BinaryNode n)
        {
            if (n.Type == TokenType.Minus)
            {
                return new BinaryNode(TokenType.Plus, VisitNode(n.Left), VisitNode(n.Right));
            }
            if (n.Type == TokenType.Plus)
            {
                return new BinaryNode(TokenType.Minus, VisitNode(n.Left), VisitNode(n.Right));
            }
            return base.Visit(n);
        }

        public override Node Visit(UnaryNode n)
        {
            if (n.Type == TokenType.Minus)
            {
                return n.Operand;
            }
            if (n.Type == TokenType.Plus)
            {
                return new UnaryNode(TokenType.Minus, VisitNode(n.Operand));
            }
            return base.Visit(n);
        }
    } 
}
