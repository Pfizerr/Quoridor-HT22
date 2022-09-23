using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student
{
    public class AgentController
    {
        PathAlgorithm pathFinder;
        Graph graph;
        Graph graphRef;

        public AgentController()
        {
            pathFinder = new BreadthFirstSearch();
        }

        public void Update(Graph graph)
        {
            this.graph = graph;
        }

        public Drag MakePlay()
        {
            return new Drag();
        }
    }
}