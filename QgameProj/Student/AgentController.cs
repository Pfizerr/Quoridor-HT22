using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Student
{
    public class AgentController
    {
        PathAlgorithm pathFinder;
        Graph graph;

        int N, mrow, orow, mpos, opos; 
        Stack<int> mpath, opath;

        public AgentController(Graph graph, int N)
        {
            this.graph = graph;
            this.N = N;

            pathFinder = new BreadthFirstSearch();

            mrow = N - 1;
            orow = 0;
        }

        public void Update(SpelBräde bräde)
        {
            mpos = Utility.ToInt(bräde.spelare[0].position, SpelBräde.N);
            opos = Utility.ToInt(bräde.spelare[1].position, SpelBräde.N);
            mpath = PathToRow(mpos, orow);
            opath = PathToRow(opos, mrow);
        }

        public Stack<int> PathToRow(int start, int row)
        {
            pathFinder.Search(graph, start);

            Stack<int> path = new Stack<int>();
            int first = row * (N - 1);
            
            if (pathFinder.HasPathTo(first))
            {
                path = pathFinder.PathTo(first);
            }

            for (int i = 1; i < N; i++)
            {
                int t = row * (N - 1) + i;

                if (!pathFinder.HasPathTo(t))
                {
                    continue;
                }

                Stack<int> tPath = pathFinder.PathTo(t);

                if (tPath.Count < path.Count)
                {
                    path = tPath;
                }
            }

            return path;
        }

        public Drag MakePlay()
        {
            Drag drag = new Drag();
            drag.typ = Typ.Flytta;
            drag.point = Utility.ToPoint(mpath.Pop(), N);

            return drag;
        }
    }
}