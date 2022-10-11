using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class WallBehaviour : IBehaviour
    {
        private bool hasStartedBlocking; //isBlocking
        private int root, previousRoot, firstRoot, currentGrowth, row;
        Typ type;

         public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
         {
//             Drag drag = new Drag();
//             Path path = opponent.Path;
// 
//             int nextAlongX = path.NextAlongX();
//             int nextAlongY = path.NextAlongY();
//             int nextY = opponent.Direction.Y;
//             int next = path.Peek(0);
// 
//             if (nextY != 0 
//                 && IsBlockable(Utility.ToInt(opponent.Position), next, Typ.Horisontell, graph))
//                 && IsWithinBounds(next, Typ.Horisontell)
//             {
//                 drag.typ = Typ.Horisontell;
//                 drag.point = next;
//                 out drag;
//                 return true;
//             }
// 
             return new Drag();
         }


        public bool IsWithinBounds(int root, Typ type)
        {
            if (type == Typ.Flytta)
            {
                System.Diagnostics.Debugger.Break();
                return false;
            }

            int N = SpelBräde.N;
            int extension = (type == Typ.Horisontell) ? root + 1 : root + N;

            Point r = Utility.ToPoint(root);
            Point e = Utility.ToPoint(extension);

            if (0 <= r.X && r.X < N && 0 <= r.Y && r.Y < N)
            {
                return true;
            }

            return false;
        }

        public bool IsBlockable(int v, int w, Typ type, Graph graph)
        {
            if (type == Typ.Flytta)
            {
                System.Diagnostics.Debugger.Break();
                return false;
            }
            else if (type == Typ.Horisontell)
            {
                if (graph.ContainsEdge(v, w) && graph.ContainsEdge(v + 1, w + 1) && IsWithinBounds(w, type))
                {
                    return true;
                }
            }
            else if (type == Typ.Vertikal)
            {
                if (graph.ContainsEdge(v, w) && graph.ContainsEdge(v + SpelBräde.N, w + SpelBräde.N) && IsWithinBounds(w, type))
                {
                    return true;
                }
            }

            return false;
        }

        public void Transition(AgentController controller, IBehaviour behaviour)
        {
            if (behaviour is WallBehaviour)
            {
                return;
            }

            controller.Behaviour = behaviour;
        }

        // are nodes v and w blockable with a wall with given orientation placed between v and w

    }
}