using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingFund = 150;
    [SerializeField] int currentFund;
    [SerializeField] TMP_Text text;

    public int CurrentFund {get{return currentFund;}}


    private void Awake() {
        currentFund = startingFund;
        UpdateCounter();
    }

    public void Deposit(int amount)
    {
        currentFund += Mathf.Abs(amount);
        UpdateCounter();
    }

    public void Withdraw(int amount)
    {
        currentFund -= Mathf.Abs(amount);
        UpdateCounter();
        if (currentFund < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void UpdateCounter()
    {
        text.text = "Gold "+CurrentFund;
    }
}
