using Microsoft.Xna.Framework;
using System;

class Agent:BaseAgent {

    [STAThread]
    static void Main() 
    {
        Program.Start(new Agent());
    }

    public Agent() 
    { 

    }

    public override Drag SökNästaDrag(SpelBräde bräde) 
    {
        return new Drag();
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag) 
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}