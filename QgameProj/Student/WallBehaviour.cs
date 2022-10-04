using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class WallBehaviour : IBehaviour
    {
        private int previousAgentPosition;
        private int previousRootPlacement;
        private Typ previousType;

        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {
            Drag drag = new Drag();

            Stack<int> path = opponent.Path;
            int agentPosition = Utility.ToInt(opponent.Position);
            int next = path.Peek();
            int rootPlacement = 0;

            if (opponent.Direction.X != 0)
            {
                drag.typ = Typ.Vertikal;

                if (IsBlockable(agentPosition, next, drag.typ, graph))
                {
                    rootPlacement = next;
                }
                //Traverse graph and discover direction to which opponent will go, this determines what side of opponent to place wall.
                 /*else if (IsBlockable(current - 1, next - 1, drag.typ, graph))
                 {
                     root = next - 1;
                 }*/
                else System.Diagnostics.Debugger.Break();
                drag.point = Utility.ToPoint(rootPlacement);
            }
            else if (opponent.Direction.Y != 0)
            {
                drag.typ = Typ.Horisontell;

                if (IsBlockable(agentPosition, next, drag.typ, graph))
                {
                    rootPlacement = next;
                }
                else if (IsBlockable(agentPosition - 1, next - 1, drag.typ, graph))
                {
                    rootPlacement = next - 1;
                }
                else if (IsBlockable(agentPosition - 2, next - 2, drag.typ, graph))
                {
                    rootPlacement = next = 2;
                }
                else if (false) // check new shortest path for each placement along axis. (possibly). (might fuck worst-case like royalty)
                {

                }
                else System.Diagnostics.Debugger.Break();
                drag.point = Utility.ToPoint(rootPlacement);
            }

            return drag;
        }

        public bool TryVertical(Opponent opponent, int current, int next, out Drag drag)
        {
            drag = new Drag();
            drag.typ = Typ.Vertikal;
            int root = 0;

            if (opponent.PreviousDirection.Y != 0)
            {
                bool result = TryHorizontal(opponent, current, next, out drag);

                //#* 

                if (!result)
            }


            return false;
        }

        public bool TryHorizontal(Opponent opponent, int current, int next, out Drag drag)
        {
            drag = new Drag();
            drag.typ = Typ.Horisontell;
            int root = 0;


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

        public bool IsWithinBounds(int root, Typ type)
        {
            if(type == Typ.Flytta)
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

        public enum WallOrientation
        {
            Vertical,
            Horizontal
        }
    }
}