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
        /// Try to mutate the node into another one if rand is below mutation ratio. 
        /// If not return the originalNode
        /// </summary>
        /// <param name="originalNode">The original node</param>
        /// <returns>The mutated node (or the original one)</returns>
        private bool TryMutate(Node originalNode, out Node mutatedNode)
        {
            //Improvement: Mutate by average; Reduce mutation ratio through generations ?
            if (randsource.NextDouble()<MutationRatio)
            {
                mutatedNode = GetRandomNode(MutationRatio);
                return true;
            }
            mutatedNode = originalNode;
            return false;
        }
        private Node GetRandomNode(double MutationRatio)
        {
            double localeMutation = MutationRatio * 0.5<0.1?0:MutationRatio * 0.5;
            switch (randsource.Next(0, 5))
            {

                case 0: //Mutate as BinaryNode
                    return new BinaryNode(
                        GetRandomOperationToken(),
                        MutateLocaly(localeMutation),
                        MutateLocaly(localeMutation));

                case 1: //Mutate as ConstantNode
                    return MutateLocaly(localeMutation);

                case 2: //Mutate as IdentifierNode
                    return MutateLocaly(localeMutation);

                case 3: //Mutate as IfNode
                    return new IfNode(
                        MutateLocaly(localeMutation),
                        MutateLocaly(localeMutation),
                       MutateLocaly(localeMutation));

                case 4: //Mutate as UnaryNode
                    return new UnaryNode(TokenType.Minus, MutateLocaly(localeMutation));
                default:
                    return GetRandomLeaf();
            }
            Node MutateLocaly(double localeMutationRatio)
            {
                return randsource.NextDouble() < localeMutationRatio ? GetRandomNode(localeMutationRatio) : GetRandomLeaf();
            }
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
            if (TryMutate(n, out Node newNode))
            {
                return newNode;
            }
            return n;
        }

        public override Node Visit(IdentifierNode n)
        {
            if (TryMutate(n, out Node newNode))
            {
                return newNode;
            }
            return n;
        }

        public override Node Visit(BinaryNode n)
        {
            if (TryMutate(n, out Node newNode))
            {
                return newNode;
            }
            return base.Visit(n);
        }

        public override Node Visit(UnaryNode n)
        {
            if (TryMutate(n, out Node newNode))
            {
                return newNode;
            }
            return base.Visit(n);
        }

        public override Node Visit(IfNode n)
        {
            if (TryMutate(n, out Node newNode))
            {
                return newNode;
            }
            return base.Visit(n);
        }
    } 
}
