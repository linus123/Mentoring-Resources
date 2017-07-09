using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace PairProgrammingExercises.AlgorithmsAndDataStructures.LinkedList
{
    public class SingleLinkedListTests
    {
        [Fact]
        public void ShouldBeAssignableToCollectionOfInt()
        {
            var linkedList = new SingleLinkedList();

            linkedList.Should().BeAssignableTo<ICollection<int>>();
        }

        [Fact]
        public void ShouldHaveZeroCountOnStart()
        {
            var linkedList = new SingleLinkedList();

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
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);

            linkedList.Count.Should().Be(1);

            var array = linkedList.ToArray();

            array.Length.Should().Be(1);
            array[0].Should().Be(value1);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 10)]
        [InlineData(3, 44)]
        public void ShouldHandleTwoValues(
            int value1,
            int value2)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);

            linkedList.Count.Should().Be(2);

            var array = linkedList.ToArray();
            array.Length.Should().Be(2);

            array[0].Should().Be(value1);
            array[1].Should().Be(value2);
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
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            linkedList.Count.Should().Be(3);

            var array = linkedList.ToArray();
            array.Length.Should().Be(3);

            array[0].Should().Be(value1);
            array[1].Should().Be(value2);
            array[2].Should().Be(value3);

        }

        [Fact]
        public void ClearShouldRemoveAllItems()
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);

            linkedList.Clear();

            linkedList.Count.Should().Be(0);

            var enumerator = linkedList.GetEnumerator();

            Assert.Throws<InvalidOperationException>(() =>
            {
                enumerator.MoveNext();
            });
        }

        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 4)]
        public void ContainsShouldReturnTrueWhenItemIsFound(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            linkedList.Contains(value1).Should().BeTrue();
            linkedList.Contains(value2).Should().BeTrue();
            linkedList.Contains(value3).Should().BeTrue();
            linkedList.Contains(-80).Should().BeFalse();
            linkedList.Contains(-20).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 4)]
        public void RemoveShouldWorkWhenRemovingMiddleNodex(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            var wasRemoved = linkedList.Remove(value2);

            wasRemoved.Should().BeTrue();

            linkedList.Count.Should().Be(2);

            var array = linkedList.ToArray();

            array.Length.Should().Be(2);
            array[0].Should().Be(value1);
            array[1].Should().Be(value3);
        }

        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 4)]
        public void RemoveShouldWhenGivenFirstNode(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            var wasRemoved = linkedList.Remove(value1);

            wasRemoved.Should().BeTrue();

            linkedList.Count.Should().Be(2);

            var array = linkedList.ToArray();

            array.Length.Should().Be(2);
            array[0].Should().Be(value2);
            array[1].Should().Be(value3);
        }

        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 4)]
        public void RemoveShouldWhenGivenLastNode(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            var wasRemoved = linkedList.Remove(value3);

            wasRemoved.Should().BeTrue();

            linkedList.Count.Should().Be(2);

            var array = linkedList.ToArray();

            array.Length.Should().Be(2);
            array[0].Should().Be(value1);
            array[1].Should().Be(value2);
        }


        [Theory]
        [InlineData(1, 2, 123)]
        [InlineData(2, 10, 3)]
        [InlineData(3, 44, 4)]
        public void RemoveShouldNotChangeWhenNodeNotRemoved(
            int value1,
            int value2,
            int value3)
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(value1);
            linkedList.Add(value2);
            linkedList.Add(value3);

            var wasRemoved = linkedList.Remove(-300);

            wasRemoved.Should().BeFalse();

            linkedList.Count.Should().Be(3);

            var array = linkedList.ToArray();

            array.Length.Should().Be(3);
            array[0].Should().Be(value1);
            array[1].Should().Be(value2);
            array[2].Should().Be(value3);
        }

        [Fact]
        public void RemoveShouldWorkGivenSingleNode()
        {
            var linkedList = new SingleLinkedList();

            linkedList.Add(100);

            var wasRemoved = linkedList.Remove(100);

            wasRemoved.Should().BeTrue();

            linkedList.Count.Should().Be(0);

            var array = linkedList.ToArray();

            array.Length.Should().Be(0);
        }


    }
}