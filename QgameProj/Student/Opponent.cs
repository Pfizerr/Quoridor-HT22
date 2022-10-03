using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class Opponent : AgentState
    {
        public Opponent(int identifier) : base()
        {
            identifier = 1;
            DestinationRow = 0;
        }

        public override void Update(SpelBr�de br�de, Graph graph, bool refreshPath)
        {
            base.Update(br�de, graph, refreshPath);

            if (!LastMoved)
            {
                Path.Pop();
            }
        }
    }
}