namespace BinarySearchTreeLab9.Models
{
    public sealed class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node(int value)
        {
            Value = value;
        }
    }
}