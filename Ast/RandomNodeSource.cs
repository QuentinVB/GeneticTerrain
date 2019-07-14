using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ast
{
    internal class RandomNodeSource
    {

        public static Node GetRandomNode(double MutationRatio, Random randsource)
        {

            double localeMutation = MutationRatio * 0.5 < 0.1 ? 0 : MutationRatio * 0.5;
            switch (randsource.Next(0, 5))
            {
                case 0: //Mutate as BinaryNode
                    return new BinaryNode(
                        GetRandomOperationToken(randsource),
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
                    return GetRandomLeaf(randsource);
            }
            Node MutateLocaly(double localeMutationRatio)
            {
                return randsource.NextDouble() < localeMutationRatio ? GetRandomNode(localeMutationRatio, randsource) : GetRandomLeaf(randsource);
            }
        }
        /// <summary>
        /// Provide a binary operation token randomly
        /// </summary>
        internal static TokenType GetRandomOperationToken(Random randsource)
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
        internal static Node GetRandomLeaf(Random randsource)
        {
            //generate Constant
            if (randsource.NextDouble() < 0.5)
            {
                double value = randsource.NextDouble();
                return new ConstantNode(value == 0 ? 1 : value);
            }
            //generate Identifier
            else
            {
                return new IdentifierNode(randsource.NextDouble() < 0.5 ? "x" : "y");
            }
        }
    }
}
