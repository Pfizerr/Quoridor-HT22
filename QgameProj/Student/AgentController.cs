using System;
using Microsoft.Xna.Framework;

namespace Student
{
    public class AgentController
    {
        private Player player;
        private Opponent opponent;
        private Graph graph;

        public AgentController()
        {
            opponent = new Opponent();
            player = new Player();
        }

        public void Update(SpelBräde bräde, Graph graph)
        {
            this.graph = graph;
            player.Update(bräde, graph);
            opponent.Update(bräde, graph);
        }

        public Drag GetPlay(Graph graph, SpelBräde bräde)
        {
            if (player.Path.Size > opponent.Path.Size)
            {
                return WallBehaviour();
            }
            else
            {
                return MoveBehaviour();
            }
        }


        public Drag MoveBehaviour()
        {
            Drag drag = new Drag();
            drag.typ = Typ.Flytta;
            drag.point = player.Path.Pop();
            return drag;
        }

        public Drag WallBehaviour()
        {
            Drag drag = new Drag();
            Path path = opponent.Path;


            Point current = opponent.Position;

            for(int i = 0; i < path.Size - 1; i++)
            {
                Point next = path.Peek(0);
                
                drag.point = next;
                if (next.X - current.X != 0)
                {
                    drag.typ = Typ.Vertikal;
                }
                else if (next.Y - current.Y != 0)
                {
                    drag.typ = Typ.Horisontell;
                }

                if (IsPlacementValid(next, drag.typ))
                {
                    return drag;
                }
            }
            
            return drag;
        }

        public bool IsPlacementValid(Point root, Typ type)
        {
            Point extension;

            if (type == Typ.Flytta)
            {
                System.Diagnostics.Debugger.Break();
                return false;
            }
            else if (typ == Typ.Horisontell)
            {
                extension = new Point(root.X + 1, root.Y);

                if (IsWithinBounds(root, type) &&
                    graph.ContainsEdge(root, new Point(root.X, root.Y + 1)) &&
                    graph.ContainsEdge(extension, new Point(extension.X, extension.Y + 1)))
                {
                    return true;
                }
            }
            else if (type == Typ.Vertikal)
            {
                extension = new Point(root.X, root.Y + 1);

                if (IsWithinBounds(root, type) &&
                    graph.ContainsEdge(new Point(root.X + 1, root.Y), root) &&
                    graph.ContainsEdge(new Point(extension.X + 1, extension.Y), extension))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsWithinBounds(int/*point*/ root, Typ type)
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
    }
}



/*



WallBehaviour --

Solution #1:
Traverse opponents shortest path and, when first possible, block path with wall tangential to path direction.

Solution #2:
Do until opponent is farther from path than player: start placing walls on a row infront of opponent.




*/