namespace Student
{
    public class AgentController
    {
        private SpelBräde bräde;
        private Opponent opponent;
        private Player player;

        public IBehaviour Behaviour
        {
            get;
            set;
        }

        public AgentController()
        {
            opponent = new Opponent();
            player = new Player();
            Behaviour = new MoveBehaviour();
        }

        public void Update(SpelBräde bräde, Graph graph)
        {
            //bool refreshPaths = (!player.LastMoved || !opponent.LastMoved) ? true : false;

            opponent.Update(bräde, graph);
            player.Update(bräde, graph);

            if (player.Path.Size() > opponent.Path.Size())
            {
                Behaviour.Transition(this, new WallBehaviour());
            }
            else
            {
                Behaviour.Transition(this, new MoveBehaviour());
            }
        }

        public Drag GetPlay(Graph graph)
        {
            return Behaviour.DoBehaviour(player, opponent, bräde, graph);
        }
    }
}



//     public class AgentController
//     {
//         PathAlgorithm pathFinder;
//         Graph graph;
// 
//         int N, mRow, oRow, mPosition, oPosition, mPreviousPosition, oPreviousPosition;
//         bool mHasMoved, oHasMoved;
//         Typ mPreviousPlayType;
//         int mStreak;
//         Point oPointPosition, oLastMovement;
// 
//         Stack<int> mpath, opath;
// 
//         public AgentController(Graph graph, SpelBräde bräde, int N)
//         {
//             this.graph = graph;
//             this.N = N;
// 
//             pathFinder = new BreadthFirstSearch();
// 
//             oPointPosition = bräde.spelare[1].position;
//             oPreviousPosition = Utility.OffsetY(oPointPosition.X, oPointPosition.Y, 1);
//             oRow = 0;
//             /*mPreviousPosition = Utility.ToInt(bräde.spelare[0].position, N);*/
//             mPreviousPosition = 0;
//             mRow = N - 1;
//         }
// 
//         public Stack<int> PathToRow(int start, int row)
//         {
//             pathFinder.Search(graph, start);
// 
//             Stack<int> path = new Stack<int>();
//             int first = row * N;
//             
//             if (pathFinder.HasPathTo(first)) //#* needed ? incorporate into loop ?
//             {
//                 path = pathFinder.PathTo(first);
//             }
// 
//             for (int i = 1; i < N; i++)
//             {
//                 int t = row * N + i;
// 
//                 if (!pathFinder.HasPathTo(t))
//                 {
//                     continue;
//                 }
// 
//                 Stack<int> tPath = pathFinder.PathTo(t);
// 
//                 if (tPath.Count < path.Count)
//                 {
//                     path = tPath;
//                 }
//             }
//             
// 
//             return path;
//         }
// 
//         public Drag MoveBehaviour(Drag drag)
//         { 
//             drag.typ = Typ.Flytta;
// 
//             drag.point = Utility.ToPoint(mpath.Pop());
// 
//             return drag;
//         }
// 
//         public void Update(SpelBräde bräde)
//         {
//             mHasMoved = (mPosition != mPreviousPosition);
//             oHasMoved = (oPosition != oPreviousPosition);
//             mPosition = Utility.ToInt(bräde.spelare[0].position);
// 
//             Point oPointPosition = bräde.spelare[1].position;
//             oPosition = Utility.ToInt(oPointPosition);
// 
//             if ((!oHasMoved) || (!mHasMoved))
//             {
//                 mpath = PathToRow(mPosition, mRow); 
//                 opath = PathToRow(oPosition, oRow);
//             }
//             else
//             {
//                 opath.Pop();
//             }
//         }
// 
//         public Drag MakePlay()
//         {
//             Drag drag = new Drag();
// 
//             
//             if (opath.Count < mpath.Count)
//             { 
//                 drag = WallBehaviour(drag);
//             }
//             else
//             {
//                 drag.typ = Typ.Flytta;
//                 drag.point = Utility.ToPoint(mpath.Pop());
//             }
// 
//             if (drag.typ == mPreviousPlayType)
//             {
//                 mStreak++;
//             }
// 
// 
//             int dx = oPosition % N - oPreviousPosition % N;
//             int dy = (oPosition - oPosition % N - oPreviousPosition - oPreviousPosition % N) / N;
//             oLastMovement.X = (dx != 0) ? dx : oLastMovement.X;
//             oLastMovement.Y = (dy != 0) ? dy : oLastMovement.Y;
// 
//             mPreviousPlayType = drag.typ;
// 
//             return drag;
//         }
// 
//         public Drag WallBehaviour(Drag drag)
//         {
// 
//             return new Drag();
// 
//         }
//     }



/*
  
  
  

//#* analysis may involve amortized analysis.
mpath = PathToRow(mPosition, mRow);



Wall-placement/Movement:


if (o2DMovement.Y != 0 || (mPreviousPlayType == Typ.Horisontell && mStreak == 1))
            { // IF: opponent moved along Y-axis, OR: last move was H-placement with a streak of 1


            // Will i move, or place a wall?
            // * IF: opponent is closer to their target row,
            // - THEN: place wall to increase the length of their ideal path by 1.
            // ~ CONSIDER: previous wall-placing frequency
            // ~~ CONSIDER: current/upcoming board segment wall prevalence
            // ~~~ CONSIDER: expected (perhaps with regards to (^) and (^^)) distance to goal)
            // ~~~~ CONSIDER: expected distance: 'Q' < certain value (with respect to opponents remaining wall count ('R'))
            // etc. etc. etc....


// '<' or '<=', what would the real opponent be using for threshold to place start placing walls, (consider Q)
if (opath.Count < mpath.Count) 

// opponent closer to goal than me.
else

// encapsulate functionality in function (if functionality beyond simple path traversal, i can't see why now
// alternatively, simply traverse path.
// drag = MoveBehaviour(drag);

// State-machine for wall/movement behaviour?
no, since there will only ever be two states.

Wall-placement:

            // Do i want to place the wall horizontally or vertitally ?
            // * IF: opponents movement is along Y-axis (OR ,
            // - THEN: place wall along X-axis, i.e. horizontal placement.
            //
            // * IF: opponents mvovement is along X-axis ... [(-- ON PAPER --)]



                // the wall is, for simplicity, chosen to block using a wall perpendicular to opponents current
                // trajectory at a distance of 1 from the opponent (immediate block) with the wall offsetting so
                // that it touches any nearby board-border, or so that the path will be its longest if both
                // options are similar, along the direction of last movement in that dimension, if none, arbitrarilly.

                // (REMINDER + EDIT TO COMMENTS): existing walls can disallow wall placements.
                // Top-level conditional!



                // for right-leaning wall (extension), the x-offset will be 0, i.e., placement immediately under
                // opponent, as for left-leaning wall, x-offset will be -1, placement under and left of opponent.

                //                 if (oPosition == 1)
                //                 { // if 
                // 
                //                 }
                //                 else (oPosition == 7)
                //                 {
                // 
                //                 }



// (REMINDER): perhaps (~atleast in some way) determine path and count its length post-placement
                // to determine best choice of action.





Movement:






// (initial?) movement behaviour setup, simply traverse path.





*/

//         public bool CanBlockVertex(int index)
//         {
//             bool result = true;
//             int row = index % N;
// 
//             for (int count = 0; count < N; count++)
//             {
//                 graph
//             }
//         }