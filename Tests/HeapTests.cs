using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using System;
using Ast;

namespace Tests
{
    public class HeapTests
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
            algo1 = new Algorithm(fakeNode, 5);
            algo2 = new Algorithm(fakeNode, 10);
            algo3 = new Algorithm(fakeNode, 20);
            algo4 = new Algorithm(fakeNode, 30);
            algo5 = new Algorithm(fakeNode, 50);
            algo6 = new Algorithm(fakeNode, 1);
        }

        [Test]
        public void heap_add_and_indexer()
        {
            //arrange
            var sut = new Heap<int>(8);

            //act and assert
            sut.Count.Should().Be(0);

            sut.Add(3);

            sut.Count.Should().Be(1);

        }
        [Test]
        public void heap_add_and_popMax()
        {
            //arrange
            var sut = new Heap<int>(8);

            //act and assert

            sut.Add(3);
            sut.Count.Should().Be(1);

            sut.Add(8);
            sut.Count.Should().Be(2);

            sut.RemoveMax().Should().Be(8);
            sut.Count.Should().Be(1);

        }
        [Test]
        public void heap_add_comparable()
        {
            //arrange
            var sut = new Heap<Algorithm>(8);


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
        /*
        [Test]
        public void heap_supports_foreach()
        {
            //arrange
            var d = new Heap<int>(50);
            for (int i = 0; i < 300; ++i)
            {
                d.Add(i);
            }
            //act assert
            int c = 0;
            bool[] seen = new bool[50];
            foreach (var j in d)
            {
                j.Should().BeInRange(0, 299);
                c++;
                seen[j].Should().BeFalse();
                seen[j] = true;
            }
            c.Should().Be(50);
        }
        */
        [Test]
        public void heap_with_limit()
        {
            //arrange
            var sut = new Heap<Algorithm>(5);

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

        /*
         *  [Test]
        public void heap_add_and_indexer()
        {
            var d = new Heap<int, string>();
            // Assert.That( d.Count == 0 );
            // Assert.That( d.Count, Is.EqualTo( 0 ) );
            d.Count.Should().Be(0);
            d.Add(3, "Three");
            d.Count.Should().Be(1);
            d[4] = "Four";
            d.Count.Should().Be(2);

            d.Invoking(x => x.Add(4, "Four bis"))
               .Should().Throw<Exception>();

            d.Invoking(sut => Console.WriteLine(sut[3712]))
                .Should().Throw<KeyNotFoundException>();

            d.Invoking(x => d[3] = "Three bis")
               .Should().NotThrow();

            d.Count.Should().Be(2);
        }
       [Test]
       public void simple_dictionary_add_and_indexer()
       {
           var d = new MyDictionary<int, string>();
           // Assert.That( d.Count == 0 );
           // Assert.That( d.Count, Is.EqualTo( 0 ) );
           d.Count.Should().Be(0);
           d.Add(3, "Three");
           d.Count.Should().Be(1);
           d[4] = "Four";
           d.Count.Should().Be(2);

           d.Invoking(x => x.Add(4, "Four bis"))
              .Should().Throw<Exception>();

           d.Invoking(sut => Console.WriteLine(sut[3712]))
               .Should().Throw<KeyNotFoundException>();

           d.Invoking(x => d[3] = "Three bis")
              .Should().NotThrow();

           d.Count.Should().Be(2);
       }

       [Test]
       public void simple_dictionary_add_and_remove()
       {
           var d = new MyDictionary<string, int>();
           d.Add("One", 1);
           d.Add("Two", 2);
           d.Add("Three", 3);
           d.Count.Should().Be(3);

           d.Remove("Two");
           d.Count.Should().Be(2);
           d.Invoking(sut => sut.Remove("Two"))
               .Should().NotThrow();
           d.Count.Should().Be(2);
       }

       [Test]
       public void simple_dictionary_grow_triggered()
       {
           var d = new MyDictionary<int, int>();
           for (int i = 0; i < 300; ++i)
           {
               d.Add(i, i);
           }
           for (int i = 0; i < 300; ++i)
           {
               d[i].Should().Be(i);
               d.Invoking(sut => Console.Write(sut[-i - 1]))
                   .Should().Throw<KeyNotFoundException>();
           }
       }

       */
    }
}