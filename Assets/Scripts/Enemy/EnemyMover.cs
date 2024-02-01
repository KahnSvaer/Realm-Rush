using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)]float speed = 1f;

    List<Node> path = new();

    GridManager gridManager;
    PathFinder pathFinder;

    private void OnEnable() {
        FindPath();
        ReturnToPath();
        StartCoroutine(FollowPath());
    }

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void FindPath()
    {
        path.Clear();
        path = pathFinder.GetNewPath(); //This seems to be correct
    }

    private void ReturnToPath()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinate);
    }//TO make the start at the starting position of the path

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count ; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
