using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class Tower : MonoBehaviour
{   
    [SerializeField] int cost = 75;
    GridManager gridManager;
    
    [SerializeField] float buildTimer = 0.5f;

    private void Start() {
        StartCoroutine(Build());
    }
    public bool CreateTower(Tower tower, Vector3 position, Transform parent)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            Debug.Log("No bank");
            return false; //This is just exception handling
        }
        if (bank.CurrentFund >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity, parent);
            bank.Withdraw(cost);
            return true;
        }
        else{
            return false;
        }
    }

    IEnumerator Build()
    {   
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
                yield return new WaitForSeconds(buildTimer);
            }
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTimer);
        }
    }
}
