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
            pathFinder = new BreadthFirstSearch();
        }

        public void Update(SpelBr�de br�de)
        {
            mpos = Utility.ToInt(br�de.spelare[0].position, SpelBr�de.N);
            opos = Utility.ToInt(br�de.spelare[1].position, SpelBr�de.N);
            mpath = PathToRow(mpos, orow);
            opath = PathToRow(opos, mrow);
        }

        public Stack<int> PathToRow(int start, int row)
        {
            pathFinder.Search(graph, start);

            Stack<int> path = new Stack<int>();
            int first = row * (N - 1);
            
            if (pathAlgorithm.HasPathTo(first))
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


/*
        Stack<int> playerIdealPath;
        int playerPosition;
        Stack<int> opponentIdealPath;
        int opponenotPosition;
*/