using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Projectile invaderAttackPrefab;
    private int cannonHealth = 5;
    
    void Start() {
        InvokeRepeating(nameof(ShootCannon), 1.0f, 1.0f);
    }

    private void ShootCannon() {
        if (Random.value <= 0.5f) {
            Instantiate(this.invaderAttackPrefab, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            this.cannonHealth--;
        }

        if(this.cannonHealth < 1) {
            Destroy(this.gameObject);
        }
    }
}
