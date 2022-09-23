using Microsoft.Xna.Framework;
using System;

using Student;

class Agent:BaseAgent {

    [STAThread]
    static void Main() 
    {
        Program.Start(new Agent());
    }


    private AgentController controller;
    private Graph graph;
    private bool buildGraph;
    private int N;

    public Agent() 
    { 
        controller = new AgentController();
        buildGraph = true;
        N = SpelBräde.N;
    }

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {

        if(buildGraph)
        {
            graph = new GraphImplementation(new GraphData(bräde, N));
            buildGraph = false;
        }

        graph.Update(bräde);
        controller.Update(graph);

        return controller.MakePlay();
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}