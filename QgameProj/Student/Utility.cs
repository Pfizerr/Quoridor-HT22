using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Student
{
    public class Utility
    {
        /// <summary>
        /// Conversion from point to one-dimensional index.
        /// </summary>
        public static int ToInt(Point pos, int N) => pos.Y * N + pos.X;

        /// <summary>
        /// Conversion from two-dimensional point to one-dimensional index.
        /// </summary>
        public static int ToInt(int x, int y, int N) => y * N + x;

        /// <summary>
        /// Conversion between the one-dimensional index of a propagated, abstract, two-dimensional grid to point. Assumes identical grid width and height
        /// </summary>
        public static Point ToPoint(int index, int N) => new Point(index % N, index / N);

        /// <summary>
        /// Offset along horizontal plane (convert from two-dimensional to one-dimensional index)
        /// </summary>
        public static int OffsetX(int x, int y, int offset, int N) => ToInt(x, y, N) + offset;

        /// <summary>
        /// Offset along vertical plane (convert from two-dimensional to one-dimensional index)
        /// </summary>
        public static int OffsetY(int x, int y, int offset, int N) => (y + offset) * N + x;

        /// <summary>
        /// This methods only purpose is to minimize the effect debug log code has on the readability of other code.
        /// </summary>
        public static void TryLog(bool isLogging, String message)
        {
            if (isLogging) Debug.WriteLine(message);
        }
    }
}