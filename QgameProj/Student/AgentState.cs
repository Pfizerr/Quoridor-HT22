using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class AgentState // NOT ABSTRACT, JUST ONE CLASS FOR BOTH, FFS.... #* #* #* #* #* #*
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

//         public bool LastMoved
//         {
//             get;
//             protected set;
//         }

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

        public Point PreviousDirection
        {
            get;
            protected set;
        }

        public AgentState()
        {
            N = SpelBräde.N;
        }

        public virtual void Update(SpelBräde bräde, Graph graph)
        {
            PreviousPosition = Position;
            Position = bräde.spelare[Identifier].position;

            BreadthFirstSearch bfs = new BreadthFirstSearch(graph, Utility.ToInt(Position));
            bfs.Search(graph, Utility.ToInt(Position));
            Path = bfs.PathToRow(DestinationRow, N);

            Point next = Utility.ToPoint(Path.Peek());
            Direction = new Point(next.X - Position.X, next.Y - Position.Y);
            PreviousDirection = new Point(Position.X - PreviousPosition.X, Position.Y - PreviousPosition.Y);
        }

        public bool HasMoved() //??? consistency
        {
            return (Position != PreviousPosition);
        }
    }
}