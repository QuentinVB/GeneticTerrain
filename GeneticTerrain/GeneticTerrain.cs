using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneticTerrain
{
    public class GeneticTerrain
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;

        List<Algorithm> population;
        Heap<Algorithm> incubator;

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="maxPopulation"></param>
        /// <param name="maxGeneration"></param>
        /// <param name="startAcceptanceRatio"></param>
        public GeneticTerrain(int maxPopulation, int maxGeneration, double startAcceptanceRatio)
        {
            this.maxPopulation = maxPopulation;
            this.maxGeneration = maxGeneration;
            this.startAcceptanceRatio = startAcceptanceRatio;
        }
        /// <summary>
        /// evaluate each algorithm and sort them by delta in the incubator
        /// </summary>
        /// <param name="population"></param>
        /// <param name="startAcceptanceRatio"></param>
        /// <param name="generation"></param>
        private void NaturalSelection(List<Algorithm> population, double startAcceptanceRatio, int generation)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Meetic : who fucks who ?
        /// each algorithm choose randomly another algorithm to mate with
        /// ,it loop until number of couple is back to the max population
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Couple> Meetic(List<Algorithm> p)
        {  
            var AlgorithmCouples = new List<Couple>();
            var random = new Random();
            foreach (var elmt in p)
            {
                for (int i = 0; i <= p.Count; i++)
                {
                    int index = random.Next(p.Count);
                    AlgorithmCouples.Add(new Couple(elmt, p[index]));
                    if(AlgorithmCouples.Count==maxPopulation) return AlgorithmCouples;
                }
            }
            return AlgorithmCouples;
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

                //Evaluation
                /*
                compute delta foreach case the average to produce the delta of the algorithm 
                Delta close to 0
                incubator heap : taille logarithmique

               

                */
                List<Couple> couples = Meetic(population);//incubator.ToList()
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
