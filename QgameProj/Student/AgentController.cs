using System;
using Microsoft.Xna.Framework;

namespace Student
{
    public class AgentController
    {
        private Player player;
        private Opponent opponent;

        public AgentController()
        {
            opponent = new Opponent();
            player = new Player();
        }

        public void Update(SpelBräde bräde, Graph graph)
        {
            player.Update(bräde, graph);
            opponent.Update(bräde, graph);
        }

        public Drag GetPlay(Graph graph, SpelBräde bräde)
        {
            if (player.Path.Size() > opponent.Path.Size())
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
            Point next = path.Peek(0);

            for(int i = 0; i < path.Size - 1; i++)
            {
                
            }
            
            return drag;
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