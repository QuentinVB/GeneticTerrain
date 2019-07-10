using NUnit.Framework;
using FluentAssertions;
using GeneticTerrain;
using System;

namespace Tests
{
    public class HeapTests
    {

        [Test]
        public void heap_add_and_indexer()
        {
            //arrange
            var sut = new Heap<int>();

            //act and assert
            sut.Count.Should().Be(0);

            sut.Add(3);

            sut.Count.Should().Be(1);

        }
        [Test]
        public void heap_add_and_popMax()
        {
            //arrange
            var sut = new Heap<int>();

            //act and assert

            sut.Add(3);
            sut.Count.Should().Be(1);

            sut.Add(8);
            sut.Count.Should().Be(2);

            sut.RemoveMax().Should().Be(8);
            sut.Count.Should().Be(1);

        }
        [Test]
        public void heap_add_and_clear()
        {
            //arrange
            var sut = new Heap<int>();

            //act and assert

            sut.Add(3);
            sut.Add(45);
            sut.Add(8);

            sut.Count.Should().Be(3);

            sut.Clear();

            sut.Count.Should().Be(0);

        }
        [Test]
        public void heap_add_comparable()
        {
            //arrange
            var sut = new Heap<Algorithm>();
            var fakeNode = new Node();

            Algorithm sut1 = new Algorithm(fakeNode, 25);
            Algorithm sut2 = new Algorithm(fakeNode, 525);
            Algorithm sut3 = new Algorithm(fakeNode, 78);

            //act
            sut.Add(sut1);
            sut.Add(sut2);
            sut.Add(sut3);

            sut.Count.Should().Be(3);

            //assert
            sut.RemoveMax().Delta.Should().Be(525);
            sut.RemoveMax().Delta.Should().Be(78);
            sut.RemoveMax().Delta.Should().Be(25);

            sut.Count.Should().Be(0);

        }
        [Test]
        public void heap_supports_foreach()
        {
            //arrange
            var d = new Heap<int>();
            for (int i = 0; i < 300; ++i)
            {
                d.Add(i);
            }
            //act assert
            int c = 0;
            bool[] seen = new bool[300];
            foreach (var j in d)
            {
                j.Should().BeInRange(0, 299);
                c++;
                seen[j].Should().BeFalse();
                seen[j] = true;
            }
            c.Should().Be(300);
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