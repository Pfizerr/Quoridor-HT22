using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student
{
    public class ShortestPathFinder
    {
        private PathAlgorithm pathAlgorithm;

        public ShortestPathFinder(PathAlgorithm pathAlgorithm)
        {
            this.pathAlgorithm = new BreadthFirstSearch();
        }

        public Stack<int> FindVectorToRow(int vectorId, int row)
        {
            return new Stack<int>();
        }
    }
}