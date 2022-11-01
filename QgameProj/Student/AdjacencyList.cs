using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Student
{

    public class AdjacencyList : Graph
    {
        private List<int>[] adjacencyList;
        private bool init;
        
        public AdjacencyList(SpelBräde bräde) : base(SpelBräde.N)
        {
            init = true;
            adjacencyList = new List<int>[V]; 
            
            for (int i = 0; i < V; i++)
            {
                adjacencyList[i] = new List<int>();
            }


            Build(bräde); 
        }
        
        
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
        

        public void RemoveEdge(int v, int w)
        {
            E--;
            adjacencyList[v].Remove(w);
            adjacencyList[w].Remove(v);

        }

        
        public override void AddEdge(int v, int w)
        {
            E++;
            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);
        }

        
        public override bool ContainsEdge(int v, int w)
        {
            return adjacencyList[v].Contains(w) ? true : false;
        }

        
        public override IEnumerator<int> AdjacentTo(int v)
        {
            return adjacencyList[v].GetEnumerator();
        }
    }
}