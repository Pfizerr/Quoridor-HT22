using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class WallBehaviour : IBehaviour
    {
        private int row;
        private Typ type;
        private int[9] root;
        private bool isBlocking;

        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {
            Drag drag = new Drag();
            int current = Utility.ToInt(opponent.Position);
            Path path = opponent.Path;
            int next = path.Peek(0);

            int nextMoveAlongX = path.NextAlongX();
            int nextMoveAlongY = path.NextAlongY();



            if (!isBlocking)
            {
                row = (current - current % SpelBräde.N ) / SpelBräde.N;
                type = Typ.Horisontell;
                isBlocking = true;

                int root_left = next + SpelBräde.N;
                int root_right = current + SpelBräde.N;

                

                bool result = TryVertical(current - (SpelBräde.N * nextMoveAlongY), nextMoveAlongY, nextMoveAlongX, graph, out drag);

                // try all vertical left

                

                // try all vertical right



            }
        }

        public bool TryVertical(int root, int moveY, int offset, Graph graph, out Drag drag)
        {
            int extension = root + 1;
            moveY = moveY * SpelBräde.N;


            IsWithinBounds(root, Typ.Horisontell);

            if (graph.ContainsEdge(root, moveY) && graph.ContainsEdge(extension, moveY))
            {
                drag.typ = Typ.Horisontell;
                drag.point = Utility.ToPoint(root);



            }

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



        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {
            Drag drag = new Drag();

            Path path = opponent.Path;
            int agentPosition = Utility.ToInt(opponent.Position);
            int next = path.Peek(0);

            TryHorizontal(opponent, graph, agentPosition, next, out drag);

            previousType = drag.typ;
            previousRootPlacement = Utility.ToInt(drag.point);

            return drag;
        }

        public bool TryVertical(Opponent opponent, Graph graph, int current, int next, out Drag drag)
        {
            drag = new Drag();
            drag.typ = Typ.Vertikal;

            if (opponent.PreviousDirection.Y != 0)
            {
                if (TryHorizontal(opponent, graph, current, next, out drag))
                {
                    return true;
                }
            }


            return false;
        }

        public bool TryHorizontal(Opponent opponent, Graph graph, int current, int next, out Drag drag)
        {
            drag = new Drag();
            drag.typ = Typ.Horisontell;
            int direction = opponent.PreviousDirection.X;

            
            if (!IsWithinBounds(current, drag.typ) || !IsWithinBounds(next, drag.typ))
            {
                return false;
            }
            else if (IsBlockable(current, next, drag.typ, graph))
            {
                drag.point = Utility.ToPoint(next);
                return true;
            }
            if (direction != 0 && TryHorizontal(opponent, graph, current + direction, next + direction, out drag))
            {
                return true;
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

        public enum WallOrientation
        {
            Vertical,
            Horizontal
        }
    }
}