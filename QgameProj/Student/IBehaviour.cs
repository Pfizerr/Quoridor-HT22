using System;

namespace Student
{
    public interface IBehaviour
    {
        Drag DoBehaviour(Player player, Opponent opponent, SpelBr�de br�de, Graph graph);
        void Transition(IBehaviour behaviour);
    }
}