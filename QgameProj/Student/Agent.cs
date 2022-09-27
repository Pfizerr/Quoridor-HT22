using Microsoft.Xna.Framework;
using System;

using Student;

class Agent:BaseAgent {

    [STAThread]
    static void Main() 
    {
        Program.Start(new Agent());
    }

    public static bool DEBUG;

    private AgentController controller;
    private Graph graph;
    private bool initialize;
    private int N;
    

    public Agent() 
    { 
        initialize = true;
        N = SpelBräde.N;
    }

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {

        if(initialize)
        {
            graph = new GraphImplementation(new GraphData(bräde, N));
            controller = new AgentController(graph, N);
            initialize = false;
        }

        graph.Update(bräde);
        controller.Update(bräde);

        return controller.MakePlay();
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}