using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private int _length = 0;
        private ListNode _head;
        private ListNode _tail;

        public int Length => _length;

        public void Add(T e)
        {
            AddAt(_length, e);
        }

        public void AddAt(int index, T e)
        {
            var newNode = new ListNode(e);
            _length++;

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
                return;
            }

            if (index == 0)
            {
                InsertBefore(_head, newNode);
                _head = newNode;
                return;
            }

            if (index == _length - 1)
            {
                InsertAfter(_tail, newNode);
                _tail = newNode;
                return;
            }

            var temp = GetNodeAt(index);
            InsertBefore(temp, newNode);
        }

        public T ElementAt(int index)
        {
            ValidateIndex(index);

            var temp = GetNodeAt(index);
            return temp.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
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

            var temp = _head;
            if (index == 0)
            {
                _head = null;
                return temp.Value;
            }

            temp = GetNodeAt(index);
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
            node.Next.Previous = node.Previous;
            _length--;
        }

        private ListNode GetNodeAt(int index)
        {
            var count = 0;
            var temp = _head;
            while (count < index && temp.Next != null)
            {
                temp = temp.Next;
                count++;
            }

            return temp;
        }

        private static void InsertBefore(ListNode before, ListNode value)
        {
            value.Next = before;
            if (before.Previous != null)
            {
                before.Previous.Next = value;
            }

            before.Previous = value;
        }

        private static void InsertAfter(ListNode after, ListNode value)
        {
            value.Previous = after;
            if (after.Next != null)
            {
                after.Next.Previous = value;
            }

            after.Next = value;
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
            private readonly int _initialLength;
            private readonly DoublyLinkedList<T> _list;
            private ListNode _current;

            public Enumerator(DoublyLinkedList<T> list)
            {
                _list = list;
                _initialLength = list.Length;
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
                if (_list == null)
                {
                    return false;
                }

                if (_initialLength != _list.Length)
                {
                    throw new InvalidOperationException("Cannot modify the list during the enumeration.");
                }

                if (_current == null)
                {
                    _current = _list._head;
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
