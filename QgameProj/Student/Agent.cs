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
            graph = new GraphImplementation(new GraphData(bräde));
            controller = new AgentController();
            isInitialized = true;
        }

        graph.Update(bräde);
        controller.Update(bräde, graph);


        return controller.GetPlay(graph);
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}