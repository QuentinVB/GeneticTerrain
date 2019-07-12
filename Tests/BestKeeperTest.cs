using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using Ast;
using System;

namespace Tests
{
    public class BestKeeperTest
    {
        Node fakeNode;
        Algorithm algo1;
        Algorithm algo2;
        Algorithm algo3;
        Algorithm algo4;
        Algorithm algo5;
        Algorithm algo6;

        [SetUp]
        public void Setup()
        {
            fakeNode = new ConstantNode(2);
            algo1 = new Algorithm(fakeNode) { Delta =5};
            algo2 = new Algorithm(fakeNode) { Delta = 10 } ;
            algo3 = new Algorithm(fakeNode) { Delta = 20 } ;
            algo4 = new Algorithm(fakeNode) { Delta = 30 } ;
            algo5 = new Algorithm(fakeNode) { Delta = 50 } ;
            algo6 = new Algorithm(fakeNode) { Delta = 1 } ;
        }
        [Test]
        public void BestKeeper_add_and_indexer()
        {
            //arrange
            var sut = new BestKeeper<Algorithm>(8);
 
            //act and assert
            sut.Count.Should().Be(0);

            sut.Add(algo1);

            sut.Count.Should().Be(1);
        }
        [Test]
        public void bestkeeper_supports_foreach()
        {
            //arrange
            var d = new BestKeeper<int>(8);
            for (int i = 0; i < 8; ++i)
            {
                d.Add(i);
            }
            //act assert
            int c = 0;
            bool[] seen = new bool[8];
            foreach (var j in d)
            {
                j.Should().BeInRange(0, 7);
                c++;
                seen[j].Should().BeFalse();
                seen[j] = true;
            }
            c.Should().Be(8);
        }


        [Test]
        public void bestkeeper_add_comparable()
        {
            //arrange
            var sut = new BestKeeper<Algorithm>(8, (a,b) =>  a.CompareTo(b));

            //act
            sut.Add(algo1);
            sut.Add(algo2);
            sut.Add(algo3);
            sut.Add(algo4);
            sut.Add(algo5);

            sut.Count.Should().Be(5);

            //assert
            sut.RemoveMax().Delta.Should().Be(algo1.Delta);
            sut.RemoveMax().Delta.Should().Be(algo2.Delta);
            sut.RemoveMax().Delta.Should().Be(algo3.Delta);
            sut.RemoveMax().Delta.Should().Be(algo4.Delta);
            sut.RemoveMax().Delta.Should().Be(algo5.Delta);

            sut.Count.Should().Be(0);
        }
        [Test]
        public void bestkeeper_with_limit()
        {
            //arrange
            var sut = new BestKeeper<Algorithm>(5);

            //act
            sut.Add(algo1);
            sut.Add(algo2);
            sut.Add(algo3);
            sut.Add(algo4);
            sut.Add(algo5);
            sut.Add(algo6);


            sut.Count.Should().Be(5);

            //assert
            sut.RemoveMax().Delta.Should().Be(algo6.Delta);
            sut.RemoveMax().Delta.Should().Be(algo1.Delta);
            sut.RemoveMax().Delta.Should().Be(algo2.Delta);
            sut.RemoveMax().Delta.Should().Be(algo3.Delta);
            sut.RemoveMax().Delta.Should().Be(algo4.Delta);
        }
        [Test]
        public void bestkeeper_peekbest()
        {
            //arrange
            var sut = new BestKeeper<Algorithm>(5);

            //act
            sut.Add(algo1);
            sut.Add(algo2);
            sut.Add(algo3);
            sut.Add(algo4);
            sut.Add(algo5);
            sut.Add(algo6);


            sut.Count.Should().Be(5);

            //assert
            sut.PeekBest.Delta.Should().Be(1);
            sut.Count.Should().Be(5);

        }
    }
}