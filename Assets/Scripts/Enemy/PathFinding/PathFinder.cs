using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    Vector2Int[] directions = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ExploreNeighBours(currentSearchNode, nodeSize);
    }

    private void ExploreNeighBours(Node curr)
    {
        List<string> neighbours = new List<string>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int coordinate = curr.coordinates + direction;
            if(gridManager.Grid.ContainsKey(coordinate))
            {
                neighbours.Add(coordinate.ToString());
            } 
        }
    }
}
