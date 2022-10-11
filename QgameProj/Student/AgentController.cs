namespace Student
{
    public class AgentController
    {
        private Player player;
        private Opponent opponent;

        public IBehaviour Behaviour
        {
            get;
            set;
        }

        public AgentController()
        {
            opponent = new Opponent();
            player = new Player();
            Behaviour = new MoveBehaviour();
        }

        public void Update(SpelBräde bräde, Graph graph)
        {
            player.Update(bräde, graph);
            opponent.Update(bräde, graph);

            if (player.Path.Size() > opponent.Path.Size())
            {
                Behaviour.Transition(this, new WallBehaviour());
            }
            else
            {
                Behaviour.Transition(this, new MoveBehaviour());
            }
        }

        public Drag GetPlay(Graph graph, SpelBräde bräde)
        {
            return Behaviour.DoBehaviour(player, opponent, bräde, graph);
        }


        public Drag MoveBehaviour()
        {
            Drag drag = new Drag();
            drag.typ = Typ.Flytta;
            drag.point = Utility.ToPoint(player.Path.Pop());

            return new Drag();
        }

        public Drag WallBehaviour()
        {
            Path path = opponent.Path;
            Point current = opponent.Position;
            Point next = Utility.ToPoint(path.Peek(0));


        }
    }
}



/*



WallBehaviour --

Solution #1:
Traverse opponents shortest path and, when first possible, block path with wall tangential to path direction.

Solution #2:
Do until opponent is farther from path than player: start placing walls on a row infront of opponent.




*/