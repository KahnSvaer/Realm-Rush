using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{   
    [SerializeField] private float Hitpoints;

    private void OnParticleCollision(GameObject other) {
        float ParticleDamage = other.transform.parent.parent.GetComponent<WeoponShooter>().WeoponDamage;
        if (Hitpoints > 0)
        {
            Hitpoints -= ParticleDamage;
        }
        if (Hitpoints<=0)
        {
            Destroy(gameObject);
        }
    }
}
