using Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    class ChildrenGenerator
    {
        //push
        public ChildrenGenerator()
        {
            
        }

        // Shuffle genome between A and B
        /* choose a method randomly => creationnnnn
         * 1 :  choose a leaf from A randomly and substitute it with the tree from B
         * 2 : 
         */
        public static Node Breed(Node parent1, Node parent2)
        {
            Random rand = new Random();

            if (rand.NextDouble()<0.2)
            {
                return parent1;
            }
            else if (rand.NextDouble() < 0.4)
            {
                return parent2;
            }

            return 
                new BinaryNode(
                    TokenType.Mult, 
                    new BinaryNode(
                        TokenType.Div, 
                        new BinaryNode(
                            TokenType.Plus, 
                            parent1, 
                            parent2), 
                        new ConstantNode(2)), 
                    new ConstantNode(
                        rand.NextDouble()
                        )
                       );

            //AstWrapper wrapper = new AstWrapper();
            //return wrapper.Breed(parent1,parent2);
        }

        private (Algorithm, Algorithm) FirstElmtInsideSecondElmtSuffle((Algorithm a1, Algorithm a2) algo)
        {
            Algorithm a1, a2,a3;
            Node newNode;
            a1 = algo.a1;
            a2 = algo.a2;
            a3 = new Algorithm();

            newNode = a3.NodeConstructor(a1.RootNode, a1.LeftNode, a1.RightNode);

            a1.RootNode = a2.RightNode;
            a1.LeftNode = null;
            a1.RightNode = null;

            a2.RightNode = newNode;

            var newCouple = (a1, a2);
            return newCouple;
        }

        private (Algorithm, Algorithm) PermuteFirstElmtOneWithSecondElmtSuffle((Algorithm a1, Algorithm a2) algo)
        {
            Algorithm a1, a2, a3;
            Node node1,node2;

            a1 = algo.a1;
            a2 = algo.a2;

            node1 = a1.RightNode;
            node2 = a2.LeftNode;

            a1.RightNode = node2;
            a2.LeftNode = node2;

  
            var newCouple = (a1, a2);
            return newCouple;
        }

        public List<Algorithm> RandomlyAplySuffle(List<(Algorithm a1, Algorithm a2)> algoCouples)
        {
            var suffleGroup1 = new List<(Algorithm, Algorithm)>();
            var suffleGroup2 = new List<(Algorithm, Algorithm)>();
            var newAlgoCouples = new List<(Algorithm, Algorithm)>();
            var totalAlgoList = new List<Algorithm>();
            var random = new Random();

            // list 1 construite de manière aléatoire
            for (int i = 0; i <= algoCouples.Count / 2; i++)
            {
                int index = random.Next(algoCouples.Count);
                foreach (var elmt in algoCouples)
                {
                    suffleGroup1.Add(elmt);
                }

            }

            // list 2 constriute de manière aléatoire
            foreach (var elmt in algoCouples)
            {
                foreach (var el in suffleGroup1)
                {
                    if (elmt.ToString() != el.ToString())
                    {
                        suffleGroup2.Add(elmt);
                    }
                }
            }

            // j'applique le suffle 1 sur la list 1
            foreach (var elmt in suffleGroup1)
            {
                var result = FirstElmtInsideSecondElmtSuffle(elmt);
                newAlgoCouples.Add(result);
            }

            // j'applique le suffle 2 sur list 2
            foreach (var elmt in suffleGroup1)
            {
                var result = PermuteFirstElmtOneWithSecondElmtSuffle(elmt);
                newAlgoCouples.Add(result);
            }

            //je sépare le tuple et j'ajoute à la liste
            foreach (var elmt in newAlgoCouples)
            {
                totalAlgoList.Add(elmt.Item1);
                totalAlgoList.Add(elmt.Item2);
            }
            // je renvoie la liste avec les enfants 
            return totalAlgoList;
        }
    }
}
