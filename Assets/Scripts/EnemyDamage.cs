using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{   
    [SerializeField] private float MaxHitpoints = 5; //This would never change
    private float CurrantHitPoints;

    private void OnEnable() {
        CurrantHitPoints = MaxHitpoints;
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
        }
    }
}
