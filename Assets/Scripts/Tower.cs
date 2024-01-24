using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{   
    [SerializeField] int cost = 75;

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
}
