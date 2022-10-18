Quoridor HT22


AgentBehaviour
BreadthFirstSearch
AdjacencyList
Path



/// AdjacencyListGraph is an undirected graph with an underlying vertex-indexed array of linked lists (adjacency list) datastructure.
adjacencylist


        private Node[] nodes; 
        private int distTo; //~distance
        
        //private Node[/*9*/,/*9*/] nodes;




WallBehaviour.cs:

        private bool hasStartedBlocking; //isBlocking
        private int root, previousRoot, firstRoot, currentGrowth, row;
        Typ type;

