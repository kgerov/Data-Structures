namespace _07.ImplementALinkedList
{
    public class ListNode<T>
    {
        public ListNode(T Value)
        {
            this.Value = Value;
        }

        public T Value { get; set; }

        public ListNode<T> NextNode { get; set; }
    }
}
