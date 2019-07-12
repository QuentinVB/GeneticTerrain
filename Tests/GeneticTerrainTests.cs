using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using System;
using Ast;
using System.Collections.Generic;

namespace Tests
{
    public class GeneticTerrainTests
    {
        Node fakeNode;
        Node fakeNode2;
        Node fakeNode3;
        Algorithm algo1;
        Algorithm algo2;
        Algorithm algo3;
        Algorithm algo4;
        Algorithm algo5;
        Algorithm algo6;

        List<Algorithm> list;

        [SetUp]
        public void Setup()
        {
            fakeNode = new ConstantNode(2);
            fakeNode2 = new IdentifierNode("x");
            fakeNode3 = new IdentifierNode("y");
            algo1 = new Algorithm(fakeNode) { Delta = 5 };
            algo2 = new Algorithm(fakeNode) { Delta = 10 };
            algo3 = new Algorithm(fakeNode) { Delta = 20 };
            algo4 = new Algorithm(fakeNode) { Delta = 30 };
            algo5 = new Algorithm(fakeNode) { Delta = 50 };
            algo6 = new Algorithm(fakeNode) { Delta = 1 };
            list = new List<Algorithm>() { algo1, algo2, algo3, algo4, algo5, algo6 };
        }
        /*
        [Test]
        [TestCase(1,6,0.5)]
        public void geneticTerrain_startup(int generation, int maxPopulation, double startAcceptanceRatio)
        {
            //arrange
            int incubatorSize = (int)Math.Ceiling((1 / generation) * maxPopulation * startAcceptanceRatio);
            var sut = new GeneticTerrainGenerator(maxPopulation, 2, startAcceptanceRatio, 20,0.2);
            //act 
            sut.runSimulation();
            //assert
            sut.Incubator.MaxCount.Should().Be(incubatorSize);
        }*/
    }
}