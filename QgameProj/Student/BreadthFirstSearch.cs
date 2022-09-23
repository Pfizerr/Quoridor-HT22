using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student
{
    public class BFS : PathAlgorithm
    {
        private bool[] marked;
        private int edgeTo;
        private int start;

        public BFS(Graph graph, int start)
        {
            this.start = start;
        }

        public BFS()
        {

        }

        public void Initialize(Graph graph, int start)
        {
            this.start = start;
        }

        public override Stack<int> Search(Graph graph, int start)
        {
            Queue<int> queue = new Queue<int>();
            marked[start] = true;
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                int v = queue.Dequeue();
                for (int w in graph.AdjacentTo(v))
                {
                    if (!marked[w])
                    {
                        edgeTo[w] = v;
                        marked[w] = true;
                        queue.Enqueue(w);<a
                    }
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return marked[v];
        }
        
        public Stack<int> PathTo(int v)
        {
            if (!HasPathTo(v))
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            for (int x = v; x != start; x = edgeTo[x])
            {
                path.Push(x);
                path.Push(start);
                return path;
            }
        }
    }
}