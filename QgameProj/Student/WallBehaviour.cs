using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Student
{
    public class WallBehaviour : IBehaviour
    {
        public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board, Graph graph)
        {
            Drag drag = new Drag();

            Stack<int> path = opponent.Path;
            int current = Utility.ToInt(opponent.Position);
            int next = path.Peek();
            int root = 0;

            if (opponent.Direction.X != 0)
            {
                drag.typ = Typ.Vertikal;

                if (IsBlockable(current, next, drag.typ, graph))
                {
                    root = next;
                }
                else if (IsBlockable(current - 1, next - 1, drag.typ, graph))
                {
                    root = next - 1;
                }
                else System.Diagnostics.Debugger.Break();
                drag.point = Utility.ToPoint(root);
            }
            else if (opponent.Direction.Y != 0)
            {
                drag.typ = Typ.Horisontell;

                if (IsBlockable(current, next, drag.typ, graph))
                {
                    root = next;
                }
                if (IsBlockable(current, next, drag.typ, graph))
                {
                    root = next - SpelBräde.N;
                }
                else System.Diagnostics.Debugger.Break();
                drag.point = Utility.ToPoint(root);
            }

            return new Drag();
        }

        public void Transition(IBehaviour behaviour)
        {
            if (behaviour is WallBehaviour)
            {
                return;
            }

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
                if (graph.ContainsEdge(v, w) && graph.ContainsEdge(v, w + 1))
                {
                    return true;
                }
            }
            else if (type == Typ.Vertikal)
            {
                if (graph.ContainsEdge(v, w) && graph.ContainsEdge(v, w + SpelBräde.N))
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