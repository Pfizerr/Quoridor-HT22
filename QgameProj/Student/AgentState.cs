using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class AgentState
    {
        public Point Position
        {
            get;
            protected set;
        }

        public Point PreviousPosition
        {
            get;
            protected set;
        }

        public int DestinationRow
        {
            get;
            protected set;
        }

        public Typ PreviousPlayType
        {
            get;
            protected set;
        }

        public int PreviousPlayTypeStreak
        {
            get;
            protected set;
        }

        public bool LastMoved
        {
            get;
            protected set;
        }

        public int Identifier
        {
            get;
            protected set;
        }

        public Point Direction
        {
            get;
            protected set;
        }

        protected int N;

        public Stack<int> Path
        {
            get;
            set;
        }

        protected Point direction;

        public AgentState()
        {
            N = SpelBräde.N;
            LastMoved = false;
        }

        public virtual void Update(SpelBräde bräde, Graph graph, bool refreshPath)
        {
            Position = bräde.spelare[Identifier].position;
            LastMoved = (Position != PreviousPosition);


            if (refreshPath)
            {
                BreadthFirstSearch bfs = new BreadthFirstSearch(graph, Utility.ToInt(Position));
                bfs.Search(graph, Utility.ToInt(Position));
                Path = bfs.PathToRow(DestinationRow, N);
            }

            int next = Path.Peek();
            Direction = new Point(next - Position.X, next - Position.Y);
        }
    }
}