using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;

namespace Tests
{
    public class AlgorithmTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void initializeAlgorithm()
        {
            //arrange
            Algorithm sut = new Algorithm(new Node(), 0);
            //act

            //assert
            sut.Delta.Should().Be(0);
        }
        [Test]
        [TestCase(50,80,-1)]
        [TestCase(80,50,1)]
        [TestCase(80,80,0)]
        public void compareAlgorithm(int deltaA, int deltaB, int compare)
        {
            //arrange
            Algorithm sut1 = new Algorithm(new Node(), deltaA);
            Algorithm sut2 = new Algorithm(new Node(), deltaB);
            //act

            //assert
            sut1.CompareTo(sut2).Should().Be(compare);
        }
        [Test]
        [TestCase(50,10)]
        [TestCase(80, 50)]
        [TestCase(80, 70)]
        public void toStringAlgorithm(int delta, int nodeCount)
        {
            //arrange
            Algorithm sut1 = new Algorithm(new Node(), delta);
            sut1.NodeCount = nodeCount;
            //act

            //assert
            sut1.ToString().Should().Be($"Algorithm,Delta:{delta},NodeCount:{nodeCount},Tree:PRINTVISITOR");
        }
    }
}