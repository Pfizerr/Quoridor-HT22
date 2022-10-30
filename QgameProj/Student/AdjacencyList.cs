using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Student
{

    public class AdjacencyList : Graph
    {
        private List<int>[] adjacencyList; // Space: E+V
        private bool init;

        // ~ 2N^2, O(N^2)
        public AdjacencyList(SpelBräde bräde) : base(SpelBräde.N)
        {
            init = true;
            // < O(N^2)
            adjacencyList = new List<int>[V]; 
            
            for (int i = 0; i < V; i++)
            {
                adjacencyList[i] = new List<int>();
            }
            // > O(N^2)


            Build(bräde); // O(N^2)
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

                    if (y < horizontalWalls.GetLength(1) - 1 && horizontalWalls[x, y])
                    {
                        if (ContainsEdge(center, center + N))
                            RemoveEdge(center, center + N);
                    }
                    else
                    {
                        if ((center + 1) % N != 0 && init)
                            AddEdge(center, center + 1);
                    }

                    if (x < verticalWalls.GetLength(0) - 1 && verticalWalls[x, y])
                    {
                        if (ContainsEdge(center, center + 1))
                            RemoveEdge(center, center + 1);
                    }
                    else
                    {
                        if ((center + N < N * N) && init)
                            AddEdge(center, center + N);
                    }
                }
            }

            init = false;
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