using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public abstract class AgentState
    {


        public virtual void Update(SpelBräde bräde, Graph graph)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch(graph, Utility.ToInt(Position));
            bfs.Search(graph, Utility.ToInt(Position));
            Path = bfs.PathToRow(DestinationRow, N);
        }
    }
}