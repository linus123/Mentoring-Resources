using System.Collections;
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

        [Fact]
        public void AddShouldSetTheCountTo1()
        {
            var linkedList = new LinkedList();

            linkedList.Add(1);

            linkedList.Count.Should().Be(1);
        }

    }

    public class LinkedList : ICollection<int>
    {
        private int _itemCount;

        public LinkedList()
        {
            _itemCount = 0;
        }

        public IEnumerator<int> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(int item)
        {
            _itemCount++;
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(int item)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get { return _itemCount; }

        }
        public bool IsReadOnly { get; }
    }
}