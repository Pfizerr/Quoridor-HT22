using System.Collections.Generic;

namespace Student
{
    public interface PathAlgorithm
    {
        Stack<int> Find(Graph graph, int start, int end);
    }
}