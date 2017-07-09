using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PairProgrammingExercises.AlgorithmsAndDataStructures.LinkedList
{
    public class LinkedListTests
    {
        [Fact]
        public void ShouldBeAssignableToCollectionOfInt()
        {
            var linkedList = new LinkedList();

            linkedList.Should().BeAssignableTo<ICollection<int>>();
        }

        [Fact]
        public void ShouldHaveZeroCountOnStart()
        {
            var linkedList = new LinkedList();

            linkedList.Count.Should().Be(0);
            linkedList.IsReadOnly.Should().BeFalse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldHandleSingleValue(
            int value1)
        {
            var linkedList = new LinkedList();

            linkedList.Add(value1);

            linkedList.Count.Should().Be(1);

            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value1);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 10)]
        [InlineData(3, 44)]
        public void ShouldHandleTwoValues(
            int value1,
            int value2)
        {
            var linkedList = new LinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);

            linkedList.Count.Should().Be(2);

            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value2);
        }

        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 3)]
        public void ShouldHandleThreeValues(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new LinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            linkedList.Count.Should().Be(3);

            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value2);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(value3);

        }

    }
}