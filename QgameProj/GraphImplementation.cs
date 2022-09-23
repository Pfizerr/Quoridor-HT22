using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QgameProj
{
    public class GraphImplementation : Graph
    {

        public GraphImplementation(GraphData data) : this(data.Next)
        {
            
        }

        public GraphImplementation()
        {

        }

        public void AddEdge(int v, int w)
        {

        }


        public void RemoveEdge(int v, int w)
        {

        }

        public bool ContainsEdge(int v, int w)
        {
            return new bool();
        }
        
        public IEnumerator<int> AdjacentTo(int v, int w)
        {
            return new IEnumerator<int>();
        }
    }
}