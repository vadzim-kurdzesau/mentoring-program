namespace UnitTestingTasks
{
    public class RecentlyUsedList
    {
        private readonly int _capacity;
        private int _length;
        private ListNode? _head;

        public int Length => _length;

        public string this[int index]
        {
            get
            {
                ValidateIndex(index);

                var count = 0;
                var temp = _head;
                while (count < index)
                {
                    temp = temp.Next;
                    count++;
                }

                return temp.Value;
            }
        }

        public RecentlyUsedList()
            : this(int.MaxValue)
        {
        }

        public RecentlyUsedList(int capacity)
        {
            _capacity = capacity;
        }

        public void Add(string value)
        {
            ValidateString(value);
            var newValue = new ListNode { Value = value };

            var temp = _head;
            while (temp != null)
            {
                if (temp.Value.Equals(value))
                {
                    RemoveNode(temp);
                    break;
                }

                temp = temp.Next;
            }

            if (_length == _capacity)
            {
                RemoveLastNode();
            }

            _length++;
            if (_head == null)
            {
                _head = newValue;
                return;
            }

            newValue.Next = _head;
            _head.Previous = newValue;
            _head = newValue;
        }

        private void RemoveLastNode()
        {
            var temp = _head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            RemoveNode(temp);
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
            _length--;
            if (node.Previous == null)
                return;

            node.Previous.Next = node.Next;

            if (node.Next == null)
                return;

            node.Next.Previous = node.Previous;
        }

        private static void ValidateString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        private class ListNode
        {
            public string Value { get; set; }

            public ListNode Next { get; set; }

            public ListNode Previous { get; set; }
        }
    }
}
