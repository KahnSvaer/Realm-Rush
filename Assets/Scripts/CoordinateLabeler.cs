using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TMP_Text label;
    Vector2Int coordinates;

    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();    
    }
    private void Update() 
    {
        if (!Application.isPlaying) //This will make the script only work in edit mode for our convineance
        {
            DisplayCoordinates();
            UpdateName();
        }    
    }

    private void DisplayCoordinates()
    {   
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
