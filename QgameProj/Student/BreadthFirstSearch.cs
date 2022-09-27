using System.Collections.Generic;

namespace Student
{
    public class BreadthFirstSearch : PathAlgorithm
    {
        private Graph graph;
        private bool[] marked;
        private int[] edgeTo;
        private int start;


        public BreadthFirstSearch()
        {
            
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

        public Stack<int> PathTo(int end)
        {
            if (!HasPathTo(end))
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            for (int x = end; x != start; x = edgeTo[x])
            {
                path.Push(x);
                path.Push(start);
            }
            return path;
        }
    }
}