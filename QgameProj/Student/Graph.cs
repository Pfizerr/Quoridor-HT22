using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Student
{
    public abstract class Graph
    {
        public Graph()
        {
        }

        public abstract void Rebuild(SpelBräde bräde);

        public abstract void AddEdge(int v, int w);

        public abstract IEnumerator<int> AdjacentTo(int v);

        public abstract bool ContainsEdge(int v, int w);

        public new string ToString()
        {
            String s = V + " vertices, " + E + " edges\n";
            for (int v = 0; v < V; v++)
            {
                s += v + ": ";
                IEnumerator<int> enumerator = AdjacentTo(v);
                while (enumerator.MoveNext())
                    s += enumerator.Current + " ";
                s += "\n";
            }
            return s;
        }

        public int V 
        { 
            get; 
            protected set; 
        }
        public int E 
        { 
            get; 
            protected set; 
        }
        
        public int N
        {
            get;
            protected set;
        }
    }
}