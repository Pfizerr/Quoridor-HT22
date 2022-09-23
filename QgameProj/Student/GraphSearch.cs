using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student
{
    public interface PathAlgorithm 
    {
        Stack<int> Find(Graph graph, int start, int end);
    }

    public class BFS : PathAlgorithm
    {
        private bool[] marked;
        private int edgeTo;
        private int start; 

        public BFS(Graph graph, int start)
        {
        }

        public override Stack<int> Find()
        {

        }
        
    }
}