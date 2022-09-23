using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Student
{
    public class GraphData
    {
        private Stack<int> data;

        public GraphData(SpelBräde bräde, int N)
        {
            var hNodes = bräde.horisontellaVäggar;
            data = new Stack<int>();
            var vNodes = bräde.vertikalaVäggar;
            int V = N * N;
            int E = 0;
            for (int y = 0; y < vNodes.GetLength(0); y++)
            {
                for (int x = 0; x < hNodes.GetLength(0); x++)
                {
                    int center = Utility.ToInt(x, y, N);

                    if (x < hNodes.GetLength(1))
                    {
                        data.Push(center);
                        data.Push(Utility.OffsetX(x, y, 1, N));
                        if (Agent.DEBUG) Debug.WriteLine(String.Format($"new edge between v and w: (v: [{x}, {y}] {y * N + x}, w: [{x + 1}, {y}] {x * N + x + 1} \n"));
                        E++;
                    }

                    if (x > 0)
                    {
                        data.Push(center);
                        data.Push(Utility.OffsetX(x, y, 1, N));
                        if (Agent.DEBUG) Debug.WriteLine(String.Format($"new edge between v and w: (v: [{x}, {y}] {y * N + x}, w: [{x - 1}, {y}] {y * N + x - 1}\n"));
                        E++;
                    }

                    if (y < vNodes.GetLength(0))
                    {
                        data.Push(center);
                        data.Push(Utility.OffsetY(x, y, 1, N));
                        if (Agent.DEBUG) Debug.WriteLine(String.Format($"new edge between v and w: (v: [{x}, {y}] {y * N + x}, w: [{x}, {y + 1}] {((y + 1) * N + x)}\n"));
                        E++;
                    }

                    if (y > 0)
                    {
                        data.Push(center);
                        data.Push(Utility.OffsetY(x, y, -1, N));

                        if (Agent.DEBUG)
                            Debug.WriteLine(String.Format($"new edge between v and w: (v: [{x}, {y}] {y * N + x}, w: [{x}, {y - 1}] {((y - 1) * N + x)}\n"));

                        E++;
                    }
                }
            }

            data.Push(E);
            data.Push(V);
        }

        public int Next
        {
            get { return data.Pop(); }
        }
    }
}