using System;

namespace Ast
{
    enum  Strategy
    {
        seek,
        branch
    }
    public class BreederVisitor : MutationVisitor
    {
        Random randsource = new Random();
        Strategy strategy;

        public Node Breed(Node parent1, Node parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;



            
            Node activeHead = this.VisitNode(parent2);
            return parent1;
        }

        Node Parent1;
        Node Parent2;

        public BreederVisitor()
        {
            strategy = Strategy.seek;
        }

        
        //Here be the visitors...
        public override Node Visit(ConstantNode n)
        {
    
            return n;
        }

        public override Node Visit(IdentifierNode n)
        {
            
            return n;
        }

        public override Node Visit(BinaryNode n)
        {
            if(randsource.NextDouble()<0.5)
            {
                strategy = Strategy.branch;
                //ActiveHead = n;

            }
            return base.Visit(n);
        }

        public override Node Visit(UnaryNode n)
        {
           
            return base.Visit(n);
        }

        public override Node Visit(IfNode n)
        {
            return base.Visit(n);
        }
    }
}