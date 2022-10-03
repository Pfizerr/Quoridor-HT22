using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class MoveBehaviour : IBehaviour
    {
        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {
            Drag drag = new Drag();
            drag.typ = Typ.Flytta;
            drag.point = Utility.ToPoint(player.Path.Pop());

            return new Drag();
        }

        public void Transition(IBehaviour behaviour)
        {
            if (behaviour is MoveBehaviour)
            {
                return;
            }
        }
    }
}