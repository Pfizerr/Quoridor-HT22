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
            int N = 0;

            bool[,] horizontalWalls = bräde.horisontellaVäggar; // O(N^2)
            bool[,] verticalWalls = bräde.vertikalaVäggar; // O(N^2)
            // O(1): int N = SpelBräde.N;
            
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    // O(1)
                }
            }

            // O(1): init = false;
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