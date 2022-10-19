using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Student
{
    
    public class AdjacencyList : Graph
    {
        private List<int>[] adjacencyList; // Space: O(E+V) // E + V

        public AdjacencyList(int V)
        {
            #region Time-Complexity: O() ~
            this.V = V;
            E = 0;
            adjacencyList = new List<int>[V];

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new List<int>();
            }
            #endregion
        }

        public AdjacencyList(GraphData data) : this(data.Next)
        {
            #region Time-Complexity: O() ~
            E = data.Next;
            for (int i = 0; i < E; i++)
            {
                AddEdge(data.Next, data.Next);
            }
            #endregion
        }

        public AdjacencyList(SpelBräde bräde)
        {
            V = N * N;
            E = 0;
            Build(bräde);
            #region 1
            /*
            int center = Utility.ToInt(x, y);
            if (x > 0)
            {
                int left = center - 1;
                adjacencyList[center].Add(left);
            }
            if (y > 0)
            {
                int down = center - N;
                adjacencyList[center].Add(down);
            }
            if (x < N)
            {
                int right = center + 1;
                adjacencyList[center].Add(right);
            }
            if (y < N)
            {
                int up = center + N;
                adjacencyList[center].Add(up);
            }
            */
            #endregion 
            #region 2
            /*
            if (ContainsEdge(x, y))
            {
                if (hasHorizontalWall)
                {
                    int v = y * N + x;
                    RemoveEdge(v, v + 1);
                }
                if (hasVerticalWall)
                {
                    int v = y * N + x;
                    RemoveEdge(v, v + N);
                }
            }
            else if (!hasHorizontalWall && !hasVerticalWall)
            {
                AddEdge(x, y);
            }
            */
            #endregion 
            #region c
            // ONLY CHECK FOR TOP AND RIGHT, LEFT AND BOTTOM HAVE ALREADY BEEN CHECKED. 
            // !hw == false: addEdge(v, v+N)
            // !vw: addEdge(v+1, v)
            // hw && !containsEdge(v, v+N): removeEdge(v, v+N)
            // vw && !containsEdge(v+1, v): removeEdge(v+1, v)
            #endregion
        }

        public void Build(SpelBräde bräde)
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

        public void RemoveEdge(int v, int w)
        {
            #region Time-Complexity: O() ~

            E--;
            adjacencyList[v].Remove(w);
            adjacencyList[w].Remove(v);

            #endregion
        }

        public void RemoveAllIncidentEdges(int v)
        {
            #region Time-Complexity: O() ~

            IEnumerator<int> enumerator = AdjacentTo(v);
            while (enumerator.MoveNext())
                RemoveEdge(v, enumerator.Current);

            #endregion
        }

        public override void AddEdge(int v, int w)
        {
            #region Time-Complexity: O(1)

            E++;
            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);

            #endregion
        }

        public override bool ContainsEdge(int v, int w)
        {
            #region Time-Complexity: O(1) 
            return adjacencyList[v].Contains(w) ? true : false;
            #endregion
        }

        public override IEnumerator<int> AdjacentTo(int v)
        {
            #region Time-Complexity: O(deg(v)) //applies to the actual enumeration of returned object

            return adjacencyList[v].GetEnumerator();

            #endregion
        }

        public override void Update(SpelBräde bräde)
        {
            // Best Achievable Time-Complexity: O(N^2)
            #region Time-Complexity: O() ~

            bool[,] horizontalVertices = bräde.horisontellaVäggar;
            bool[,] verticalVertices = bräde.vertikalaVäggar;

            for (int y = 0; y < N; y++) 
            { // O(N)
                for (int x = 0; x < N; x++)
                { // O(N*N)
                    bool h = (y > horizontalVertices.GetLength(1) - 1) ? false : horizontalVertices[x, y];
                    bool v = (x > verticalVertices.GetLength(0) - 1) ? false : verticalVertices[x, y];
                    int center = Utility.ToInt(x, y);
                    int right = Utility.OffsetX(x, y, 1);
                    int top = Utility.OffsetY(x, y, 1);
                    IEnumerator<int> enumerator = AdjacentTo(center);
                    if (h || v)
                    {
                        while (enumerator.MoveNext())
                        {
                            if (h && right == enumerator.Current)
                            {
                                RemoveAllIncidentEdges(enumerator.Current);
                            }
                            else if (v && top == enumerator.Current)
                            {
                                RemoveAllIncidentEdges(enumerator.Current);
                            }
                            else
                            {
                                RemoveEdge(center, enumerator.Current);
                            }
                        }
                    }
                }
            }

            #endregion
        }
    }
}