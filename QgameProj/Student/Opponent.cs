using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class Opponent : AgentState
    {
        private bool firstRoundSkip;

        public Opponent() : base()
        {
            firstRoundSkip = true;
            Identifier = 1;
            DestinationRow = 0;
        }

        public override void Update(SpelBräde bräde, Graph graph)
        {
            if (HasMoved && !firstRoundSkip)
            {
                Path.Pop();
            }
            else
            {
                firstRoundSkip = false;
            }
            

            base.Update(bräde, graph);
        }
    }
}