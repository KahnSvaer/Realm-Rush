using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeoponShooter : MonoBehaviour
{
    [SerializeField] Transform Aimer;
    [SerializeField] float weoponDamage = 1f;
    public float WeoponDamage {get {return weoponDamage; }}
    [SerializeField] float ballistaRange = 20f;

    ParticleSystem boltParticle;

    Transform target;
    
    private void Start() {
        boltParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetNFireTarget();
        AimTarget();
    }

    private void SetNFireTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        target = FindClosestEnemy(enemies);
        SetAmmoParticles(enemies.Length);
    }

    private Transform FindClosestEnemy(Enemy[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < enemies.Length; i++)
        {
            float enemyDistance = Vector3.Distance(enemies[i].transform.position, transform.position);
            if (enemyDistance < closestDistance)
            {
                closestDistance = enemyDistance;
                closestEnemy = enemies[i].transform;
            }
        }
        return closestEnemy;
    }

    private void SetAmmoParticles(int Length)
    {
        if (Length == 0)
        {
            boltParticle.Stop();
        }
        else if (Vector3.Magnitude(target.position-transform.position)>ballistaRange)
        {
            boltParticle.Stop();
        }
        else if (!boltParticle.isPlaying)
        {
            boltParticle.Play();
        }
    }

    private void AimTarget()
    {
        Aimer.LookAt(target);
    }
}
