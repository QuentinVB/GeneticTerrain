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
        double acceptanceRatio;

        List<Algorithm> population;
        Heap<Algorithm> bestKeeper;

        /*
        max : n loop
        if : 99%  or nth loop : stop
        */
        public GeneticTerrain(int maxPopulation, int maxGeneration, double acceptanceRatio)
        {
            this.maxPopulation = maxPopulation;
            this.maxGeneration = maxGeneration;
            this.acceptanceRatio = acceptanceRatio;
        }

        public void runSimulation()
        {
            int generation = 0;

            do
            {
                //Create initial population
                /*
                 * Inject strings :only x et y as identifier, const unlimited
                 * example : "x+1/2"
                 * exception
                 */

                //Evaluation
                /*
                compute delta foreach case the average to produce the delta of the algorithm 
                Delta close to 0
                incubator heap : taille logarithmique

                 */
                // Meetic : no asct
                /* who fuck who ?
                 * each algorithm choose randomly another to mate with
                 * loop until back to normal population
                 */

                // Breeding : Shuffle genome between A and B : breeding strategy
                /* choose a method randomly => creationnnnn
                 * 1 : choose a leaf from A randomly and substitute it with the tree from B
                 * 2 : average
                 */

                //Mutation : 
                /* visitor who travel the tree 
                 * 20% chance to mutate a node
                 * the node will be substituate with another node chosed randomly among the 5 potential nodes
                 * a binary node have to choose randomly between the 4 operation
                 * then fill the leaf with a constant node OR a identifier
                 * the constant node will be a random number
                 */

                 //optimization + node number

                //DO IT AGAIN :)

                generation++;

            } while (generation < maxGeneration);
        }        
    }
}
