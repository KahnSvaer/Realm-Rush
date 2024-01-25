using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)]float speed = 1f;

    List<Waypoint> path = new();

    private void OnEnable() {
        FindPath();
        ReturnToPath();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();
        Transform pathHolder = GameObject.FindWithTag("Path").transform;
        foreach (Transform child in pathHolder)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint!=null)
            {
                path.Add(waypoint);
            }
        }
    }

    private void ReturnToPath()
    {
        transform.position = path[0].transform.position;
    }//TO make the start at the starting position of the path

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
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
        FinishPath();
    }

    private void FinishPath()
    {
        GetComponent<Enemy>().StealGold();
        gameObject.SetActive(false); //Destory itself at the end of the path
    }
}
