﻿using System;
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
        readonly int maxPopulation;
        readonly int maxGeneration;
        readonly double startAcceptanceRatio;
        readonly int gridSize;
        readonly double mutationChance;

        List<Algorithm> _population;
        BestKeeper<Algorithm> _incubator;

        //Only for the tests
        public List<Algorithm> Population { get => _population;  }
        public BestKeeper<Algorithm> Incubator { get => _incubator; }

        private GenerateStringGraph _string_graph = new GenerateStringGraph();
        private AstWrapper _wrapper;
        private Logger logger;

        Random randomsource;

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="parameter">the size of the terrain</param>
        /// <param name="logger">the logger object</param>
        public GeneticTerrainGenerator(GeneticParameters options, Logger logger=null)
        {
            this.maxPopulation = options.MaxPopulation;
            this.maxGeneration = options.MaxGeneration;
            this.startAcceptanceRatio = options.StartAcceptanceRatio;
            this.gridSize = options.GridSize;
            this.mutationChance = options.MutationChance;
            this.logger = logger ?? new Logger();

            this._population = new List<Algorithm>();
            this._incubator = new BestKeeper<Algorithm>(1);

            _wrapper = new AstWrapper();
            randomsource = new Random();
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

            int incubatorSize = Convert.ToInt32(1.0 / generation * maxPopulation * startAcceptanceRatio);

            this._incubator = new BestKeeper<Algorithm>(incubatorSize<= 1 ? 1: incubatorSize, (a, b) => a.CompareTo(b));

            foreach (Algorithm candidate in population)
            {
                Evaluate(candidate);
                //confront candidate to other (may the odd be ever in his favor)
                if (!double.IsNaN(candidate.Delta))_incubator.Add(candidate);
            }
            //peek best delta
            var bestdelta = _incubator.PeekBest != null ? _incubator.PeekBest.Delta : 0.0;
            logger.Log($" Best delta {bestdelta}");
        }

        /// <summary>
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
        /// <param name="survivors"></param>
        /// <returns></returns>
        public List<(Algorithm, Algorithm)> Meetic(List<Algorithm> survivors)
        {
            var AlgorithmCouples = new List<(Algorithm, Algorithm)>();
            
            foreach (Algorithm individual in survivors)
            {
                for (int i = 0; i <= (int)Math.Ceiling((double)(maxPopulation / survivors.Count)); i++)
                {                   
                    int index = randomsource.Next(survivors.Count);
                    AlgorithmCouples.Add((individual, survivors[index]));
                    if (AlgorithmCouples.Count == maxPopulation) return AlgorithmCouples;
                }
            }

            return AlgorithmCouples;
        }

        private void BreedChildren(List<(Algorithm, Algorithm)> couples)
        {
            foreach ((Algorithm, Algorithm) couple in couples)
            {
                Node child = ChildrenGenerator.Breed(couple.Item1.RootNode, couple.Item2.RootNode);
                _population.Add(new Algorithm(child));
                if (_population.Count >= maxPopulation) break;
                
            }
        }

        /// <summary>
        /// a visitor who travel the tree 
        /// 20% chance to mutate a node
        /// the node will be substituate with another node chosed randomly among the 5 potential nodes
        ///  a binary node have to choose randomly between the 4 operation
        ///  then fill the leaf with a constant node OR a identifier
        ///  the constant node will be a random number
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="mutationChance">The mutation chance.</param>
        private string Mutation(List<Algorithm> population, double mutationChance)
        {
            double mutationSum = 0;
            foreach (Algorithm candidate in population)
            {
                
                candidate.RootNode = _wrapper.MutateGraph(candidate.RootNode, mutationChance, randomsource, out int mutationCount);

                candidate.RootNode = _wrapper.OptimizeGraph(candidate.RootNode);

                mutationSum += mutationCount / candidate.NodeCount;
            }

            return $" {Math.Round( mutationSum/population.Count *100.0,2)}% of mutations on {population.Count} elements";
        }



        public Algorithm RunSimulation()
        {
            int generation = 1;
            logger.Log("Create initial population");

            //Fichier en dur pour les tests de Quentin
            //string[] lines = File.ReadAllLines(@"../../../init_pop.txt", Encoding.UTF8);
            //string[] t = lines[0].Split(',');

            //Deprecated
            /*
            string line = _string_graph.CreateRandomPopulation(maxPopulation);
            string[] t = line.Split(',');
            foreach (string parsedEquation in t)
            {
                /*
                 throw exception : if : another identifier than x or y
                 
            //if(parsedEquation.Contains()|| ) throw new ArgumentException("The identifier doesn't exist")
            _population.Add(new Algorithm(_wrapper.Parse(parsedEquation)));
                }
            */

            for (int i = 0; i < maxPopulation; i++)
            {
                var tree = _wrapper.GetRandomGraph(randomsource);
                _population.Add(new Algorithm(tree));
            }

            do
            {
                //Natural selection
                NaturalSelection(_population, generation);

                //Making couple
                List<(Algorithm, Algorithm)> couples = Meetic(_incubator.ToList());
                //logger.Log($"couples {couples.Count}");

                //Since we didnt make babies yet, the previous generation population must not be destroyed !
                _population.Clear();

                //Making Children
                BreedChildren(couples);

                //logger.Log($"pop {_population.Count}");

                // Mutate the children and log
                logger.Log($"Generation {generation} " + Mutation(_population, mutationChance));

                generation++;
            } while (generation < maxGeneration);
            logger.Log($"End Generations");

            //evaluateAtLeast
            NaturalSelection(_population, generation);
            //give me the best one baby !
            return _incubator.RemoveMax();
        }

        
    }
}
