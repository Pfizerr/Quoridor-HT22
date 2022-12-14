using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class Player : AgentState
    {
        public Player() : base()
        {
            Identifier = 0;
            DestinationRow = N - 1;
            PreviousPosition = Point.Zero;
        }

        public int RemainingWalls
        {
            get;
            protected set;
        }

        public override void Update(SpelBräde bräde, Graph graph)
        {
            RemainingWalls = bräde.spelare[0].antalVäggar;
            base.Update(bräde, graph);
        }
    }
}