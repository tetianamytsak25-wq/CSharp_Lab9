using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeLab9.Models
{
    public class BinarySearchTree : IEnumerable<int>
    {
        private Node? root;
        public Node? Root
        {
            get { return root; }
        }
        public int Count { get; private set; }
        public int Height
        {
            get { return GetHeight(root); }
        }
        public int? Min
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                Node current = root;

                while (current.Left != null)
                {
                    current = current.Left;
                }
                return current.Value;
            }
        }
        public int? Max
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                Node current = root;

                while (current.Right != null)
                {
                    current = current.Right;
                }
                return current.Value;
            }
        }
        public bool Add(int value)
        {
            if (root == null)
            {
                root = new Node(value);
                Count++;
                return true;
            }
            Node? current = root;

            while (current != null)
            {
                if (value == current.Value)
                {
                    return false;
                }

                if (value < current.Value)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node(value);
                        Count++;
                        return true;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node(value);
                        Count++;
                        return true;
                    }
                    current = current.Right;
                }
            }
            return false;
        }
        public bool Contains(int value, out List<int> visited)
        {
            visited = new List<int>();
            Node? current = root;

            while (current != null)
            {
                visited.Add(current.Value);

                if (value == current.Value)
                {
                    return true;
                }

                if (value < current.Value)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
            return false;
        }
        public bool Delete(int value)
        {
            bool removed;
            root = DeleteNode(root, value, out removed);

            if (removed)
            {
                Count--;
            }
            return removed;
        }
        private Node? DeleteNode(Node? node, int value, out bool removed)
        {
            if (node == null)
            {
                removed = false;
                return null;
            }

            if (value < node.Value)
            {
                node.Left = DeleteNode(node.Left, value, out removed);
            }
            else if (value > node.Value)
            {
                node.Right = DeleteNode(node.Right, value, out removed);
            }
            else
            {
                removed = true;

                if (node.Left == null)
                {
                    return node.Right;
                }

                if (node.Right == null)
                {
                    return node.Left;
                }
                Node successor = node.Right;

                while (successor.Left != null)
                {
                    successor = successor.Left;
                }
                node.Value = successor.Value;
                node.Right = DeleteNode(node.Right, successor.Value, out _);
            }
            return node;
        }
        public void Clear()
        {
            root = null;
            Count = 0;
        }
        public List<int> Preorder()
        {
            List<int> result = new List<int>();
            Preorder(root, result);
            return result;
        }
        public List<int> Inorder()
        {
            List<int> result = new List<int>();
            Inorder(root, result);
            return result;
        }
        public List<int> Postorder()
        {
            List<int> result = new List<int>();
            Postorder(root, result);
            return result;
        }
        private void Preorder(Node? node, List<int> result)
        {
            if (node == null)
            {
                return;
            }
            result.Add(node.Value);
            Preorder(node.Left, result);
            Preorder(node.Right, result);
        }
        private void Inorder(Node? node, List<int> result)
        {
            if (node == null)
            {
                return;
            }
            Inorder(node.Left, result);
            result.Add(node.Value);
            Inorder(node.Right, result);
        }
        private void Postorder(Node? node, List<int> result)
        {
            if (node == null)
            {
                return;
            }
            Postorder(node.Left, result);
            Postorder(node.Right, result);
            result.Add(node.Value);
        }
        private int GetHeight(Node? node)
        {
            if (node == null)
            {
                return 0;
            }
            int leftHeight = GetHeight(node.Left);
            int rightHeight = GetHeight(node.Right);

            return 1 + Math.Max(leftHeight, rightHeight);
        }
        public IEnumerator<int> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}