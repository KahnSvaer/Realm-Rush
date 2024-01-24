using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeoponShooter : MonoBehaviour
{
    [SerializeField] Transform Aimer;
    [SerializeField] float weoponDamage = 1f;
    public float WeoponDamage {get {return weoponDamage; }}

    Transform target;

    private void Start() 
    {
        target = FindObjectOfType<EnemyMover>().transform; //This needs to change later   
    }

    // Update is called once per frame
    private void Update()
    {
        AimTarget();
    }

    private void AimTarget()
    {
        Aimer.LookAt(target);
    }
}
