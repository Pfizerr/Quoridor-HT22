using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        public void RemoveEdge(Point v, Point w)
        {
            int _v = Utility.ToInt(v);
            int _w = Utility.ToInt(w);

            adjacencyList[_v].Remove(_w);
            adjacencyList[_w].Remove(_v);
        }
        
        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public void RemoveAllIncidentEdges(Point v)
        {
            int _v = Utility.ToInt(v);

            IEnumerator<int> enumerator = AdjacentTo(_v);
            while (enumerator.MoveNext())
                RemoveEdge(_v, enumerator.Current);
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public override void AddEdge(Point v, Point w)
        {
            int _v = Utility.ToInt(v);
            int _w = Utility.ToInt(w);

            adjacencyList[_v].Add(_w);
            adjacencyList[_w].Add(_v);
        }

        public override bool ContainsEdge(Point v, Point w)
        {
            int _v = Utility.ToInt(v);
            int _w = Utility.ToInt(w);

            return adjacencyList[_v].Contains(_w) ? true : false;
        }

        /// <summary>
        /// time-complexity: O() ~
        /// </summary>
        public override IEnumerator<int> AdjacentTo(Point v)
        {
            int _v = Utility.ToInt(v);
            return adjacencyList[_v].GetEnumerator();
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