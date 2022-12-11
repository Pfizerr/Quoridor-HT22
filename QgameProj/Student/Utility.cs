using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Student
{
    public class Utility
    {
        public static int N = SpelBräde.N;

        /// <summary>
        /// O(1)
        /// </summary>
        public static int ToInt(Point pos) => pos.Y * N + pos.X;

        /// <summary>
        /// O(1)
        /// </summary>
        public static Point ToPoint(int index) => new Point(index % N, index / N);
    }
}