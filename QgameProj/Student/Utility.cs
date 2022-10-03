using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Student
{
    public class Utility
    {

        public static int N = SpelBräde.N;
        /// <summary>
        /// Conversion from point to one-dimensional index.
        /// </summary>
        public static int ToInt(Point pos) => pos.Y * N + pos.X;

        /// <summary>
        /// Conversion from two-dimensional point to one-dimensional index.
        /// </summary>
        public static int ToInt(int x, int y) => y * N + x;

        /// <summary>
        /// Conversion between the one-dimensional index of a propagated, abstract, two-dimensional grid to point. Assumes identical grid width and height
        /// </summary>
        public static Point ToPoint(int index) => new Point(index % N, index / N);

        /// <summary>
        /// Offset along horizontal plane (convert from two-dimensional to one-dimensional index)
        /// </summary>
        public static int OffsetX(int x, int y, int offset) => ToInt(x, y) + offset;

        public static int OffsetX(Point xy, int offset) => ToInt(xy.X + offset, xy.Y);

        /// <summary>
        /// Offset along vertical plane (convert from two-dimensional to one-dimensional index)
        /// </summary>
        public static int OffsetY(int x, int y, int offset) => (y + offset) * N + x;

        public static int OffsetY(Point xy, int offset) => ToInt(xy.X, xy.Y + offset);

        /// <summary>
        /// This methods only purpose is to minimize the effect debug log code has on the readability of other code.
        /// </summary>
        public static void TryLog(bool isLogging, String message)
        {
            if (isLogging) Debug.WriteLine(message);
        }
    }
}