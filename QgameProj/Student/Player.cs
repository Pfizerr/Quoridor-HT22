using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class Player : AgentState
    {
        public Player(int N) : base()
        {
            Identifier = 0;
            DestinationRow = N - 1;
            PreviousPosition = Point.Zero;
            DestinationRow = N - 1;
        }

        public override void Update(SpelBr�de br�de, Graph graph, bool refreshPath)
        {
            base.Update(br�de, graph, refreshPath);
        }
    }
}