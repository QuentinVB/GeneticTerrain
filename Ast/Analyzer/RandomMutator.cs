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

        public int MutationCount { get; private set; }

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
            //Improvement: Mutate by average; Reduce root mutation ratio through generations ?
            if (randsource.NextDouble()<MutationRatio)
            {
                MutationCount++;

                mutatedNode = RandomNodeSource.GetRandomNode(MutationRatio);
                return true;
            }
            mutatedNode = originalNode;
            return false;
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
