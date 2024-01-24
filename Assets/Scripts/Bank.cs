using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingFund = 150;
    [SerializeField] int currentFund;
    public int CurrentFund {get{return currentFund;}}

    private void Awake() {
        currentFund = startingFund;
    }

    public void Deposit(int amount)
    {
        currentFund += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        currentFund -= Mathf.Abs(amount);
        if (currentFund < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
