using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly IDoublyLinkedList<T> _linkedList;

        public HybridFlowProcessor()
        {
            _linkedList = new DoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            try
            {
                return _linkedList.RemoveAt(0);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("List is empty.");
            }
        }

        public void Enqueue(T item)
        {
            _linkedList.Add(item);
        }

        public T Pop()
        {
            try
            {
                return _linkedList.ElementAt(0);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }

        public void Push(T item)
        {
            _linkedList.AddAt(0, item);
        }
    }
}
