using System;
using System.Collections;
using System.Collections.Generic;

namespace PairProgrammingExercises.AlgorithmsAndDataStructures.LinkedList
{
    public class SingleLinkedList : ICollection<int>
    {
        private int _itemCount;

        private Node _head;

        public SingleLinkedList()
        {
            _itemCount = 0;
            _head = null;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var currentNode = _head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }

            throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node GetLastNode()
        {
            Node lastNode = null;

            ForEachNode(node =>
            {
                if (node.Next == null)
                {
                    lastNode = node;
                    return false;
                }

                return true;
            });

            return lastNode;
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
            _itemCount = 0;
            _head = null;
        }

        public bool Contains(int item)
        {
            bool isFound = false;

            ForEachNode(node =>
            {
                if (node.Value == item)
                {
                    isFound = true;
                    return false;
                }
                return true;
            });

            return isFound;
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            var indexCount = 0;

            ForEachNode(node =>
            {
                if (indexCount >= arrayIndex)
                    array[indexCount] = node.Value;

                indexCount++;
                return true;
            });
        }

        public bool Remove(int item)
        {
            Node previousNode = null;
            Node currentNode = null;

            ForEachNode(node =>
            {
                currentNode = node;

                if (currentNode.Value == item)
                {
                    return false;
                }

                previousNode = currentNode;
                currentNode = currentNode.Next;

                return true;
            });

            if (currentNode == null)
                return false;

            if (previousNode == null)
                _head = currentNode.Next;
            else
                previousNode.Next = currentNode.Next;

            _itemCount--;

            return true;
        }

        public int Count
        {
            get { return _itemCount; }

        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        private void ForEachNode(
            Func<Node, bool> actionWithContinue)
        {
            var currentNode = _head;
            var shouldContinue = true;

            while ((currentNode != null) && (shouldContinue))
            {
                shouldContinue = actionWithContinue(currentNode);
                currentNode = currentNode.Next;
            }
        }

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