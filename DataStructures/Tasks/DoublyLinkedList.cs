using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private int _length = 0;
        private ListNode _head;

        public int Length => _length;

        public void Add(T e)
        {
            var newNode = new ListNode(e);
            var temp = _head;
            _length++;

            if (_head == null)
            {
                _head = newNode;
                return;
            }

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Next = newNode;
            newNode.Previous = temp;
        }

        public void AddAt(int index, T e)
        {
            var newNode = new ListNode(e);
            _length++;

            if (_head == null)
            {
                _head = newNode;
                return;
            }

            if (index == 0)
            {
                newNode.Next = _head;
                newNode.Next.Previous = newNode;
                _head = newNode;
                return;
            }

            var count = 0;
            var temp = _head;
            while (count < index && temp.Next != null)
            {
                temp = temp.Next;
                count++;
            }

            newNode.Next = temp.Next;
            newNode.Previous = temp;
            if (temp.Previous != null)
            {
                temp.Previous.Next = newNode;
            }
            temp.Next = newNode;
        }

        public T ElementAt(int index)
        {
            ValidateIndex(index);

            var count = 0;
            var temp = _head;
            while (count < index && temp.Next != null)
            {
                temp = temp.Next;
                count++;
            }

            return temp.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_head);
        }

        public void Remove(T item)
        {
            if (_head == null)
            {
                return;
            }

            if (_head.Equals(item))
            {
                _head = _head.Next;
                _length--;
                return;
            }

            var temp = _head;
            while (temp != null)
            {
                if (temp.Equals(item))
                {
                    RemoveNode(temp);
                    return;
                }

                temp = temp.Next;
            }
        }

        public T RemoveAt(int index)
        {
            ValidateIndex(index);

            ListNode temp = _head;
            if (index == 0)
            {
                _head = null;
                return temp.Value;
            }

            var count = 0;
            while (count < index && temp.Next != null)
            {
                temp = temp.Next;
                count++;
            }

            RemoveNode(temp);
            return temp.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }

        private void RemoveNode(ListNode node)
        {
            node.Previous.Next = node.Next;
            _length--;
        }

        private class ListNode : IEquatable<T>
        {
            public ListNode(T value)
            {
                Value = value;
            }

            public T Value { get; set; }

            public ListNode Next { get; set; }

            public ListNode Previous { get; set; }

            public bool Equals([AllowNull] T other)
            {
                return Value.Equals(other);
            }
        }

        private class Enumerator : IEnumerator<T>
        {
            private readonly ListNode _head;
            private ListNode _current;

            public Enumerator(ListNode head)
            {
                _head = head;
                _current = null;
            }

            public T Current => _current.Value;

            object IEnumerator.Current => _current.Value;

            public void Dispose()
            {
                // There are no resources to dispose
            }

            public bool MoveNext()
            {
                if (_head == null)
                {
                    return false;
                }

                if (_current == null)
                {
                    _current = _head;
                    return true;
                }

                if (_current.Next == null)
                {
                    return false;
                }

                _current = _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = null;
            }
        }
    }
}
