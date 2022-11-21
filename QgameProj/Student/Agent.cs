using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using Student;

class Agent:BaseAgent {

    [STAThread]
    static void Main() 
    {
        Program.Start(new Agent());
    }

    private Graph graph;
    private bool isInitialized;
    private AgentController controller;
    

    public Agent() 
    { 
        isInitialized = false;
    }

    public override Drag SökNästaDrag1(SpelBräde bräde)
    {
        if (!isInitialized)
        {
            graph = new AdjacencyList(bräde);
            #region ~ 5N^2, O(N^2)

            // O(1):   init = true;
            // O(N^2): adjacencyList = new List<int>[V];
            //       
            // O(N^2): for (int i = 0; i < V; i++)
            //         {
            // O(1):       adjacencyList[i] = new List<int>();
            //         }
            //         
            // O(N^2): Build(bräde);

            #endregion

            controller = new AgentController();
            #region O(1)

            // O(1):   opponent = new Opponent();
            //      O(1):   Identifier = 0;
            //      O(1):   DestinationRow = N - 1;
            //      O(1):   PreviousPosition = Point.Zero;
            // O(1):   player = new Player();
            //      O(1):   firstRoundSkip = true;
            //      O(1):   Identifier = 1;
            //      O(1):   DestinationRow = 0;

            #endregion

            // O(1): isInitialized = true;
        }

        graph.Build(bräde);
        #region O(N^2)
        // See analysis for graph.Build
        #endregion

        controller.Update(bräde, graph);
        #region 
        //          this.graph = graph;
        //          player.Update(bräde, graph);
        //          opponent.Update(bräde, graph);
        #endregion

        return controller.GetPlay();
        #region
        //          if (player.Path.Size > opponent.Path.Size)
        //          {
        //              return WallBehaviour();
        //          }
        //          else
        //          {
        //              return MoveBehaviour();
        //          }
        #endregion
    }


    public override Drag SökNästaDrag(SpelBräde bräde) 
    {
        if (!isInitialized) 
        {
            graph = new AdjacencyList(bräde); // ~5N^2, O(N^2)

            /*  adjacencyList = new List<int>[V]; O(N^2)
                for (int i = 0; i < V; i++) // O(N^2)
                { O(1) }
                Build(bräde); // O(N^2)  */

            controller = new AgentController(); //O(1)
        } // O(N^2)

        graph.Build(bräde); // O(N^2)
        

        controller.Update(bräde, graph);
        /*  this.graph = graph;
            player.Update(bräde, graph);
            opponent.Update(bräde, graph);   */

        return controller.GetPlay();
        /*  if (player.Path.Size > opponent.Path.Size)
            {
                return WallBehaviour();
            }
            else
            {
                return MoveBehaviour();
            }  */


        /// WALLBEHAVIOUR
        /// *
        /*              Drag drag = new Drag();           
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

                    return MoveBehaviour();  */

        /// MOVEBEHAVIOUR
        /// *
        /*  Drag drag = new Drag();           
            drag.typ = Typ.Flytta;            
            drag.point = player.Path.Pop();   
            return drag;  */
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
{
System.Diagnostics.Debugger.Break();
return SökNästaDrag(bräde);
}
}