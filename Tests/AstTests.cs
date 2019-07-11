using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using Ast;
using System;

namespace Tests
{
    public class AstTests
    {
        Node fakeNode;

        [SetUp]
        public void Setup()
        {
            fakeNode = new ConstantNode(2);
        }
        [Test]
        public void Ast_parse()
        {
            //arrange
            AstWrapper sut = new AstWrapper();
            //act
            var n = sut.Parse("x+1");
            //assert
            n.Should().BeOfType<BinaryNode>();

            (n as BinaryNode).Type.Should().Be(TokenType.Plus);
            (n as BinaryNode).Left.Should().BeOfType<IdentifierNode>();
            (n as BinaryNode).Right.Should().BeOfType<ConstantNode>();

            ((n as BinaryNode).Left as IdentifierNode).Identifier.Should().Be("x");
            ((n as BinaryNode).Right as ConstantNode).Value.Should().Be(1);
        }
        [Test]
        [TestCase("8+1",8+1)]
        [TestCase("8-23?9+5:4", 8 - 23 >0? 9 + 5 : 4)]
        [TestCase("-2+2", -2 + 2)]
        public void Ast_compute(string toCompute, double result)
        {
            //arrange
            AstWrapper sut = new AstWrapper();
            //act
            var n = sut.Parse(toCompute);
            //assert
            sut.Compute(n, 0,0).Should().Be(result);       
        }
        [Test]
        [TestCase("8+x",5,5, 8+5)]
        [TestCase("y+x",5,5, 5+5)]
        [TestCase("y/x",5,12, 12.0/5.0 )]
        [TestCase("x+y?x+1:-x",2, -4, 2 + -4>0 ? 2 + 1 : -2)]
        public void Ast_compute_withIdentifier(string toCompute, double x,double y,double result)
        {
            //arrange
            AstWrapper sut = new AstWrapper();
            //act
            var n = sut.Parse(toCompute);
            //assert
            sut.Compute(n, x, y).Should().Be(result);
        }
        [Test]
        [TestCase("8+a+x", 5, 5, 8 + 5)]
        public void Ast_compute_with_wrong_Identifier(string toCompute, double x, double y, double result)
        {
            //arrange
            AstWrapper sut = new AstWrapper();
            //act
            var n = sut.Parse(toCompute);
            //assert
            sut.Invoking(a => a.Compute(n, x, y))
               .Should().Throw<ArgumentException>();
        }
    }
}