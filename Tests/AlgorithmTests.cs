using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using Ast;

namespace Tests
{
    public class AlgorithmTests
    {
        Node fakeNode;
        AstWrapper wrapper = new AstWrapper();

        [SetUp]
        public void Setup()
        {
            fakeNode = new ConstantNode(2);
        }
        [Test]
        public void initializeAlgorithm()
        {
            //arrange
            Algorithm sut = new Algorithm(fakeNode);
            //act
            //assert
            sut.Delta.Should().Be(0);
        }
        [Test]
        [TestCase(50,80,1)]
        [TestCase(80,50,-1)]
        [TestCase(80,80,0)]
        public void compareAlgorithm(int deltaA, int deltaB, int compare)
        {
            //arrange
            Algorithm sut1 = new Algorithm(fakeNode) { Delta = deltaA } ;
            Algorithm sut2 = new Algorithm(fakeNode) { Delta = deltaB };
            //act

            //assert
            sut1.CompareTo(sut2).Should().Be(compare);
        }

        [Test]
        [TestCase("x",2,1)]
        [TestCase("y+y", 32,3)]
        [TestCase("y+9+8", 4,5)]
        public void NodeCountAlgorithm(string toParse,int delta,int nodeCount)
        {
            //arrange
            
            //act
            Algorithm sut = new Algorithm(wrapper.Parse(toParse)) { Delta = delta };

            //assert
            sut.NodeCount.Should().Be(nodeCount);
        }

        [Test]
        [TestCase(50)]
        [TestCase(80)]
        public void toStringAlgorithm(int delta)
        {
            //arrange
            Algorithm sut1 = new Algorithm(fakeNode) { Delta = delta };
            //act

            //assert
            sut1.ToString().Should().Be($"Algorithm,Delta:{delta},NodeCount:1,Tree:2");
        }
    }
}