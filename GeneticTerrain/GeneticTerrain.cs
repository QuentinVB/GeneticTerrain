using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ast;


namespace GeneticTerrain
{
    public class GeneticTerrainGenerator
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;
        int gridSize;
        double mutationChance;


        List<Algorithm> _population;
        BestKeeper<Algorithm> _incubator;

        public List<Algorithm> Population { get => _population;  }
        public BestKeeper<Algorithm> Incubator { get => _incubator; }

        private AstWrapper _wrapper;

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="maxPopulation">the maximum population generated</param>
        /// <param name="maxGeneration">the maximum number of generation</param>
        /// <param name="startAcceptanceRatio">the first ratio for natural selection</param>
        /// <param name="gridSize">the size of the terrain</param>
        /// <param name="mutationChance">the mutation threshold</param>
        public GeneticTerrainGenerator(int maxPopulation, int maxGeneration, double startAcceptanceRatio, int gridSize,double mutationChance)
        {
            if (maxPopulation <= 0) throw new ArgumentException("The max population must be higher than 0",nameof(maxPopulation));
            if (maxGeneration <= 0) throw new ArgumentException("Generations must be higher than 0",nameof(maxGeneration));
            if (startAcceptanceRatio <= 0) throw new ArgumentException("Acceptance ratio must be higher than 0", nameof(startAcceptanceRatio));
            if (gridSize <= 0) throw new ArgumentException("The grid size must be higher than 0", nameof(gridSize));
            if (mutationChance <= 0) throw new ArgumentException("The mutation chance size must be higher than 0", nameof(mutationChance));

            this.maxPopulation = maxPopulation;
            this.maxGeneration = maxGeneration;
            this.startAcceptanceRatio = startAcceptanceRatio;
            this.gridSize = gridSize;
            this.mutationChance = mutationChance;

            this._population = new List<Algorithm>();
            this._incubator = new BestKeeper<Algorithm>(1);

            _wrapper = new AstWrapper();

        }

        /// <summary> OK
        /// https://www.youtube.com/watch?v=PL6jwxw9T3c
        /// evaluate each algorithm and sort them by delta in the incubator
        /// compute delta foreach case the average to produce the delta of the algorithm
        /// Delta close to 0
        /// incubator heap size :inverse law (converging toward 0)
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="startAcceptanceRatio">The start acceptance ratio.</param>
        /// <param name="generation">The generation.</param>
        public void NaturalSelection(List<Algorithm> population, int generation)
        {
            if (generation == 0) throw new DivideByZeroException();

            int incubatorSize = (int)Math.Ceiling( (1/generation)*maxPopulation * startAcceptanceRatio);

            this._incubator = new BestKeeper<Algorithm>(incubatorSize, (a, b) => a.CompareTo(b));

            foreach (Algorithm candidate in population)
            {
                Evaluate(candidate);
                //confront candidate to other (may the odd be ever in his favor)
                _incubator.Add(candidate);
            }
        }/// <summary>
        /// Nested evaluation for reusability
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        private double Evaluate(Algorithm candidate)
        {
            double deltaSum = 0;
            //generate delta from matrix
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    double x = (i - gridSize / 2) * 0.1;
                    double y = -(j - gridSize / 2) * 0.1;

                    //compute local delta
                    double value = _wrapper.Compute(candidate.RootNode, x, y);

                    //Confront to the reality and sum
                    deltaSum += Math.Pow(RealitySource.GetZFromMysteryEquation(x, y) - value, 2);
                    // improvement => store the value of Z from mystery equation to save CPU at a little cost of memory
                }
            } 
            return candidate.Delta = deltaSum / (gridSize * gridSize);
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

        /// <summary>
        /// Mutation
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="mutationChance">The mutation chance.</param>
        private void Mutation(List<Algorithm> population, double mutationChance)
        /*a visitor who travel the tree 
        * 20% chance to mutate a node
        * the node will be substituate with another node chosed randomly among the 5 potential nodes
        * a binary node have to choose randomly between the 4 operation
        * then fill the leaf with a constant node OR a identifier
        * the constant node will be a random number
        */
        {
            foreach (Algorithm candidate in population)
            {
                candidate.RootNode = _wrapper.MutateGraph(candidate.RootNode, mutationChance);
                //log the mutations number ?
                //then optimize and count node
            }
        }

        public Algorithm runSimulation()
        {
            int generation = 1;

            //Create initial population
            string[] lines = File.ReadAllLines(@"../../../../init_pop.txt", Encoding.UTF8);

            string[] t = lines[0].Split(',');
            foreach (string parsedEquation in t)
            {
                /*
                 throw exception : if : another identifier than x or y
                 */
                //if(parsedEquation.Contains()|| ) throw new ArgumentException("The identifier doesn't exist")
                _population.Add(new Algorithm(_wrapper.Parse(parsedEquation), 0));
            }

            do
            {
                //NaturalSelection
                NaturalSelection(_population, generation);
                _population.Clear();

                //Making Couple
                List<(Algorithm, Algorithm)> couples = Meetic(_incubator.ToList());

                //Making Children
                // Shuffle genome between A and B
                /* choose a method randomly => creationnnnn
                 * 1 :  choose a leaf from A randomly and substitute it with the tree from B
                 * 2 : 
                 */

                //Mutate the children
                Mutation(_population, mutationChance);             

                generation++;
            } while (generation < maxGeneration);

            //evaluateAt last ?

            //give me the best one baby !
            return _incubator.RemoveMax();
        }       
    }
}
