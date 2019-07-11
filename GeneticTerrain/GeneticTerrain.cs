using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ast;


namespace GeneticTerrain
{
    public class GeneticTerrain
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;
        int gridSize;


        List<Algorithm> population;
        Heap<Algorithm> incubator;

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="maxPopulation">the maximum population generated</param>
        /// <param name="maxGeneration">the maximum number of generation</param>
        /// <param name="startAcceptanceRatio">the first ratio for natural selection</param>
        /// <param name="gridSize">the size of the terrain</param>
        public GeneticTerrain(int maxPopulation, int maxGeneration, double startAcceptanceRatio, int gridSize)
        {
            this.maxPopulation = maxPopulation;
            this.maxGeneration = maxGeneration;
            this.startAcceptanceRatio = startAcceptanceRatio;
            this.gridSize = gridSize;

            this.population = new List<Algorithm>();
            this.incubator = new Heap<Algorithm>((int)Math.Ceiling(maxPopulation * startAcceptanceRatio));
        }

        /// <summary>
        /// evaluate each algorithm and sort them by delta in the incubator
        /// compute delta foreach case the average to produce the delta of the algorithm
        /// Delta close to 0
        /// incubator heap : taille logarithmique
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="startAcceptanceRatio">The start acceptance ratio.</param>
        /// <param name="generation">The generation.</param>
        private void NaturalSelection(List<Algorithm> population, double startAcceptanceRatio, int generation)
        {
            /*
            int incubatorSize = (int)Math.Ceiling(maxPopulation * startAcceptanceRatio) 
            foreach (Algorithm candidate in population)
            {
                double deltaSum = 0;
                //generate delta for matriw
                for (int i = 0; i < gridSize; i++)
                {
                    for (int i = 0; i < gridSize; i++)
                    {
                        double x = (i - gridSize / 2) * 0.1;
                        double y = -(j - gridSize / 2) * 0.1;
                        //INSERT COMPUTE FROM WRAPPER HERE
                        double value = AstWrapper.Compute(candidate.RootNode,)
                    }
                }
               
                candidate.Delta =
            }*/
        }


        /// <summary>
        /// Meetic : who fucks who ?
        /// each algorithm choose randomly another algorithm to mate with
        /// ,it loop until number of couple is back to the max population
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<(Algorithm, Algorithm)> Meetic(List<Algorithm> p)
        {
            var AlgorithmCouples = new List<(Algorithm, Algorithm)>();
            var random = new Random();
            foreach (var elmt in p)
            {
                for (int i = 0; i <= p.Count; i++)
                {
                    int index = random.Next(p.Count);
                    AlgorithmCouples.Add((elmt, p[index]));
                    if (AlgorithmCouples.Count == maxPopulation) return AlgorithmCouples;
                }
            }
            return AlgorithmCouples;
        }


    
        private (Algorithm, Algorithm) FirstElmtInsideSecondElmtSuffle((Algorithm a1, Algorithm a2) algo)
        {
            Algorithm a1, a2;
            a1 = null;
            a2 = null;
            // reste à implemewnter avec le mutationVisitor
            var newCouple = (a1,a2);
            return newCouple;
        }

        private (Algorithm, Algorithm) PermuteFirstElmtOneWithSecondElmtSuffle((Algorithm a1, Algorithm a2) algo)
        {
            Algorithm a1, a2;

            a1 = null;
            a2 = null;
            // reste à implemewnter avec le mutationVisitor 
            var newCouple = (a1, a2);
            return newCouple;
        }

        private List<Algorithm> RandomlyAplySuffle(List<(Algorithm a1, Algorithm a2)> algoCouples)
        {
            var suffleGroup1 = new List<(Algorithm,Algorithm)>();
            var suffleGroup2 = new List<(Algorithm,Algorithm)>();
            var newAlgoCouples = new List<(Algorithm, Algorithm)>();
            var totalAlgoList = new List<Algorithm>();
            var random = new Random();
            
            // list 1
            for(int i = 0; i<= algoCouples.Count/2; i++)
            {
                int index = random.Next(algoCouples.Count);
                foreach(var elmt in algoCouples )
                {
                    suffleGroup1.Add(elmt);
                }
                
            }

            // list 2
            foreach(var elmt in algoCouples)
            {
                foreach(var el in suffleGroup1)
                {
                    if (elmt.ToString() != el.ToString())
                    {
                        suffleGroup2.Add(elmt);
                    }
                }
            }

            // j'applique le suffle 1 sur la list 1
            foreach(var elmt in suffleGroup1)
            {
                var result = FirstElmtInsideSecondElmtSuffle(elmt);
                newAlgoCouples.Add(result);
            }

            // j'applique le suffle 2 sur list 2
            foreach(var elmt in suffleGroup1)
            {
                var result = PermuteFirstElmtOneWithSecondElmtSuffle(elmt);
                newAlgoCouples.Add(result);
            }

            //je sépare le tuple et j'ajoute à la liste
            foreach(var elmt in newAlgoCouples)
            {
                totalAlgoList.Add(elmt.Item1);
                totalAlgoList.Add(elmt.Item2);
            }

            return totalAlgoList;
        }
      

        public Algorithm runSimulation()
        {
            int generation = 0;

            //Create initial population
            /*
             * Inject strings :only x et y as identifier, const unlimited
             * example : "x+1/2"
             * exception
             */

            do
            {
                NaturalSelection(population, startAcceptanceRatio, generation);

                List<(Algorithm, Algorithm)> couples = Meetic(population);//incubator.ToList()
                population.Clear();

                // Shuffle genome between A and B
                /* choose a method randomly => creationnnnn
                 * 1 :  choose a leaf from A randomly and substitute it with the tree from B
                 * 2 : 
                 */

                //Mutation : 
                /* visitor who travel the tree 
                 * 20% chance to mutate a node
                 * the node will be substituate with another node chosed randomly among the 5 potential nodes
                 * a binary node have to choose randomly between the 4 operation
                 * then fill the leaf with a constant node OR a identifier
                 * the constant node will be a random number
                 */
                 //optimizer

                //DO IT AGAIN :)

                generation++;

            } while (generation < maxGeneration);

            //evaluateAt last

            return incubator.RemoveMax();
        }

        
    }
}
