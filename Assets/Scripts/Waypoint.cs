using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{   
    [SerializeField] GameObject ballistaPrefab;
    
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
            Vector3 position = transform.position;
            Instantiate(ballistaPrefab,position,Quaternion.identity,ballistaParent);
            isPlaceable = false;
        }
    }
}
