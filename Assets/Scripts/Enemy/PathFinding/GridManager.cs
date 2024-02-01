using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int,Node> Grid { get{ return grid; }} //Getter Property

    [SerializeField]int unitySnapSettings = 10;
    public int UnitySnapSettings{get {return unitySnapSettings;}} 

    private void Awake() {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {   
        if (grid.ContainsKey(coordinates))
            return  grid[coordinates];
        
        return null;
    }

    private void CreateGrid()
    {
        for(int x=0;x<gridSize.x;x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    public void BlockNode(Vector2Int coords)
    {
        if(grid.ContainsKey(coords))
        {
            grid[coords].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / unitySnapSettings),
            y = Mathf.RoundToInt(position.z / unitySnapSettings)
        };
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3
        {
            x = Mathf.RoundToInt(coordinates.x * unitySnapSettings),
            y=0,
            z = Mathf.RoundToInt(coordinates.y * unitySnapSettings)
        };
        return position;
    }

    public void ClearNodes()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.isPath = false;
            entry.Value.isExplored = false;
        }
    }
}
