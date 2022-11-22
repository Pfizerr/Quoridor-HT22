using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
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

        /// <summary>
        /// O(1), 
        /// </summary>
        /// <param name="item"></param>
        public void Push(Point item)
        {
        // O(1):        Node old = first;
        // O(1):        first = new Node();
        // O(1):        first.Item = item;
        // O(1):        first.Next = old;
        // O(1):        Size++;
        }

        /// <summary>
        /// O(1), 
        /// </summary>
        public Point Pop()
        {
        // O(1):        Point item = first.Item;
        // O(1):        first = first.Next;
        // O(1):        Size--;
            return item;
        } public static Node node = new Node();

        /// <summary>
        /// Linear time proportional to the length of the path, BUT ---- to N
        /// </summary>
        public Point Peek(int depth)
        {
        // O(1):        if (depth > Size)
        //              {
        // DEBUG CONF.      System.Diagnostics.Debugger.Break();
        // DEBUG CONF.      return Point.Zero;
        //              }
        //          
        // O(1):        Node node = first;
        //          
        // O(X):        for (int i = 0; i < depth; i++)
        //              {
        // O(1):            node = node.Next;
        //              }
        //     
                return node.Item;
        }
    }
}