using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    [SerializeField] float enemyInterval = 2;
    [SerializeField] int enemyNumber = 5;

    private void Awake() {
        PopulateWaves();
    }
    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private void PopulateWaves()
    {   
        for(int i=0; i<enemyNumber;i++)
        {
            Instantiate(enemyObject, transform).SetActive(false);
        }
    }

    IEnumerator StartSpawn()
    {
        while(true) //Maybe remove later to make the wave function We may also combine waves too
        {
            ObjectEnableInPool();
            yield return new WaitForSeconds(enemyInterval);
        }
    }

    private void ObjectEnableInPool()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (!child.activeInHierarchy)
            {
                child.SetActive(true);  
                return;             
            }
        }
    }
}
