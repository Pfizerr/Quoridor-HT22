using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public abstract class AgentState
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

        public Path Path
        {
            get;
            set;
        }

        public Point PreviousDirection
        {
            get;
            protected set;
        }

        public bool HasMoved
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
            // O(1):    PreviousPosition = Position;
            // O(1):    Position = bräde.spelare[Identifier].position;
            // O(1):    HasMoved = Position != PreviousPosition;

            // O(1): BreadthFirstSearch bfs = new BreadthFirstSearch(graph, Utility.ToInt(Position));
            // O(E+V): bfs.Search(graph, Utility.ToInt(Position));
            // O(X): Path = bfs.PathToRow(DestinationRow, N);


            // O(1):    Point next = Path.Peek(0);
            // O(1):    Direction = new Point(next.X - Position.X, next.Y - Position.Y);
            //   
            // O(1):    if (PreviousPosition == Point.Zero)
            //          {
            // O(1):        PreviousPosition = Position;
            //          }
            //  
            // O(1):    PreviousDirection = new Point(Position.X - PreviousPosition.X, Position.Y - PreviousPosition.Y);
        }
    }
}