﻿using Microsoft.Xna.Framework;
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

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {
        if(!isInitialized)
        {
            graph = new AdjacencyList(bräde);
            controller = new AgentController();
            isInitialized = true;
        }

        graph.Build(bräde); // ~ 5N^2, O(N^2)
        controller.Update(bräde, graph); // ~ 8N^2, O(N^2)
        return controller.GetPlay(); // ~ 3N^2, O(N^2) 
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}