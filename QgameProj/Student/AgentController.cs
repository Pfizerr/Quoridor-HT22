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
                return WallBehaviour();
                #region
                //          [.. O(1) ..]
                //   
                // O(1):    for (int i = 0; i < path.Size - 1; i++)
                //          {
                //              [.. O(1) ..]
                //
                // O(1):        if (next.X - current.X != 0)
                //              {
                // O(1):            [.. O(1) ..]
                //              }
                // O(1):        else if (next.Y - current.Y != 0)
                //              {
                // O(1):            [.. O(1) ..]
                //              }
                // O(1):    
                // O(1):        if (IsPlacementValid(next, drag.typ))
                //              {
                // O(1):            return drag;
                //              }
                // O(1):        else if (IsPlacementValid(next - k, drag.typ))
                //              {
                // O(1):            drag.point = next - k;
                // O(1):            return drag;
                //              }
                //          }
                //    
                // O(1):    return MoveBehaviour();
                #endregion
            }
            else
            {
                return MoveBehaviour();
                #region O(1)
                // O(1):   Drag drag = new Drag();
                // O(1):   drag.typ = Typ.Flytta;
                // O(1):   drag.point = player.Path.Pop();
                // O(1):   return drag;
                #endregion
            }
        } public Drag drag = new Drag();


        public Drag MoveBehaviour()
        {
            // O(1):   Drag drag = new Drag();
            // O(1):   drag.typ = Typ.Flytta;
            // O(1):   drag.point = player.Path.Pop();
            return drag;
        }

        public Drag WallBehaviour()
        {
            
        }

        public bool IsPlacementValid(Point root, Typ type)
        {
            if (type == Typ.Horisontell)
            {
                if (IsWithinBounds(root, type) &&
                    IsWithinBounds(new Point(root.X + 1, root.Y), type) &&
                    graph.ContainsEdge(_root, _root + N) &&
                    graph.ContainsEdge(_extension, _extension + N))
                {
                    PredictForAlteredGraph(new BreadthFirstSearch(graph, start), graph as AdjacencyList, start, 0, _root, _extension, out hasPath);
                }
            }

            return false;
        }

        public bool IsWithinBounds(Point root, Typ type)
        {
        /// 8-vägsjämförelse?
        //
        // O(X):    if (0 <= root.X && root.X < N &&
        // O(X):        0 <= root.Y && root.Y < N &&
        // O(X):        0 <= extension.X && extension.X < N &&
        // O(X):        0 <= extension.Y && extension.Y < N)
        //          {
        // O(X):        return true;
        //          }
        // O(X):
            return false;
        }

        /// <summary>
        /// O(E+V), due to BFS SP-search.
        /// </summary>
        public Path PredictForAlteredGraph(BreadthFirstSearch instance, AdjacencyList graph, int start, int end, int root, int ext, out bool hasPath)
        {
            graph.RemoveEdge(root, ext);
            #region O(1)
            // See analysis for Graph.RemoveEdge.
            #endregion

            instance.Search(graph, start);
            #region O(E+V)
            // See analysis for BFS.
            #endregion

            hasPath = instance.HasPathTo(end);
            #region O(1)
            // See analysis for BFS.HasPathTo.
            #endregion

            Path path = instance.PathTo(end);
            #region O(X) ???Linear??? O(N)
            // See analysis for BFS.PathTo.
            #endregion

            graph.AddEdge(root, ext);
            #region O(1)
            // See analysis for Graph.AddEdge.
            #endregion

            return path;
        }
    }
}

/*
        public Drag WallBehaviour()
        {
            Drag drag = new Drag();           
            Path path = opponent.Path;        
            Point current = opponent.Position;

            for(int i = 0; i < path.Size - 1; i++) 
            {
                Point next = path.Peek(0);    
                drag.point = next;            
                Point k = new Point();        

                if (next.X - current.X != 0) 
                {
                    drag.typ = Typ.Vertikal;
                    k = new Point(0, 1);

                    if (next.X - current.X == 1)
                    {
                        drag.point = new Point(next.X - 1, next.Y);
                    }
                }
                else if (next.Y - current.Y != 0)
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
*/