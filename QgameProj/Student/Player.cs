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

        public override void Update(SpelBräde bräde, Graph graph)
        {
            base.Update(bräde, graph);
        }
    }
}