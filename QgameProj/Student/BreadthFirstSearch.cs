using System.Collections.Generic;

namespace Student
{
    public class BreadthFirstSearch
    {
        private Graph graph;
        private bool[] marked;
        private int[] edgeTo;
        private int start;


        public BreadthFirstSearch(Graph graph, int start) 
        {
            this.graph = graph;
            this.start = start;
            marked = new bool[graph.V];
            edgeTo = new int[graph.V];
        }

        public void Search(Graph graph, int start)
        {
            Queue<int> queue = new Queue<int>();
            marked[start] = true;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                int vertex = queue.Dequeue();
                IEnumerator<int> enumerator = graph.AdjacentTo(vertex);
                while (enumerator.MoveNext())
                {
                    int adjacentVertex = enumerator.Current;
                    if (!marked[adjacentVertex])
                    {
                        edgeTo[adjacentVertex] = vertex;
                        marked[adjacentVertex] = true;
                        queue.Enqueue(adjacentVertex);
                    }
                }
            }
        }

        public bool HasPathTo(int end)
        {

            return marked[end];
        }

        public Path PathTo(int end)
        {
            if (!HasPathTo(end))
            {
                return null;
            }

            Path path = new Path();

            for (int x = end; x != start; x = edgeTo[x])
            {
                path.Push(Utility.ToPoint(x));
            }

            return path;
        }
        
        public Path PathToRow(int row, int N)
        {
            Path path = new Path();
            int first = row * N;

            if (HasPathTo(first)) 
            {
                path = PathTo(first);
            }

            for (int i = 1; i < N; i++)
            {
                int t = row * N + i;

                if (!HasPathTo(t))
                {
                    continue;
                }

                Path tPath = PathTo(t);

                if (tPath.Size < path.Size)
                {
                    path = tPath;
                }
            }


            return path;
        }
    }
}