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

        public Drag GetPlay()
        {
            if (player.Path.Size > opponent.Path.Size)
            {
                return WallBehaviour(); // 
            }
            else
            {
                return MoveBehaviour(); // O(1)
            }
        }


        public Drag MoveBehaviour()
        {
            Drag drag = new Drag();           // O(1)
            drag.typ = Typ.Flytta;            // O(1)
            drag.point = player.Path.Pop();   // O(1)
            return drag;                      // O(1)
        }

        public Drag WallBehaviour()
        {
            Drag drag = new Drag();           // O(1)
            Path path = opponent.Path;        // O(1)
            Point current = opponent.Position;// O(1)

            for(int i = 0; i < path.Size - 1; i++)  // # longest possible path size? 10 + 10 walls placed in order for longest path
            {
                Point next = path.Peek(0);    // O(1)
                drag.point = next;            // O(1)
                Point k = new Point();        // O(1)

                if (next.X - current.X != 0) // O(1)
                {
                    drag.typ = Typ.Vertikal;
                    k = new Point(0, 1);

                    if (next.X - current.X == 1)
                    {
                        drag.point = new Point(next.X - 1, next.Y);
                    }
                }
                else if (next.Y - current.Y != 0) // O(1)
                {
                    drag.typ = Typ.Horisontell;
                    k = new Point(1, 0);
                }

                if (IsPlacementValid(next, drag.typ))
                {
                    return drag;
                }
                else if (IsPlacementValid(next - k, drag.typ))
                {
                    drag.point = next - k;
                    return drag;
                }
            }

            return MoveBehaviour();
        }

        public bool IsPlacementValid(Point root, Typ type)
        {
            int _root = Utility.ToInt(root);
            int _extension = 0;
            int N = SpelBräde.N;

            bool hasPath = false;
            int start = Utility.ToInt(opponent.Position);
            int end = 0;

            if (type == Typ.Flytta)
            {
                System.Diagnostics.Debugger.Break();
                return false;
            }
            else if (type == Typ.Horisontell)
            {
               _extension = _root + 1;

                if (IsWithinBounds(root, type) &&
                    IsWithinBounds(new Point(root.X + 1, root.Y), type) &&
                    graph.ContainsEdge(_root, _root + N) &&
                    graph.ContainsEdge(_extension, _extension + N))
                {
                    PredictForAlteredGraph(new BreadthFirstSearch(graph, start), graph as AdjacencyList, start, 0, _root, _extension, out hasPath);

                    if (hasPath)
                    {
                        return true;
                    }
                }
            }
            else if (type == Typ.Vertikal)
            {
                _extension = _root + N;

                if (IsWithinBounds(root, type) &&
                    graph.ContainsEdge(_root + 1, _root) &&
                    graph.ContainsEdge(_extension + 1, _extension))
                {
                    PredictForAlteredGraph(new BreadthFirstSearch(graph, start), graph as AdjacencyList, start, 0, _root, _extension, out hasPath);

                    if (hasPath)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsWithinBounds(Point root, Typ type)
        {
            int N = SpelBräde.N;
            Point extension = Point.Zero;

            if (type == Typ.Flytta)
            {
                System.Diagnostics.Debugger.Break();
                return false;
            }
            else if (type == Typ.Horisontell)
            {
                extension = new Point(root.X + 1, root.Y);
            }
            else if (type == Typ.Vertikal)
            {
                extension = new Point(root.X, root.Y + 1);
            }

            if (0 <= root.X && root.X < N &&
                0 <= root.Y && root.Y < N &&
                0 <= extension.X && extension.X < N &&
                0 <= extension.Y && extension.Y < N)
            {
                return true;
            }

            return false;
        }

        public Path PredictForAlteredGraph(BreadthFirstSearch instance, AdjacencyList graph, int start, int end, int root, int ext, out bool hasPath)
        {
            graph.RemoveEdge(root, ext);
            instance.Search(graph, start);
            hasPath = instance.HasPathTo(end);
            Path path = instance.PathTo(end);
            graph.AddEdge(root, ext);
            return path;
        }
    }
}