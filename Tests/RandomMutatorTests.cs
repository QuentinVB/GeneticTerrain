using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using Ast;
using System;

namespace Tests
{
    public class RandomMutatorTests
    {
        Node fakeNode;

        [SetUp]
        public void Setup()
        {
            fakeNode = new ConstantNode(2);
        }
        [Test]
        [TestCase("8+x")]
        [TestCase("y")]
        [TestCase("2/x")]
        [TestCase("3+2*y/x")]
        public void Mutator_parse(string toParse)
        {
            //arrange
            AstWrapper sut = new AstWrapper();
            var n = sut.Parse(toParse);
            string print = sut.Print(n);
            //act
            var nut = sut.MutateGraph(n, 1.0);

            //assert
            sut.Print(nut).Should().NotBe(print);
            Console.WriteLine(sut.Print(nut));
        }      
    }
}