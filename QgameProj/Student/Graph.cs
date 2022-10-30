using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Student
{
    public abstract class Graph
    {
        public Graph(int N)
        {
            V = N * N;
            E = 0;
        }

        public abstract void Build(SpelBräde bräde);
        public abstract void AddEdge(int v, int w);
        public abstract bool ContainsEdge(int v, int w);
        public abstract IEnumerator<int> AdjacentTo(int v);

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