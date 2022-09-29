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
            controller = new AgentController(graph, bräde, N);
            isInitialized = true;
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