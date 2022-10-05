using System.Collections.Generic;

namespace Student
{
    public interface PathAlgorithm
    {
        void Search(Graph graph, int start);
        bool HasPathTo(int end);
        Path PathTo(int end);
        Path PathToRow(int row, int N);
    }
}