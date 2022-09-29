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

        int N, mRow, oRow, mPosition, oPosition, mPreviousPosition, oPreviousPosition;
        bool mHasMoved, oHasMoved;
        Stack<int> mpath, opath;

        public AgentController(Graph graph, SpelBräde bräde, int N)
        {
            this.graph = graph;
            this.N = N;

            pathFinder = new BreadthFirstSearch();

            mRow = N - 1;
            oRow = 0;
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
        public void Update(SpelBräde bräde)
        {
            mHasMoved = (mPosition != mPreviousPosition);
            oHasMoved = (oPosition != oPreviousPosition);
            mPosition = Utility.ToInt(bräde.spelare[0].position, SpelBräde.N);
            oPosition = Utility.ToInt(bräde.spelare[1].position, SpelBräde.N);

            if ((!oHasMoved) || (!mHasMoved))
            {
                mpath = PathToRow(mPosition, mRow); //#* analysis may involve amortized analysis.
                opath = PathToRow(oPosition, oRow);
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