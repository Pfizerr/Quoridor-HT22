using System;
using System.Collections.Generic;

namespace Student
{
    public abstract class PathAlgorithm : IDisposable
    {

        public virtual Path PredictForAlteredGraph(BreadthFirstSearch instance, AdjacencyList graph, int start, int end, int root, int ext, out bool hasPath)
        {
            graph.RemoveEdge(root, ext);
            instance.Search(graph, start);
            hasPath = instance.HasPathTo(end);
            Path path = instance.PathTo(end);
            graph.AddEdge(root, ext);
            return path;
        }

        public virtual void Dispose()
        {
        }

        public abstract void Search(Graph graph, int start);

        public abstract bool HasPathTo(int end);

        public abstract Path PathTo(int end);

        public abstract Path PathToRow(int row, int N);
    }
}