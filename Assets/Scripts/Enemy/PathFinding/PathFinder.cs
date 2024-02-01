using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{   
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinate { get {return startCoordinate;}}
    
    [SerializeField] Vector2Int endCoordinate;
    public Vector2Int EndCoordinate { get {return endCoordinate;}}

    Node startNode;
    Node endNode;
    Node currentSearchNode;
    
    Vector2Int[] directions = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid;
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();//To check if explored
    Queue<Node> toExplore = new Queue<Node>();


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
        if (grid.ContainsKey(StartCoordinate))
        {
            startNode = grid[StartCoordinate];
        }

        if (grid.ContainsKey(endCoordinate))
        {
            endNode = grid[endCoordinate];
        }
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinate);
    }

    public List<Node> GetNewPath(Vector2Int EnemyCoords)
    {
        BreadthFirstSearch(EnemyCoords);
        return BuildPath();
    }

    // Start is called before the first frame update

    private void ExploreNeighBours()
    {
        List<Node> neighbours = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int coordinate = currentSearchNode.coordinates + direction;
            if(grid.ContainsKey(coordinate))
            {   
                neighbours.Add(grid[coordinate]);
            }
        }

        foreach (Node neighbour in neighbours)
        {
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {   
                AddNode(neighbour, currentSearchNode);
            }
        }
    }


    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        toExplore.Clear();
        reached.Clear();
        gridManager.ClearNodes(); //TO remove all the past information

        Node startNodeEnemy = grid[coordinates];
        startNodeEnemy.isWalkable = true;
        endNode.isWalkable = true;

        bool isRunning = true;
        AddNode(startNodeEnemy, null); //As the previous node would be null

        while (toExplore.Count > 0 && isRunning)
        {
            currentSearchNode = toExplore.Dequeue();
            ExploreNeighBours();
            if(currentSearchNode.coordinates == endCoordinate )
            {
                isRunning = false; //To break the loop
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = currentSearchNode; //Would most probably be our original code
        while(currentNode != null) 
        {
            path.Add(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.ConnectedTo;
        }
        
        path.Reverse();
        return path;
    }

    
    private void AddNode(Node curr, Node prev)
    {
        curr.ConnectedTo = prev;
        reached.Add(curr.coordinates, curr);
        toExplore.Enqueue(curr);
        curr.isExplored = true;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = true;

            if (newPath[newPath.Count-1] != endNode) //This works better than the tutorial due to some Update
            {
                GetNewPath(); //This line seems to be not working Maybe make it so that enemyObject has a new path list to return back;
                return true;
            }
        }
        return false;
    }

    public void BroadCastPathChange()
    {
        BroadcastMessage("CalculatePath",false,SendMessageOptions.DontRequireReceiver);
    }
}
