using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{   
    [SerializeField] Tower tower;
    
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {get{return isPlaceable;}}
    
    Transform ballistaParent; 

    private void Start() 
    {
        ballistaParent = GameObject.FindWithTag("BallistaHolder").transform;
    }
    
    private void OnMouseDown() 
    {
        if (isPlaceable)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position, ballistaParent);
            isPlaceable = !isPlaced;
        }
    }
}
