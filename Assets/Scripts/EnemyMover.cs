using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> waypoints;
    private void Start() {
        StartCoroutine(DisplayWaypoints());
    }

    private IEnumerator DisplayWaypoints()
    {
        foreach(Waypoint waypoint in waypoints)
        {
            yield return new WaitForSeconds(1f);
            transform.position = waypoint.transform.position;
        }
    }
}
