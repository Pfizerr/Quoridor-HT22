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
        private int size;

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

        public int Size()
        {
            return size;
        }

        public void Push(int item)
        {
            Node old = first;
            first = new Node();
            first.Item = item;
            first.Next = old;
            size++;
        }

        public Point Pop()
        {
            int item = first.Item;
            first = first.Next;
            size--;
            return item;
        }

        public Point Peek(int depth)
        {
            if (depth > Size())
            {
                System.Diagnostics.Debugger.Break();
                return 0;
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