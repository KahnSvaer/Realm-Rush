using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = Color.yellow;
    [SerializeField] Color exploredColor = new Color(1,.5f,0f);

    bool showCoords = true;
    public bool ShowCoords {get {return showCoords;}}

    TMP_Text label;
    Vector2Int coordinates;

    GridManager gridManager;

    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates(); 
    }
    private void Update()
    {
        if (!Application.isPlaying) //This will make the script only work in edit mode for our convineance
        {
            DisplayCoordinates();
            UpdateName();
        }
        SetLabelColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        int unitySnapSize = gridManager.UnitySnapSettings;
        coordinates.x = Mathf.RoundToInt(transform.position.x / unitySnapSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / unitySnapSize); //Note this can't be used for the final build
        label.text = coordinates.x + "," + coordinates.y;
    }

    private void SetLabelColor()
    {
        if (gridManager == null){ return; }
        Node node = gridManager.GetNode(coordinates);

        if (node == null) {return;}
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void UpdateName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
