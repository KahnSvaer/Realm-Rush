using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] Tower tower;
    
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {get{return isPlaceable;}}
    
    Transform ballistaParent; 

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates;

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start() 
    {
        ballistaParent = GameObject.FindWithTag("BallistaHolder").transform;
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
    
    private void OnMouseDown() 
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isPlaced = tower.CreateTower(tower, transform.position, ballistaParent);
            if (isPlaced)
                gridManager.BlockNode(coordinates);
        }
    }
}
