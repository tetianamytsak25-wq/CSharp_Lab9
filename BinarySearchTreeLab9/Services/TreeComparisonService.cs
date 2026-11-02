using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTreeLab9.Models;

namespace BinarySearchTreeLab9.Services
{
    public class TreeComparisonService
    {
        public (int RandomHeight, int SortedHeight) CompareHeights(int amount)
        {
            List<int> values = Enumerable.Range(1, amount).ToList();
            BinarySearchTree randomTree = new BinarySearchTree();
            BinarySearchTree sortedTree = new BinarySearchTree();
            Random random = new Random();

            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                int temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }

            foreach (int value in values)
            {
                randomTree.Add(value);
            }

            foreach (int value in Enumerable.Range(1, amount))
            {
                sortedTree.Add(value);
            }
            return (randomTree.Height, sortedTree.Height);
        }
    }
}