using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> waypoints;
    [SerializeField] [Range(0f, 5f)]float speed = 1f;

    private void Start() {
        StartCoroutine(DisplayWaypoints());
    }

    private IEnumerator DisplayWaypoints()
    {
        foreach(Waypoint waypoint in waypoints)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercentage = 0f;

            transform.LookAt(endPosition);

            while (travelPercentage < 1)
            {
                travelPercentage += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
