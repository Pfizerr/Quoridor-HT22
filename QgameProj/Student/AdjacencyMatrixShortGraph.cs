using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Student
{
    public class AdjacencyMatrixShortGraph : Graph
    {
        private int N = SpelBräde.N;
        private int?[][] adj;
        //private List<int?>[] adj;

        public AdjacencyMatrixShortGraph(GraphData data) : this(data.Next)
        {
            this.N = N;
            E = data.Next;
            for (int i = 0; i < E; i++)
            {
                int? v = data.Next;
                int? w = data.Next;
                if (v == null || w == null)
                    Debugger.Break();
                AddEdge((int)v, (int)w);
            }
        }

        public AdjacencyMatrixShortGraph(int V)
        {
            this.V = V;
            adj = new int?[V][];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new int?[4] { null, null, null, null };
            }
        }

        public override void AddEdge(int v, int w)
        {
            for (int i = 0; i < 4; i++)
            {
                if (adj[v][i] == w) break;
                if (adj[v][i] == null) //#*????
                {
                    adj[v][i] = w;
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (adj[w][i] == v) break;
                if (adj[w][i] == null)
                {
                    adj[w][i] = v;
                    break;
                }
            }
        }

        public override bool ContainsEdge(int v, int w)
        {
            IEnumerator<int> enumerator1 = AdjacentTo(v);
            while (enumerator1.MoveNext())
                if (enumerator1.Current == w)
                    return true;
            return false;
        }
        
        public override IEnumerator<int> AdjacentTo(int v)
        {
            List<int> e = new List<int>();
            foreach (int? nullable in adj[v])
                if (nullable != null) e.Add(nullable ?? default(int));
            return e.GetEnumerator();
        }

        public void RemoveEdge(int v, int w)
        {
            for (int i = 0; i < 4; i++)
                if (adj[v][i] == w)
                    adj[v][i] = null;

            for (int i = 0; i < 4; i++)
                if (adj[w][i] == v)
                    adj[w][i] = null;
        }

        public void RemoveAllIncidentEdges(int v)
        {
            IEnumerator<int> enumerator = AdjacentTo(v);
            while (enumerator.MoveNext())
                RemoveEdge(v, enumerator.Current);
        }

        public override void Update(SpelBräde bräde)
        {
            bool[,] horizontalVertices = bräde.horisontellaVäggar;
            bool[,] verticalVertices = bräde.vertikalaVäggar;

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
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