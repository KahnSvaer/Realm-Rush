using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{   
    [SerializeField] private float maxHitpoints = 5; //This would never change
    [SerializeField] private float difficultyRamp = 1;
    private float CurrantHitPoints;

    Enemy enemy;

    private void OnEnable() {
        CurrantHitPoints = maxHitpoints;
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) {
        float ParticleDamage = other.transform.parent.parent.GetComponent<WeoponShooter>().WeoponDamage;
        if (CurrantHitPoints > 0)
        {
            CurrantHitPoints -= ParticleDamage;
        }
        if (CurrantHitPoints<=0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            maxHitpoints += difficultyRamp;
        }
    }
}
