using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using BinarySearchTreeLab9.Models;

namespace BinarySearchTreeLab9.Controls
{
    public class TreeDrawingControl : FrameworkElement
    {
        private const double LevelHeight = 70;
        private const double NodeRadius = 19;
        public Node? Root { get; set; }
        public TreeDrawingControl()
        {
            MinHeight = 220;
            MinWidth = 500;
        }
        public void RefreshTree()
        {
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawRectangle(new SolidColorBrush(Color.FromRgb(43, 54, 69)), null, new Rect(0, 0, ActualWidth, ActualHeight));

            if (Root == null)
            {
                FormattedText emptyText = new FormattedText("Дерево порожнє", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 16, Brushes.Gray, VisualTreeHelper.GetDpi(this).PixelsPerDip);

                dc.DrawText(emptyText, new Point(20, 20));
                return;
            }
            Dictionary<Node, Point> positions = new Dictionary<Node, Point>();

            int index = 0;
            CalculatePositions(Root, 0, ref index, positions);

            double horizontalOffset = 20;
            double verticalOffset = 35;

            foreach (KeyValuePair<Node, Point> item in positions)
            {
                Node node = item.Key;
                Point point = item.Value;

                if (node.Left != null && positions.TryGetValue(node.Left, out Point leftPoint))
                {
                    DrawConnection(dc, point, leftPoint, horizontalOffset, verticalOffset);
                }

                if (node.Right != null && positions.TryGetValue(node.Right, out Point rightPoint))
                {
                    DrawConnection(dc, point, rightPoint, horizontalOffset, verticalOffset);
                }
            }

            foreach (KeyValuePair<Node, Point> item in positions)
            {
                Point point = item.Value;

                double x = point.X + horizontalOffset;
                double y = point.Y + verticalOffset;

                dc.DrawEllipse(new SolidColorBrush(Color.FromRgb(56, 184, 120)), new Pen(new SolidColorBrush(Color.FromRgb(34, 143, 91)), 1.5), new Point(x, y), NodeRadius, NodeRadius);
                FormattedText text = new FormattedText(item.Key.Value.ToString(), System.Globalization.CultureInfo.CurrentCulture,FlowDirection.LeftToRight, new Typeface("Segoe UI"), 12, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                dc.DrawText(text, new Point(x - text.Width / 2, y - text.Height / 2));
            }
        }
        private void CalculatePositions(Node? node, int depth, ref int index, Dictionary<Node, Point> positions)
        {
            if (node == null)
            {
                return;
            }
            CalculatePositions(node.Left, depth + 1, ref index, positions);

            double horizontalSpacing = Math.Max(55, ActualWidth / (RootCount() + 1));
            double x = 25 + index * horizontalSpacing;
            double y = 25 + depth * LevelHeight;

            positions[node] = new Point(x, y);
            index++;

            CalculatePositions(node.Right, depth + 1, ref index, positions);
        }
        private int RootCount()
        {
            return CountNodes(Root);
        }
        private int CountNodes(Node? node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }
        private void DrawConnection(DrawingContext dc, Point parent, Point child, double horizontalOffset, double verticalOffset)
        {
            Pen pen = new Pen(new SolidColorBrush(Color.FromRgb(94, 112, 132)), 1.5);

            dc.DrawLine(pen, new Point(parent.X + horizontalOffset, parent.Y + verticalOffset), new Point(child.X + horizontalOffset, child.Y + verticalOffset));
        }
    }
}