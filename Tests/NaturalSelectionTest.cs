using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using System;
using Ast;
using System.Collections.Generic;

namespace Tests
{
    public class NaturalSelectionTest
    {
        Node fakeNode;
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
            algo1 = new Algorithm(fakeNode, 5);
            algo2 = new Algorithm(fakeNode, 10);
            algo3 = new Algorithm(fakeNode, 20);
            algo4 = new Algorithm(fakeNode, 30);
            algo5 = new Algorithm(fakeNode, 50);
            algo6 = new Algorithm(fakeNode, 1);
            list = new List<Algorithm>() { algo1, algo2, algo3, algo4, algo5, algo6 };

        }

        [Test]
        [TestCase(1,6,0.5)]
        public void geneticTerrain_startup(int generation, int maxPopulation, double startAcceptanceRatio)
        {
            //arrange
            
            int incubatorSize = (int)Math.Ceiling((1 / generation) * maxPopulation * startAcceptanceRatio);

            var sut = new GeneticTerrain.GeneticTerrain(maxPopulation, 2, startAcceptanceRatio, 10);
            //act 
            sut.NaturalSelection(list, generation);


            //assert
            sut.Incubator.MaxCount.Should().Be(incubatorSize);
            sut.Incubator.RemoveMax().Delta.Should().Be(0.285);

        }

        [Test]
        [TestCase(1, 0, 0.5)]
        public void geneticTerrain_exception(int generation, int maxPopulation, double startAcceptanceRatio)
        {
            //arrange
            var sut = new GeneticTerrain.GeneticTerrain(maxPopulation, 2, startAcceptanceRatio, 10);
            //act 
            //assert

            sut.Invoking(a => a.NaturalSelection(list, generation))
                .Should().Throw<KeyNotFoundException>();


        }

    }
}