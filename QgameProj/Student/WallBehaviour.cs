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
            Drag drag = new Drag();
            Path path = opponent.Path;

            int nextAlongX = path.NextAlongX();
            int nextAlongY = path.NextAlongY();
            int nextX = opponent.Direction.X;
            int nextY = opponent.Direction.Y;
            int next = path.Peek(0);

            bool result = true; 

            if (!hasStartedBlocking)
            {
                hasStartedBlocking = true;
                row = (next - next % SpelBräde.N) / SpelBräde.N;
                currentGrowth = nextAlongX;

                if (nextX != 0)
                {
                    //TryVertical();
                    firstRoot = root = next + nextAlongX;
                    result = TryHorizontal(next + nextAlongX, nextAlongY, opponent, graph, out drag);
                }
                else if (nextY != 0)
                {
                    firstRoot = root = next + nextAlongX;
                    result = TryHorizontal(next + nextAlongX, nextAlongY, opponent, graph, out drag);
                }
                else System.Diagnostics.Debugger.Break();
                
            }
            else // HasStartedBlocking 
            {
                if (currentGrowth == 0)
                {
                    currentGrowth = (nextAlongX == 0) ? opponent.PreviousDirection.X : nextAlongX;
                }

                previousRoot = root;
                root += currentGrowth;

                if (nextAlongX == -1)
                {
                    TryLeft(next, nextAlongY, opponent, graph, out drag);
                }
                else if (nextAlongX == 1)
                {
                    TryRight(next, nextAlongY, opponent, graph, out drag);
                }
                else if (opponent.PreviousDirection.X == -1)
                {
                    TryLeft(next, nextAlongY, opponent, graph, out drag);
                }
                else if (opponent.PreviousDirection.X == 1)
                {
                    TryRight(next, nextAlongY, opponent, graph, out drag);
                }
            }

            if (result == false)
            {
                System.Diagnostics.Debugger.Break();
            }

            return drag;
        }

        public bool TryLeft(int root, int moveY, Opponent opponent, Graph graph, out Drag drag)
        {
            drag = new Drag();
            for (int i = root; i > 0; i--)
            {
                if (TryHorizontal(i, moveY, opponent, graph, out drag))
                {
                    drag.point = Utility.ToPoint(root);
                    return true;
                }
            }

            return false;
        }

        public bool TryRight(int root, int moveY, Opponent opponent, Graph graph, out Drag drag)
        {
            drag = new Drag();
            for (int i = firstRoot; i < SpelBräde.N - 1; i++)
            {
                if (TryHorizontal(root + i, moveY, opponent, graph, out drag))
                {
                    drag.point = Utility.ToPoint(root);
                    return true;
                }
            }

            return false;
        }

        public bool TryHorizontal(int root, int moveY, Opponent opponent, Graph graph, out Drag drag)
        {
            drag.typ = type = Typ.Horisontell;
            drag.point = Point.Zero;

            int ext = root + 1;
            moveY = moveY * SpelBräde.N;
            int current = root + moveY;
            bool hasPath = false;

            if (IsWithinBounds(root, Typ.Horisontell) &&
                graph.ContainsEdge(root, current) &&
                graph.ContainsEdge(ext, current + 1))
            {
                // Connected Components
                using (BreadthFirstSearch bfs = new BreadthFirstSearch(graph, current))
                {
                    bfs.PredictForAlteredGraph(bfs, graph as AdjacencyList, current, opponent.DestinationRow, root, root + 1, out hasPath);

                    // use this path for opponent instead of rebuilding next turn
                }

                if (hasPath)
                {
                    drag.point = Utility.ToPoint(root);
                    return true;
                }
            }

            return false;
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