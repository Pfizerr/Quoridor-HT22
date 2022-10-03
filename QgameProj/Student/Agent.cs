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

    private AgentController controller;
    private Graph graph;
    private bool isInitialized;
    private int N;
    

    public Agent() 
    { 
        isInitialized = false;
        N = SpelBräde.N;
    }

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {

        if(!isInitialized)
        {
            graph = new GraphImplementation(new GraphData(bräde, N), N);
            //controller = new AgentController(graph, bräde, N);
            isInitialized = true;
        }

        graph.Update(bräde);
        //controller.Update(bräde);

        return new Drag(); /*controller.MakePlay();*/
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}



public class AgentController
{
    PathAlgorithm pathFinder;
    IBehaviour behaviour;
    SpelBräde bräde;
    Graph graph;

    Opponent opponent;
    Player player;

    int N;

    public AgentController()
    {
        opponent = new Opponent(1);
        player = new Player(0);
        N = SpelBräde.N;
    }

    public void Update(SpelBräde bräde, Graph graph)
    {
        this.bräde = bräde;
        this.graph = graph;

        opponent.Update(bräde);
        player.Update(bräde);

        if (!player.LastMoved || !opponent.LastMoved)
        {
            pathFinder.Search(graph, Utility.ToInt(player.Position));
            player.ShortestPath = pathFinder.PathToRow(player.DestinationRow, N);
            pathFinder.Search(graph, Utility.ToInt(opponent.Position));
            opponent.ShortestPath = pathFinder.PathToRow(opponent.DestinationRow, N);
        }
        
    }

    public Drag GetPlay()
    {
        return behaviour.DoBehaviour(player, opponent, bräde);
    }
}



public interface IBehaviour
{
    Drag DoBehaviour(Player player, Opponent opponent, SpelBräde bräde);
    void Transition(IBehaviour behaviour);
}

public class MoveBehaviour : IBehaviour
{
    public Drag DoBehaviour(Player player, Opponent opponent, SpelBräde board)
    {
        Drag drag = new Drag();
        drag.typ = Typ.Flytta;
        drag.point = Utility.ToPoint(player.ShortestPath.Pop());

        return new Drag();
    }

    public void Transition(IBehaviour behaviour)
    {
    }
}

public class WallBehaviour : IBehaviour
{
    public Drag DoBehaviour(Player playerState, Opponent opponentState, SpelBräde board)
    {
        Drag drag = new Drag();

        return new Drag();
    }

    public void Transition(IBehaviour behaviour)
    {
    }
}


public class Opponent : AgentState
{
    public Opponent(int identifier) : base()
    {
        identifier = 1;
        DestinationRow = 0;
    }

    public override void Update(SpelBräde bräde)
    {
        base.Update(bräde);
        
        if (!LastMoved)
        {
            ShortestPath.Pop();
        }
    }
}

public class Player : AgentState
{
    public Player(int N) : base()
    {
        Identifier = 0;
        DestinationRow = N - 1;
        PreviousPosition = Point.Zero;
        DestinationRow = N - 1;
    }

    public override void Update(SpelBräde bräde)
    {
        base.Update(bräde);
    }
}

public class AgentState
{
    public Point Position
    {
        get;
        protected set;
    }

    public Point PreviousPosition
    {
        get;
        protected set;
    }

    public int DestinationRow
    {
        get;
        protected set;
    }

    public Typ PreviousPlayType
    {
        get;
        protected set;
    }

    public int PreviousPlayTypeStreak
    {
        get;
        protected set;
    }

    public bool LastMoved
    {
        get;
        protected set;
    }

    public int Identifier
    {
        get;
        protected set;
    }

    protected int N;

    public Stack<int> ShortestPath
    {
        get;
        set;
    }

    public AgentState()
    {
        N = SpelBräde.N;
        LastMoved = false;
    }

    public virtual void Update(SpelBräde bräde)
    {
        Position = bräde.spelare[Identifier].position;
        LastMoved = (Position != PreviousPosition);

    }
}