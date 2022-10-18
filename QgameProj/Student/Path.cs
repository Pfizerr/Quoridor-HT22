using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    // space is always proportional to the size of the collection.
    // the time per operation is always independent of the size of the collection.

    public class Path
    {
        private Node first;

        private class Node
        {
            public Point Item
            {
                get;
                set;
            }

            public Node Next
            {
                get;
                set;
            }
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public int Size
        {
            get;
            private set;
        }

        public void Push(Point item)
        {
            Node old = first;
            first = new Node();
            first.Item = item;
            first.Next = old;
            Size++;
        }

        public Point Pop()
        {
            Point item = first.Item;
            first = first.Next;
            Size--;
            return item;
        }

        public Point Peek(int depth)
        {
            if (depth > Size)
            {
                System.Diagnostics.Debugger.Break();
                return Point.Zero;
            }

            Node node = first;

            #region Complexity
            for (int i = 0; i < depth; i++)
            {
                node = node.Next;
            }
            #endregion

            return node.Item;
        }
    }
}