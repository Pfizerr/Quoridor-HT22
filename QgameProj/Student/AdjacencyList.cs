using System.Collections.Generic;
using System;

namespace Student
{
    
    public class AdjacencyList : Graph
    {
        private List<int>[] adjacencyList;

        public AdjacencyList(int V)
        {
            this.V = V;
            E = 0;
            adjacencyList = new List<int>[V];

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new List<int>();
            }
        }

        public AdjacencyList(GraphData data) : this(data.Next)
        {
            E = data.Next;
            for (int i = 0; i < E; i++)
            {
                AddEdge(data.Next, data.Next);
            }
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public void RemoveEdge(int v, int w)
        {
            adjacencyList[v].Remove(w);
            adjacencyList[w].Remove(v);
        }
        
        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public void RemoveAllIncidentEdges(int v)
        {
            IEnumerator<int> enumerator = AdjacentTo(v);
            while (enumerator.MoveNext())
                RemoveEdge(v, enumerator.Current);
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public override void AddEdge(int v, int w)
        {
            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public override bool ContainsEdge(int v, int w)
        {
            return adjacencyList[v].Contains(w) ? true : false;
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public override IEnumerator<int> AdjacentTo(int v)
        {
            return adjacencyList[v].GetEnumerator();
        }

        /// <summary>
        /// time-complexity: O() ~
        /// best achievable complexity: O(N^2).
        /// </summary>
        public override void Update(SpelBräde bräde)
        {
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
        }
    }
}