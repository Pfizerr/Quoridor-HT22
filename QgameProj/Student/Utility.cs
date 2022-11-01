using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Student
{
    public class Utility
    {
        public static int N = SpelBräde.N;

        public static int ToInt(Point pos) => pos.Y * N + pos.X;

        public static Point ToPoint(int index) => new Point(index % N, index / N);
    }
}