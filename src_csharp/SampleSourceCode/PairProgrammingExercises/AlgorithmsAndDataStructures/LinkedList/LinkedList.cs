using System.Collections;
using System.Collections.Generic;

namespace PairProgrammingExercises.AlgorithmsAndDataStructures.LinkedList
{
    public class LinkedList : ICollection<int>
    {
        private int _itemCount;

        private Node _head;

        public LinkedList()
        {
            _itemCount = 0;
            _head = null;
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (HasAnyNodes)
            {
                var currentNode = _head;

                while (currentNode != null)
                {
                    yield return currentNode.Value;
                    currentNode = currentNode.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node GetLastNode()
        {
            var currentNode = _head;

            while (currentNode != null)
            {
                if (currentNode.Next == null)
                    return currentNode;

                currentNode = currentNode.Next;
            }

            return null;
        }

        public void Add(int item)
        {
            if (!HasAnyNodes)
            {
                _head = new Node(item);
            }
            else
            {
                var lastNode = GetLastNode();

                var newNode = new Node(item);
                lastNode.Next = newNode;
            }

            _itemCount++;
        }

        private bool HasAnyNodes
        {
            get
            {
                return _head != null;
            }
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

        private class Node
        {
            public Node(
                int value)
            {
                Value = value;
                Next = null;
            }

            public Node(
                int value,
                Node next) : this(value)
            {
                Value = value;
            }

            public int Value { get; }
            public Node Next { get; set; }
        }
    }
}