using System.Collections.Generic;

namespace Student
{
    public interface PathAlgorithm
    {
        void Search(Graph graph, int start);
        bool HasPathTo(int end);
        Stack<int> PathTo(int end);
        Stack<int> PathToRow(int row, int N);
    }
}