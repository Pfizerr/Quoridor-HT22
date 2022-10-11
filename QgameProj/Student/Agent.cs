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

        return controller.GetPlay(graph, bräde);
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }

    public class Node
    {
        public Node()
        {

        }

        public List<Node> AdjacencyList
        {
            get;
            set;
        }

        public int Distance
        {
            get;
            set;
        }
    }

    public class Graph
    {

    }
}