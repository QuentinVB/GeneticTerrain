using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ast
{
    public class RandomMutator : MutationVisitor
    {
        Random randsource = new Random();

        /// <summary>
        /// A Mutation visitor who randomly mutate the node he visits
        /// </summary>
        /// <param name="mutationRatio">The threshold to mutate</param>
        public RandomMutator(double mutationRatio)
        {
            this.MutationRatio = mutationRatio;
        }

        public double MutationRatio { get; private set; }
        /// <summary>
        /// Try to mutate the node into another one if rand is below mutation ratio
        /// </summary>
        /// <param name="originalNode">The original node</param>
        /// <returns>The mutated node (or the original one)</returns>
        private Node Mutate(Node originalNode)
        {
            //Improvement: Mutate by average; Reduce mutation ratio through generations ?
            if (randsource.NextDouble()<MutationRatio)
            {
                switch (randsource.Next(0, 5))
                {
                    case 0: //Mutate as BinaryNode
                        return new BinaryNode(GetRandomOperationToken(), GetRandomLeaf(), GetRandomLeaf());
                    case 1: //Mutate as ConstantNode
                        return GetRandomLeaf();
                    case 2: //Mutate as IdentifierNode
                        return GetRandomLeaf();
                    case 3: //Mutate as IfNode
                        return new IfNode(GetRandomLeaf(), GetRandomLeaf(), GetRandomLeaf());
                    case 4: //Mutate as UnaryNode
                        return new UnaryNode(TokenType.Minus,GetRandomLeaf());
                    default:
                        return originalNode;
                }
            }
            return originalNode;
        }
        /// <summary>
        /// Provide a binary operation token randomly
        /// </summary>
        private TokenType GetRandomOperationToken()
        {
            switch (randsource.Next(0, 3))
            {
                case 0: return TokenType.Plus;
                case 1: return TokenType.Minus;
                case 2: return TokenType.Mult;
                case 3: return TokenType.Div;
                default: throw new NotSupportedException();
            }
        }
        /// <summary>
        /// Provide a leaf (constant or identifier) randomly
        /// </summary>
        private Node GetRandomLeaf()
        {
            //generate Constant
            if(randsource.NextDouble()<0.5)
            {
                double value = randsource.NextDouble();
                return new ConstantNode(value ==0 ?1:value);
            }
            //generate Identifier
            else
            {
                return new IdentifierNode(randsource.NextDouble() < 0.5 ? "x" : "y");
            }
        }
        //Here be the visitors...

        public override Node Visit(ConstantNode n)
        {
            return Mutate(n);
        }

        public override Node Visit(IdentifierNode n)
        {
            return Mutate(n);
        }

        public override Node Visit(ErrorNode n)
        {
            return Mutate(n);
        }

        public override Node Visit(BinaryNode n)
        {
           
            return Mutate(n);
        }

        public override Node Visit(UnaryNode n)
        {
            return Mutate(n);
        }

        public override Node Visit(IfNode n)
        {
            return Mutate(n);
        }
    } 
}
