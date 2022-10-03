using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class WallBehaviour : IBehaviour
    {
        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board)
        {
            Drag drag = new Drag();

            Stack<int> path = opponent.Path;

            Point next = Utility.ToPoint(path.Peek());

            // A) x:1, y:0
            // B) x:-1, y:0
            // C) X:0, y:1
            // D) x:0, y:-1
            // 
            // A) (1)x:[x+1] ,y:   (2)x: ,y: 
            // B) (1)x: ,y:   (2)x: ,y: 
            // C) (1)x: ,y:   (2)x: ,y: 
            // D) (1)x: ,y:   (2)x: ,y: 



            return new Drag();
        }   

        public void Transition(IBehaviour behaviour)
        {
            if (behaviour is WallBehaviour)
            {
                return;
            }


        }
    }

}