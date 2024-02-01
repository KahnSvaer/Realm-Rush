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
        ReturnToPath(); //This should be first to spawn enemy at startCoords first
        CalculatePath(true);
        StartCoroutine(FollowPath());
    }

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void CalculatePath(bool atStart)
    {
        Vector2Int coordinates;
        if (atStart)
        {
            coordinates = pathFinder.StartCoordinate;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position); 
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private void ReturnToPath()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinate);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count ; i++)
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
