using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Student
{
    
    public class AdjacencyList : Graph
    {
        private List<int>[] adjacencyList; // Space: O(E+V) // E + V

        public AdjacencyList(SpelBräde bräde) : base(SpelBräde.N)
        {
            Build(bräde);
        }

        // O(N^2)
        public override void Build(SpelBräde bräde)
        {
            bool[,] horizontalWalls = bräde.horisontellaVäggar;
            bool[,] verticalWalls = bräde.vertikalaVäggar;
            int N = SpelBräde.N;

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    int center = y * N + x;

                    if (horizontalWalls[x, y])
                    {
                        if (ContainsEdge(center, center + 1))
                            RemoveEdge(center, center + 1);
                    }
                    else
                    {
                        if (!ContainsEdge(center, center + 1))
                            AddEdge(center, center + 1);
                    }

                    if (verticalWalls[x, y])
                    {
                        if (ContainsEdge(center, center + N))
                            RemoveEdge(center, center + N);
                    }
                    else
                    {
                        if (!ContainsEdge(center, center + N))
                            AddEdge(center, center + N);
                    }
                }
            }
        }

        // O(1)
        public void RemoveEdge(int v, int w)
        {
            E--;
            adjacencyList[v].Remove(w);
            adjacencyList[w].Remove(v);

        }

        // deg(v)
        public void RemoveAllIncidentEdges(int v)
        {
            IEnumerator<int> enumerator = AdjacentTo(v);
            while (enumerator.MoveNext())
                RemoveEdge(v, enumerator.Current);

        }


        // O(1)
        public override void AddEdge(int v, int w)
        {
            E++;
            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);
        }


        // O(1)
        public override bool ContainsEdge(int v, int w)
        {
            return adjacencyList[v].Contains(w) ? true : false;
        }


        // O(1), enumeration is deg(v)
        public override IEnumerator<int> AdjacentTo(int v)
        {
            return adjacencyList[v].GetEnumerator();
        }
    }
}