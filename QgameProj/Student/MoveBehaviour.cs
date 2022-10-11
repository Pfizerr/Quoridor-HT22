using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class MoveBehaviour : IBehaviour
    {
        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {

        }

        public void Transition(AgentController controller, IBehaviour behaviour)
        {
            if (behaviour is MoveBehaviour)
            {
                return;
            }

            controller.Behaviour = behaviour;
        }
    }
}