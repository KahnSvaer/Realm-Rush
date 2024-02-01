using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{   
    [SerializeField] Vector2Int startCoordinate;
    [SerializeField] Vector2Int endCoordinate;

    Node startNode;
    Node endNode;
    [SerializeField] Node currentSearchNode;
    
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
    }

    private void Start() {
        if (grid.ContainsKey(startCoordinate))
        {
            startNode = grid[startCoordinate];
            currentSearchNode = startNode;
        }

        if (grid.ContainsKey(endCoordinate))
        {
            endNode = grid[endCoordinate];
        }

        BreadthFirstSearch();
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


    private List<Node> BreadthFirstSearch()
    {
        bool isRunning = true;
        AddNode(startNode, null); //As the previous node would be null

        Debug.Log(currentSearchNode.coordinates);

        while (toExplore.Count > 0 && isRunning)
        {
            currentSearchNode = toExplore.Dequeue();
            ExploreNeighBours();
            if(currentSearchNode.coordinates == endCoordinate )
            {
                isRunning = false; //To break the loop
            }
        }

        return BuildPath();

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = currentSearchNode; //Would most probably be our original code
        while(currentNode != null) 
        {
            path.Add(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.isConnectedTo;
        }
        
        path.Reverse();
        return path;
    }

    
    private void AddNode(Node curr, Node prev)
    {
        curr.isConnectedTo = prev;
        reached.Add(curr.coordinates, curr);
        toExplore.Enqueue(curr);
        curr.isExplored = true;
    }
}
