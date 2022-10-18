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









   public abstract class Graph
    {
        public Graph()
        { }

        public abstract void Build();
        public abstract void AddEdge(Point v, Point w);
        public abstract bool ContainsEdge(Point v, Point w);
        public abstract List<Node> AdjacentTo(Point v);
        
        public new String ToString()
        {
            String s = V + " vertices, " + E + " edges\n";
            for (int v = 0; v < V; v++)
            {
                s += v + ": ";
                
                IEnumerator<int> enumerator = AdjacentTo(v);
                while (enumerator.MoveNext())
                    s += enumerator.Current + " ";
                s += "\n";
            }
            return s;
        }
    }






        public Point NextAlongX()
        {
            int previous = Peek(0);
            for(int i = 1; i < size; i++)
            {
                int current = Peek(i);
                int diff = current - previous;

                if (diff == 1 || diff == -1)
                {
                    return diff;
                }

                previous = current;
            }

            return 0;
        }
        public Point NextAlongY()
        {
            int previous = Peek(0);
            for (int i = 1; i < size; i++)
            {
                int current = Peek(i);
                int diff = current - previous;

                if (diff == SpelBräde.N || diff == -SpelBräde.N)
                {
                    return diff / SpelBräde.N;
                }

                previous = current;
            }

            return 0;
        }