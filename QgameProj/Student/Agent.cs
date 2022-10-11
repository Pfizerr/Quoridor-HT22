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
    private Opponent opponent;
    private Player player;
    
    public IBehaviour Behaviour
    {
        get; set;
    }
    

    public Agent() 
    { 
        isInitialized = false;
    }

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {
        if(!isInitialized)
        {
            graph = new AdjacencyList(new GraphData(bräde));
            controller = new AgentController();
            isInitialized = true;
        }

        graph.Update(bräde);
        controller.Update(bräde, graph);
        opponent.Update(bräde, graph);
        player.Update(bräde, graph);

        if (player.Path.Size() <= opponent.Path.Size())
        {
            return PlaceraVägg();
        }
        else
        {
            return Flytta();
        }

        //return controller.GetPlay(graph);
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }

    public Drag PlaceraVägg()
    {
        Drag drag = new Drag();
        drag.typ = Typ.Horisontell;
        int next = opponent.Path.Peek(0);
        int nextY = (next - next % SpelBräde.N / SpelBräde.N) - opponent.Position.Y;

        if (nextY == 0)
        {
            return Flytta();
        }

        drag.point = Utility.ToPoint(next);


        return drag;
    }


    public Drag Flytta()
    {

        Drag drag = new Drag();
        drag.typ = Typ.Flytta;
        drag.point = Utility.ToPoint(player.Path.Peek(0));
        return drag;
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
}