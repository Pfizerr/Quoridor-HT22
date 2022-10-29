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
            bool[,] horizontalWalls = bräde.horisontellaVäggar; // Kan bara fylla 8 utav 9 platser på en rad/kolumn med väggar. (?)
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


namespace test
{
    public class Node
    {
        private static int N = SpelBräde.N;
        private List<Node> adjacentNodes;
        private Point location;

        public Point Location
        {
            get
            {
                return location;
            }
            private set
            {
                location = value;
            }
        }

        public int Index
        {
            get 
            { 
                return Location.Y * N + Location.X; 
            }
            private set 
            { 
                location = new Point(value % N, (value - value % N) / N);
            }
        }

        public Node(Point location)
        {
            this.location = location;
            adjacentNodes = new List<Node>();
        }

        public void AddEdge(Node other)
        {
            adjacentNodes.Add(other);
        }

        public void RemoveEdge(Node other)
        {
            adjacentNodes.Remove(other);
        }

        public void RemoveAllEdges()
        {
            for (int i = adjacentNodes.Count - 1; i >= 0; i--)
            {
                Node node = adjacentNodes[i];
                node.RemoveEdge(this);
                adjacentNodes.RemoveAt(i);
            }
        }

        public IEnumerator<Node> AdjacentNodes()
        {
            return adjacentNodes.GetEnumerator();
        }

        public bool AdjacentTo(Node other)
        {
            bool result = false;

            foreach(Node node in adjacentNodes)
                if (node == other)
                    result = true;

            return result;
        }
    }

    public class Graph
    {
        int N;//

        private Node[] nodes;

        public Graph(SpelBräde bräde)
        {
            nodes = new Node[N * N];
            Build(bräde);
        }

        public void Build(SpelBräde bräde)
        {
            bool[,] horizontalWalls = bräde.horisontellaVäggar; // Kan bara fylla 8 utav 9 platser på en rad/kolumn med väggar. (?)
            bool[,] verticalWalls = bräde.vertikalaVäggar;
            int N = SpelBräde.N;

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    int center = y * N + x;

                    if (x < horizontalWalls.GetLength(1) && horizontalWalls[x, y])
                    {
                        if (ContainsEdge(center, center + 1))
                            RemoveEdge(center, center + 1);
                    }
                    else
                    {
                        if (!ContainsEdge(center, center + 1))
                            AddEdge(center, center + 1);
                    }

                    if (y < verticalWalls.GetLength(0) && verticalWalls[x, y])
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

        public void AddEdge(int v, int w)
        {
            nodes[v].AddEdge(nodes[w]);
            nodes[w].AddEdge(nodes[v]);
        }

        public void RemoveEdge(int v, int w)
        {
            nodes[v].RemoveEdge(nodes[w]);
            nodes[w].RemoveEdge(nodes[v]);
        }

        public void RemoveAllEdges(int v)
        {
            IEnumerator<Node> enumerator = AdjacentTo(v);
            while (enumerator.MoveNext())
                RemoveEdge(v, enumerator.Current.Index);
        }

        public bool ContainsEdge(int v, int w)
        {
            return nodes[v].AdjacentTo(nodes[w]);
        }

        public IEnumerator<Node> AdjacentTo(int v)
        {
            return nodes[v].AdjacentNodes();
        }
    }
}