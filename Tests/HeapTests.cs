using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class HeapTests
    {
        /*
        [Test]
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

       [Test]
       public void simple_dictionary_supports_foreach()
       {
           var d = new MyDictionary<int, int>();
           for (int i = 0; i < 300; ++i)
           {
               d.Add(i, i);
           }
           int c = 0;
           bool[] seen = new bool[300];
           foreach (var kv in d)
           {
               kv.Key.Should().BeInRange(0, 299);
               c++;
               seen[kv.Key].Should().BeFalse();
               seen[kv.Key] = true;
           }
           c.Should().Be(300);
       }*/
    }
}