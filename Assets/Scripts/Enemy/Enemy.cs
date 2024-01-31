using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] int rewardGold = 25;
    [SerializeField] int stealGold = 25;

    Bank bank;

    private void Start() {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        bank.Deposit(rewardGold);
    }

    public void StealGold()
    {
        bank.Withdraw(stealGold);
    }
}
