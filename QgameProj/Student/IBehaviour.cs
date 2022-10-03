using System;

namespace Student
{
    public interface IBehaviour
    {
        Drag DoBehaviour(Player player, Opponent opponent, SpelBräde bräde, Graph graph);
        void Transition(IBehaviour behaviour);
    }
}